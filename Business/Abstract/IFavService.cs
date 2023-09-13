using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFavService
    {
        IResult Add(Fav fav);
        IResult Delete(Fav fav);
        IResult Update(Fav fav);
        IDataResult<List<int>> GetAll(int id);
        IDataResult<List<int>> GetPostCommentsFavs(int userId, int postId);

    }
}
