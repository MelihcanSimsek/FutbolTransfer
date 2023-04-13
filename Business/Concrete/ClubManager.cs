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
    public class ClubManager : IClubService
    {
        IClubDal _clubDal;

        public ClubManager(IClubDal clubDal)
        {
            _clubDal = clubDal;
        }

        public IResult Add(Club club)
        {
            _clubDal.Add(club);
            return new SuccessResult(Messages.ClubAdded);
        }

        public IResult Delete(Club club)
        {
            _clubDal.Delete(club);
            return new SuccessResult(Messages.ClubDeleted);
        }

        public IDataResult<List<Club>> GetAll()
        {
            var result = _clubDal.GetAll();
            return new SuccessDataResult<List<Club>>(result);
        }

        public IDataResult<Club> GetByClubId(int id)
        {
            var result = _clubDal.Get(c => c.Id == id);
            return new SuccessDataResult<Club>(result);
        }

        public IResult Update(Club club)
        {
            _clubDal.Update(club);
            return new SuccessResult(Messages.ClubUpdated);
        }
    }
}
