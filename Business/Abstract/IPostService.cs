using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPostService
    {
        IResult Add(Post post);
        IResult Delete(Post post);
        IResult Update(Post post);
        IDataResult<List<Post>> GetAll();
        IDataResult<List<Post>> GetByUserId(int userId);
        IDataResult<Post> GetById(int id);
        IDataResult<List<Post>> GetMainPost();
        IDataResult<List<Post>> GetCommentsByPostId(int id);
        IResult IncreaseFavNumberByPostId(int id);
        IResult DecreaseFavNumberByPostId(int id);
        IResult IncreaseVerifyNumberByPostId(int id);
        IResult DecreaseVerifyNumberByPostId(int id);
        IDataResult<List<TransferPostDto>> GetTransferPosts();
        IDataResult<List<TransferPostDto>> GetTransferPostsByUserId(int id);

    }
}
