using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        ITokenHelper _tokenHelper;
        IUserService _userService;


        public AuthManager(ITokenHelper tokenHelper,IUserService userService)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken =  _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.TokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLogin)
        {
            var userToCheck = _userService.GetByEmail(userForLogin.Email);
            if( !userToCheck.Success)
            {
                return new ErrorDataResult<User>(userToCheck.Message);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLogin.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegister)
        {
            byte[] passwordSalt, passwordHash;
           
            HashingHelper.CreatePasswordHash(userForRegister.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegister.Email,
                Name = userForRegister.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
           if(_userService.GetByEmail(email).Data != null)
            {
                return new SuccessResult(Messages.UserAlreadyExists);
            }
            return new ErrorResult();
        }
    }
}
