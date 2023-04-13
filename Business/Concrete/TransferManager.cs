using Business.Abstract;
using Business.Constants;
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
    public class TransferManager : ITransferService
    {
        ITransferDal _transferDal;

        public TransferManager(ITransferDal transferDal)
        {
            _transferDal = transferDal;
        }

        public IResult Add(Transfer transfer)
        {
            _transferDal.Add(transfer);
            return new SuccessResult(Messages.TransferNewsAdded);
        }

        public IResult Delete(Transfer transfer)
        {
            _transferDal.Delete(transfer);
            return new SuccessResult(Messages.TransferNewsDeleted);
        }

        public IDataResult<List<Transfer>> GetByClubId(int id)
        {
            var result = _transferDal.GetAll(t => t.TeamId == id);
            return new SuccessDataResult<List<Transfer>>(result);
        }

        public IDataResult<List<Transfer>> GetByPlayerId(int id)
        {
            var result = _transferDal.GetAll(t => t.PlayerId == id);
            return new SuccessDataResult<List<Transfer>>(result);
        }

        public IDataResult<Transfer> GetByPostId(int id)
        {
            var result = _transferDal.Get(t => t.PostId == id);
            return new SuccessDataResult<Transfer>(result);
        }

        public IResult Update(Transfer transfer)
        {
            _transferDal.Update(transfer);
            return new SuccessResult(Messages.TransferNewsUpdated);
        }
    }
}
