using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITransferService
    {
        IResult Add(Transfer transfer);
        IResult Delete(Transfer transfer);
        IResult Update(Transfer transfer);
        IDataResult<Transfer> GetByPostId(int id);
        IDataResult<List<Transfer>> GetByPlayerId(int id);
        IDataResult<List<Transfer>> GetByClubId(int id);
    }
}
