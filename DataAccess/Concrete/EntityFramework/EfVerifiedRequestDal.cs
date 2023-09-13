using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfVerifiedRequestDal : EfEntityFrameworkBase<VerifiedRequest, FutbolTransferContext>, IVerifiedRequestDal
    {
        public List<RequestDto> GetRequests(Expression<Func<RequestDto, bool>> filter = null)
        {
            using (var context = new FutbolTransferContext())
            {
                var result = from request in context.VerifiedRequests
                             join user in context.Users
                             on request.UserId equals user.Id
                             join profile in context.Profiles
                             on user.Id equals profile.UserId
                             select new RequestDto
                             {
                                 Id = request.Id,
                                 UserId = user.Id,
                                 Message = request.Message,
                                 ProfileImage = profile.ProfileImage,
                                 Username = user.Name
                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
