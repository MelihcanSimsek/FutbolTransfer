using Core.DataAccess;
using Core.Entities;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPostDal:IEntityRepository<Post>
    {
        List<TransferPostDto> GetTransferPosts(Expression<Func<TransferPostDto, bool>> filter = null);
        List<PostDetailDto> GetPostDetails(Expression<Func<PostDetailDto, bool>> filter = null);
        
    }
}
