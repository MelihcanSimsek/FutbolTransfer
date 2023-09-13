using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVerifyService
    {

        IResult Add(Verify verify);
        IResult Delete(Verify verify);
        IResult Update(Verify verify);
        IDataResult<List<int>> GetAll(int id);
        IDataResult<List<int>> GetVerify(int userid, int postid);
        IDataResult<List<Verify>> GetAllByPostId(int id);

    }
}
