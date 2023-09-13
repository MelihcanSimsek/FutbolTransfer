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
        IDataResult<Post> Add(Post post);
        IResult Delete(Post post);
        IResult Update(Post post);
        IDataResult<List<Post>> GetAll();
        IDataResult<List<PostDetailDto>> GetByUserId(int userId);
        IDataResult<PostDetailDto> GetPostDetailById(int postId);
        IDataResult<TransferPostDto> GetTransferPostById(int postId);

        IDataResult<List<PostDetailDto>> GetMainPostWithAlgorithm(int userId);
        IDataResult<List<PostDetailDto>> GetCommentsByPostId(int postId,int userId);
        IDataResult<List<TransferPostDto>> GetTransferPosts();
        IDataResult<List<TransferPostDto>> GetTransferPostsWithAlgorithm(int userId);
        IDataResult<Post> GetPostId(Post post);

        IDataResult<List<TransferPostDto>> GetDailyTransferPost();
        IDataResult<List<TransferPostDto>> GetWeeklyTransferPost();
        IDataResult<List<TransferPostDto>> GetMonthlyTransferPost();

    }
}
