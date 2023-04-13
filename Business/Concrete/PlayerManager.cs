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
    public class PlayerManager : IPlayerService
    {
        IPlayerDal _playerDal;

        public PlayerManager(IPlayerDal playerDal)
        {
            _playerDal = playerDal;
        }

        public IResult Add(Player player)
        {
            _playerDal.Add(player);
            return new SuccessResult(Messages.PlayerAdded);
        }

        public IResult Delete(Player player)
        {
            _playerDal.Delete(player);
            return new SuccessResult(Messages.PlayerDeleted);
        }

        public IDataResult<List<Player>> GetAll()
        {
            return new SuccessDataResult<List<Player>>(_playerDal.GetAll());
        }

        public IDataResult<Player> GetPlayerById(int id)
        {
            var result = _playerDal.Get(p => p.Id == id);
            return new SuccessDataResult<Player>(result);
        }

        public IResult Update(Player player)
        {
            _playerDal.Update(player);
            return new SuccessResult(Messages.PlayerUpdated);
        }
    }
}
