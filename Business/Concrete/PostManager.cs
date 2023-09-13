using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PostManager : IPostService
    {
        IPostDal _postDal;
        IFollowerService _followerService;
        IVerifyService _verifyService;
        IUserOperationClaimService _userOperationClaimService;

        public PostManager(IPostDal postDal,IFollowerService followerService,IVerifyService verifyService, IUserOperationClaimService userOperationClaimService)
        {
            _postDal = postDal;
            _followerService = followerService;
            _verifyService = verifyService;
            _userOperationClaimService = userOperationClaimService;

        }

        public IDataResult<Post> Add(Post post)
        {
            post.CreationDate = DateTime.Now;
            _postDal.Add(post);
            CheckIfPostIsComment(post);
            return new SuccessDataResult<Post>(post,Messages.PostAdded);
        }

        public IResult Delete(Post post)
        {
            _postDal.Delete(post);
            return new SuccessResult(Messages.PostDeleted);
        }

       

        public IDataResult<List<Post>> GetAll()
        {
            return new SuccessDataResult<List<Post>>(_postDal.GetAll(),Messages.PostListed);
        }

        public IDataResult<PostDetailDto> GetPostDetailById(int postId)
        {
            var post = _postDal.GetPostDetails(p => p.PostId == postId).SingleOrDefault();
            return new SuccessDataResult<PostDetailDto>(post);
        }



        public IDataResult<List<PostDetailDto>> GetByUserId(int userId)
        {
            var allPosts = _postDal.GetPostDetails(p => p.ParentId == 0 && p.UserId == userId).OrderByDescending(p => p.CreationDate).ToList();
            return new SuccessDataResult<List<PostDetailDto>>(allPosts);
        }

        public IDataResult<List<PostDetailDto>> GetCommentsByPostId(int postId,int userId)
        {
            var followedUserIds = _followerService.GetFollowedList(userId).Data;
            var allPosts = _postDal.GetPostDetails(p => p.ParentId == postId);
            var sortedPosts = GetBestOfPosts(allPosts);
            var nonFollowedPosts = sortedPosts.Where(p => !followedUserIds.Contains(p.UserId)).ToList();
            var followedPosts = sortedPosts.Where(p => followedUserIds.Contains(p.UserId)).OrderByDescending(p => p.CreationDate).ToList();
            var concatenatedPosts = followedPosts.Concat(nonFollowedPosts).ToList();
            return new SuccessDataResult<List<PostDetailDto>>(concatenatedPosts);
        }

        public IDataResult<List<PostDetailDto>> GetMainPostWithAlgorithm(int userId)
        {
            var followedUserIds = _followerService.GetFollowedList(userId).Data;
            var allPosts = _postDal.GetPostDetails(p=>p.ParentId == 0 && p.CreationDate >= DateTime.Now.AddDays(-1).Date);
            var nonFollowedPosts = allPosts.Where(p => !followedUserIds.Contains(p.UserId)).Take(20);
            var followedPosts = allPosts.Where(p => followedUserIds.Contains(p.UserId)).Take(20);
            var unionPosts = nonFollowedPosts.Union(followedPosts).Take(50).ToList();
            var sortedPosts = GetBestOfPosts(unionPosts);
            var HourlyPosts = new List<PostDetailDto>();
            var restOfPosts = new List<PostDetailDto>();
            foreach (var post in sortedPosts)
            {
                if(post.CreationDate > DateTime.Now.AddHours(-1))
                {
                    HourlyPosts.Add(post);
                }
                else
                {
                    restOfPosts.Add(post);

                }
            }
            var concatenatedPosts = HourlyPosts.Concat(restOfPosts).ToList();
            return new SuccessDataResult<List<PostDetailDto>>(concatenatedPosts, Messages.PostListed);
        }

        public IResult Update(Post post)
        {
            _postDal.Update(post);
            return new SuccessResult(Messages.PostUpdated);
        }

        private void CheckIfPostIsComment(Post post)
        {
            if(post.ParentId != 0)
            {
                var parentPost = _postDal.Get(p => p.Id == post.ParentId);
                parentPost.Comment = parentPost.Comment + 1;
                _postDal.Update(parentPost);
            }
        }

        public IDataResult<List<TransferPostDto>> GetTransferPosts()
        {
            var allTransferPost = _postDal.GetTransferPosts(tp=>tp.CreationDate >= DateTime.Now.AddDays(-1).Date);
            var bestOfTransferPost = GetBestOfTransferPost(allTransferPost).Take(40).ToList();
            var totalReporter = _userOperationClaimService.GetAllReporter().Data.Count;
            var quarterReporter = totalReporter / 3;
            foreach (var transferPost in bestOfTransferPost)
            {
                var totalVerifyNumber = _verifyService.GetAllByPostId(transferPost.PostId).Data.Count;
                if (totalVerifyNumber > quarterReporter)
                {
                    transferPost.TransferRate = 100;
                }
                else
                {
                    transferPost.TransferRate = (int)(((float)totalVerifyNumber / (float)quarterReporter) * 100);
                }

            }
            return new SuccessDataResult<List<TransferPostDto>>(bestOfTransferPost);
        }

        public IDataResult<List<TransferPostDto>> GetTransferPostsWithAlgorithm(int userId)
        {
            var result = _postDal.GetTransferPosts(tp => tp.UserId == userId);
            var sortedResult = result.OrderByDescending(tp => tp.CreationDate).ToList();
            return new SuccessDataResult<List<TransferPostDto>>(sortedResult);
        }

        private List<PostDetailDto> GetBestOfPosts(List<PostDetailDto> allPosts)
        {
            var bestOfPosts = allPosts.Select(p => new
            {
                Post = p,
                Score = p.Fav.Length*3+p.Verify.Length*1+ p.Comment * 5
            }).ToList();
            var sortedPosts = bestOfPosts.OrderByDescending(p => p.Score).Select(p => p.Post).ToList();
            return sortedPosts;
        }

        private List<TransferPostDto> GetBestOfTransferPost(List<TransferPostDto> allTransferPost)
        {
            var bestOfPosts = allTransferPost.Select(p => new
            {
                Post = p,
                Score = p.Fav.Length*2 + p.Verify.Length * 5 + p.Comment * 3
            }).ToList();
            var sortedPosts = bestOfPosts.OrderByDescending(p => p.Score).Select(p => p.Post).ToList();
            return sortedPosts;
        }

        public IDataResult<Post> GetPostId(Post post)
        {
            var result = _postDal.Get(p => p.UserId == post.UserId && p.CreationDate >= post.CreationDate.AddSeconds(-30) && p.CreationDate <= post.CreationDate.AddSeconds(30) && p.Status == true);
            return new SuccessDataResult<Post>(result);
        }

        public IDataResult<TransferPostDto> GetTransferPostById(int postId)
        {
            var result = _postDal.GetTransferPosts(tp => tp.PostId == postId).SingleOrDefault();
            var totalVerifyNumber = _verifyService.GetAllByPostId(result.PostId).Data.Count;
            var totalReporter = _userOperationClaimService.GetAllReporter().Data.Count;
            var quarterReporter = totalReporter / 3;
            var halfReporter = totalReporter / 2;
            if(result.CreationDate >= DateTime.Now.AddDays(-1))
            {
                if(totalVerifyNumber > quarterReporter)
                {
                    result.TransferRate = 100;
                }
                else
                {
                    result.TransferRate = (int)(((float)totalVerifyNumber / (float)quarterReporter) * 100);
                }
            }
            else if(result.CreationDate < DateTime.Now.AddDays(-1) && result.CreationDate >= DateTime.Now.AddDays(-7))
            {
                if(totalVerifyNumber > halfReporter )
                {
                    result.TransferRate = 100;
                }
                else
                {
                    result.TransferRate = (int)(((float)totalVerifyNumber / (float)halfReporter) * 100);
                }
            }
            else
            {
                result.TransferRate = (int)(((float)totalVerifyNumber / (float)totalReporter) * 100);

            }
            return new SuccessDataResult<TransferPostDto>(result);
        }

        public IDataResult<List<TransferPostDto>> GetDailyTransferPost()
        {
            var dailyTransferPost = _postDal.GetTransferPosts(t => t.CreationDate >= DateTime.Now.AddDays(-1));
            var totalReporter = _userOperationClaimService.GetAllReporter().Data.Count;
            var quarterReporter = totalReporter / 3;
            foreach (var transferPost in dailyTransferPost)
            {
                var totalVerifyNumber = _verifyService.GetAllByPostId(transferPost.PostId).Data.Count;
                if(totalVerifyNumber > quarterReporter)
                {
                    transferPost.TransferRate = 100;
                }
                else
                {
                    transferPost.TransferRate = (int)(((float)totalVerifyNumber / (float)quarterReporter) * 100);
                }
            }

            return new SuccessDataResult<List<TransferPostDto>>(dailyTransferPost);
        }

        public IDataResult<List<TransferPostDto>> GetWeeklyTransferPost()
        {
            var weeklyTransferPost = _postDal.GetTransferPosts(t => t.CreationDate < DateTime.Now.AddDays(-1) && t.CreationDate >= DateTime.Now.AddDays(-7));
            var totalReporter = _userOperationClaimService.GetAllReporter().Data.Count;
            var halfReporter = totalReporter / 2;
            foreach (var transferPost in weeklyTransferPost)
            {
                var totalVerifyNumber = _verifyService.GetAllByPostId(transferPost.PostId).Data.Count;
                if (totalVerifyNumber > halfReporter)
                {
                    transferPost.TransferRate = 100;
                }
                else
                {
                    transferPost.TransferRate = (int)(((float)totalVerifyNumber / (float)halfReporter) * 100);
                }

            }
            return new SuccessDataResult<List<TransferPostDto>>(weeklyTransferPost);
        }

        public IDataResult<List<TransferPostDto>> GetMonthlyTransferPost()
        {
            var monthlyTransferPost = _postDal.GetTransferPosts(t => t.CreationDate >= DateTime.Now.AddDays(-30) && t.CreationDate < DateTime.Now.AddDays(-7));
            var totalReporter = _userOperationClaimService.GetAllReporter().Data.Count;
       
            foreach (var transferPost in monthlyTransferPost)
            {
                var totalVerifyNumber = _verifyService.GetAllByPostId(transferPost.PostId).Data.Count;
                transferPost.TransferRate =(int) (((float)totalVerifyNumber / (float)totalReporter) * 100);
            }
            return new SuccessDataResult<List<TransferPostDto>>(monthlyTransferPost);
        }
    }
}
