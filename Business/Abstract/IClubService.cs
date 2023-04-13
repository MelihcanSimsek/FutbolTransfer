using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IClubService
    {
        IResult Add(Club club);
        IResult Delete(Club club);
        IResult Update(Club club);
        IDataResult<List<Club>> GetAll();
        IDataResult<Club> GetByClubId(int id);
    }
}
