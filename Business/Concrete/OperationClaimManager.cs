using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        IOperationClaimDal _claimDal;

        public OperationClaimManager(IOperationClaimDal claimDal)
        {
            _claimDal = claimDal;
        }

        public IResult Add(OperationClaim operationClaim)
        {
            _claimDal.Add(operationClaim);
            return new SuccessResult();
        }

        public IResult Delete(OperationClaim operationClaim)
        {
            _claimDal.Delete(operationClaim);
            return new SuccessResult();
        }

        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_claimDal.GetAll());
        }

        public IResult Update(OperationClaim operationClaim)
        {
            _claimDal.Update(operationClaim);
            return new SuccessResult();
        }
    }
}
