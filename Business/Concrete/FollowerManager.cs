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
    public class FollowerManager : IFollowerService
    {
        IFollowerDal _followerDal;
        public FollowerManager(IFollowerDal followerDal)
        {
            _followerDal = followerDal;
        }
        public IResult Add(Follower follower)
        {
            _followerDal.Add(follower);
            return new SuccessResult(Messages.FollowerAdded);
        }

        public IResult Delete(Follower follower)
        {
            _followerDal.Delete(follower);
            return new SuccessResult(Messages.FollowerDeleted);
        }

        public IDataResult<List<Follower>> GetByFollowerId(int followerId)
        {
            return new SuccessDataResult<List<Follower>>(_followerDal.GetAll(f => f.FollowerId == followerId),Messages.FollowerListed);
        }

        public IResult Update(Follower follower)
        {
            _followerDal.Update(follower);
            return new SuccessResult(Messages.FollowerUpdated);
        }
    }
}
