using Core.DataAccess;
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
    public interface IVerifiedRequestDal:IEntityRepository<VerifiedRequest>
    {
        List<RequestDto> GetRequests(Expression<Func<RequestDto, bool>> filter = null);
    }
}
