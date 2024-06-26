﻿using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class VerifiedRequestManager : IVerifiedRequestService
    {
        IVerifiedRequestDal _requestDal;

        public VerifiedRequestManager(IVerifiedRequestDal requestDal)
        {
            _requestDal = requestDal;
        }

        public IResult Add(VerifiedRequest verifiedRequest)
        {
            _requestDal.Add(verifiedRequest);
            return new SuccessResult();
        }

        public IResult Delete(VerifiedRequest verifiedRequest)
        {
            _requestDal.Delete(verifiedRequest);
            return new SuccessResult();
        }

        public IDataResult<List<RequestDto>> GetAll()
        {
            var result = _requestDal.GetRequests();
            return new SuccessDataResult<List<RequestDto>>(result);
        }

        public IDataResult<VerifiedRequest> GetByRequestId(int id)
        {
            var result = _requestDal.Get(r => r.Id == id);
            return new SuccessDataResult<VerifiedRequest>(result);
        }

        public IDataResult<VerifiedRequest> GetRequestByUserId(int id)
        {
            var result = _requestDal.Get(r => r.UserId == id);
            return new SuccessDataResult<VerifiedRequest>(result);
        }

        public IResult Update(VerifiedRequest verifiedRequest)
        {
            _requestDal.Update(verifiedRequest);
            return new SuccessResult();
        }
    }
}
