using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProfileImageManager : IProfileImageService
    {
        IProfileImageDal _profileImageDal;

        public ProfileImageManager(IProfileImageDal profileImageDal)
        {
            _profileImageDal = profileImageDal;
        }

        public IResult Add(IFormFile file, ProfileImage profileImage)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(ProfileImage profileImage)
        {
            throw new NotImplementedException();
        }

        public IResult Update(IFormFile file, ProfileImage profileImage)
        {
            throw new NotImplementedException();
        }

        public IDataResult<ProfileImage> GetByUserId(int id)
        {
            return new SuccessDataResult<ProfileImage>(_profileImageDal.Get(p => p.UserId == id));
        }
    }
}
