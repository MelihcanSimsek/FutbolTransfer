using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class VerifyManager : IVerifyService
    {
        IVerifyDal _verifyDal;

        public VerifyManager(IVerifyDal verifyDal)
        {
            _verifyDal = verifyDal;
        }

        public IResult Add(Verify verify)
        {
            verify.CreationDate = DateTime.Now;
            _verifyDal.Add(verify);
            return new SuccessResult();
        }

        public IResult Delete(Verify verify)
        {
            var result = _verifyDal.Get(v => v.UserId == verify.UserId && v.PostId == verify.PostId);
            _verifyDal.Delete(result);
            return new SuccessResult();
        }

        public IDataResult<List<int>> GetAll(int id)
        {
            var result = _verifyDal.GetAll(v=>v.UserId == id && v.CreationDate >= DateTime.Now.AddDays(-1)).Select(v=>v.PostId).ToList();
            return new SuccessDataResult<List<int>>(result);
        }

        public IDataResult<List<Verify>> GetAllByPostId(int id)
        {
            var result = _verifyDal.GetAll(v => v.PostId == id);
            return new SuccessDataResult<List<Verify>>(result);
        }

        public IDataResult<List<int>> GetVerify(int userid, int postid)
        {
            var result = _verifyDal.GetAll(v => v.UserId == userid && v.PostId == postid).Select(v=>v.PostId).ToList();
            return new SuccessDataResult<List<int>>(result);
            
        }

        public IResult Update(Verify verify)
        {
            _verifyDal.Update(verify);
            return new SuccessResult();
        }
    }
}
