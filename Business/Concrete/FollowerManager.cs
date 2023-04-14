﻿using Business.Abstract;
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

        public IDataResult<List<int>> GetByFollowerId(int followerId)
        {
            var result = _followerDal.GetAll(f => f.FollowerId == followerId).Select(f => f.FollowedId).ToList();
            return new SuccessDataResult<List<int>>(result,Messages.FollowerListed);
        }

        public IResult Update(Follower follower)
        {
            _followerDal.Update(follower);
            return new SuccessResult(Messages.FollowerUpdated);
        }
    }
}
