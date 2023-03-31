using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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
            return new SuccessResult(Messages.PostAdded);
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
            return new SuccessDataResult<List<Post>>(_postDal.GetAll(p => p.ParentId == null),Messages.MainPostListed);
        }

        public IResult Update(Post post)
        {
            _postDal.Update(post);
            return new SuccessResult(Messages.PostUpdated);
        }
    }
}
