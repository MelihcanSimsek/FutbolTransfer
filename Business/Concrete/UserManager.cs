using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IProfileService _profileService;
        
        public UserManager(IUserDal userDal,IProfileService profileService)
        {
            _userDal = userDal;
            _profileService = profileService;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            var userForId =  GetByEmail(user.Email).Data;
            var profile = new Profile
            {
                UserId = userForId.Id
            };
            _profileService.Add(profile);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<User> GetByEmail(string email)
        {
            var result = _userDal.Get(u => u.Email == email);
            if(result  == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(result,Messages.UserListed);
        }

        public IDataResult<User> GetByUserId(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id),Messages.UserListed);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<UserInformationDto> GetUserInformationById(int id)
        {
            var result = _userDal.GetUserInformation(u => u.UserId == id).SingleOrDefault();
            return new SuccessDataResult<UserInformationDto>(result);
        }

        public IResult Update(User user)
        {
            var result = _userDal.Get(u => u.Id == user.Id);
            result.Name = user.Name;
            _userDal.Update(result);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult UpdateUserPassword(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.PasswordUpdated);
        }
    }
}
