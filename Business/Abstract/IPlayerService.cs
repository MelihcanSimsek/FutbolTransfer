using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPlayerService
    {
        IResult Add(Player player);
        IResult Delete(Player player);
        IResult Update(Player player);
        IDataResult<List<Player>> GetAll();
        IDataResult<Player> GetPlayerById(int id);
    }
}
