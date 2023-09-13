using Business.Abstract;
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
    public class FavManager : IFavService
    {
        IFavDal _favDal;
        IPostDal _postDal;

        public FavManager(IFavDal favDal,IPostDal postDal)
        {
            _favDal = favDal;
            _postDal = postDal;
        }

        public IResult Add(Fav fav)
        {
            fav.CreationDate = DateTime.Now;
            _favDal.Add(fav);
            return new SuccessResult();
        }

        public IResult Delete(Fav fav)
        {
            var result =  _favDal.Get(f => f.UserId == fav.UserId && f.PostId == fav.PostId);
            _favDal.Delete(result);

            return new SuccessResult();
        }

        public IDataResult<List<int>> GetAll(int id)
        {
            var result = _favDal.GetAll(f=>f.UserId == id && f.CreationDate >= DateTime.Now.AddDays(-1)).Select(f=>f.PostId).ToList();
            return new SuccessDataResult<List<int>>(result);
        }

        public IResult Update(Fav fav)
        {
            _favDal.Update(fav);
            return new SuccessResult();
        }

        public IDataResult<List<int>> GetPostCommentsFavs(int userId, int postId)
        {
            var commentIds = _postDal.GetAll(p => p.ParentId == postId).Select(p => p.Id).ToList();
            var favs = _favDal.GetAll(f => f.UserId == userId);
            var intersectIds = favs.Select(f=>f.PostId).ToList().Intersect(commentIds).ToList();
            if(favs.Any(f=>f.PostId == postId))
            {
                intersectIds.Add(postId);
            }
            return new SuccessDataResult<List<int>>(intersectIds);
        }
    }
}
