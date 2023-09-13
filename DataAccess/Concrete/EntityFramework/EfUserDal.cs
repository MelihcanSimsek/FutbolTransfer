using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityFrameworkBase<User, FutbolTransferContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new FutbolTransferContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

                return result.ToList();
                            
            }
        }

        public List<UserInformationDto> GetUserInformation(Expression<Func<UserInformationDto, bool>> filter = null)
        {
            using (var context = new FutbolTransferContext())
            {
                var result = from user in context.Users
                             select new UserInformationDto
                             {
                                 Email = user.Email,
                                 CreationDate = user.CreationDate,
                                 UserId = user.Id,
                                 UserName = user.Name,
                                 Roles = context.OperationClaims.Where(oc => context.UserOperationClaims
                                 .Any(uoc => uoc.UserId == user.Id && uoc.OperationClaimId == oc.Id))
                                 .Select(oc => oc.Name)
                                 .ToArray()
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
