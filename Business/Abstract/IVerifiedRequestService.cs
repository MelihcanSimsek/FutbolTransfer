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
    public interface IVerifiedRequestService
    {
        IResult Add(VerifiedRequest verifiedRequest);
        IResult Delete(VerifiedRequest verifiedRequest);
        IResult Update(VerifiedRequest verifiedRequest);
        IDataResult<List<RequestDto>> GetAll();
        IDataResult<VerifiedRequest> GetByRequestId(int id);
        IDataResult<VerifiedRequest> GetRequestByUserId(int id);
    }
}
