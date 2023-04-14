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
        public PostManager(IPostDal postDal,IFollowerService followerService)
        {
            _postDal = postDal;
            _followerService = followerService;
        }

        public IResult Add(Post post)
        {
            _postDal.Add(post);
            CheckIfPostIsComment(post);
            return new SuccessResult(Messages.PostAdded);
        }

        public IResult Delete(Post post)
        {
            _postDal.Delete(post);
            return new SuccessResult(Messages.PostDeleted);
        }

        public IResult IncreaseFavNumberByPostId(int id)
        {
            var post = _postDal.Get(p => p.Id == id);
            post.Fav = post.Fav + 1;
            Update(post);
            return new SuccessResult(Messages.PostLiked);
        }

        public IDataResult<List<Post>> GetAll()
        {
            return new SuccessDataResult<List<Post>>(_postDal.GetAll(),Messages.PostListed);
        }

        public IDataResult<PostDetailDto> GetById(int postId)
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
            var followedUserIds = _followerService.GetByFollowerId(userId).Data;
            var allPosts = _postDal.GetPostDetails(p => p.ParentId == postId);
            var sortedPosts = GetBestOfPosts(allPosts);
            var nonFollowedPosts = sortedPosts.Where(p => !followedUserIds.Contains(p.UserId)).OrderByDescending(p => p.CreationDate).ToList();
            var followedPosts = sortedPosts.Where(p => followedUserIds.Contains(p.UserId)).OrderByDescending(p => p.CreationDate).ToList();
            var concatenatedPosts = followedPosts.Concat(nonFollowedPosts).ToList();
            return new SuccessDataResult<List<PostDetailDto>>(concatenatedPosts);
        }

        public IDataResult<List<PostDetailDto>> GetMainPostWithAlgorithm(int userId)
        {
            var followedUserIds = _followerService.GetByFollowerId(userId).Data;
            var allPosts = _postDal.GetPostDetails(p=>p.ParentId == 0 && p.CreationDate >= DateTime.Now.AddDays(-1));
            var sortedPosts = GetBestOfPosts(allPosts);
            var nonFollowedPosts = sortedPosts.Where(p => !followedUserIds.Contains(p.UserId)).OrderByDescending(p => p.CreationDate).Take(40);
            var followedPosts = sortedPosts.Where(p => followedUserIds.Contains(p.UserId)).OrderByDescending(p => p.CreationDate).Take(25);
            var unionPosts = nonFollowedPosts.Union(followedPosts).OrderByDescending(p => p.CreationDate).Take(30).ToList();
            return new SuccessDataResult<List<PostDetailDto>>(unionPosts, Messages.PostListed);
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

        public IResult DecreaseFavNumberByPostId(int id)
        {
            var post = _postDal.Get(p => p.Id == id);
            if(post.Fav <= 0 )
            {

                return new ErrorResult();
            }
            post.Fav = post.Fav - 1;
            Update(post);
            return new SuccessResult(Messages.PostUnliked);
        }

        public IResult IncreaseVerifyNumberByPostId(int id)
        {
            var post = _postDal.Get(p => p.Id == id);
            post.Verify = post.Verify + 1;
            Update(post);
            return new SuccessResult(Messages.PostVerified);
        }

        public IResult DecreaseVerifyNumberByPostId(int id)
        {
            var post = _postDal.Get(p => p.Id == id);
            if(post.Verify <= 0)
            {
                return new ErrorResult();
            }
            post.Verify = post.Verify - 1;
            Update(post);
            return new SuccessResult(Messages.PostUnverified);

        }

        public IDataResult<List<TransferPostDto>> GetTransferPosts()
        {
            var result = _postDal.GetTransferPosts();
            return new SuccessDataResult<List<TransferPostDto>>(result);
        }

        public IDataResult<List<TransferPostDto>> GetTransferPostsByUserId(int id)
        {
            var result = _postDal.GetTransferPosts(tp => tp.UserId == id);
            return new SuccessDataResult<List<TransferPostDto>>(result);
        }

        private List<PostDetailDto> GetBestOfPosts(List<PostDetailDto> allPosts)
        {
            var bestOfPosts = allPosts.Select(p => new
            {
                Post = p,
                Score = p.Fav + p.Verify * 2 + p.Comment * 3
            }).ToList();
            var sortedPosts = bestOfPosts.OrderByDescending(p => p.Score).Select(p => p.Post).ToList();
            return sortedPosts;
        }
        
    }
}
