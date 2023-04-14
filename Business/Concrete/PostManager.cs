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
        public PostManager(IPostDal postDal)
        {
            _postDal = postDal;
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

        public IDataResult<Post> GetById(int id)
        {
            return new SuccessDataResult<Post>(_postDal.Get(p => p.Id == id));
        }

        public IDataResult<List<Post>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<Post>>(_postDal.GetAll(p => p.UserId == userId));
        }

        public IDataResult<List<Post>> GetCommentsByPostId(int id)
        {
            return new SuccessDataResult<List<Post>>(_postDal.GetAll(p => p.ParentId == id),Messages.CommentListed);
        }

        public IDataResult<List<Post>> GetMainPost()
        {
            return new SuccessDataResult<List<Post>>(_postDal.GetAll(p => p.ParentId == 0),Messages.MainPostListed);
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
    }
}
