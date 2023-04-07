using Business.Abstract;
using Business.Constants;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProfileManager : IProfileService
    {
        IProfileDal _profileDal;

        public ProfileManager(IProfileDal profileDal)
        {
            _profileDal = profileDal;
        }

        public IResult Add(Profile profile)
        {
            _profileDal.Add(profile);
            return new SuccessResult();
        }

        public IResult BackgroundImageUpdate(IFormFile formFile, Profile profile)
        {
            var profileModel = BackgroundImageCheck(formFile, profile).Data;
            _profileDal.Update(profileModel);
            return new SuccessResult(Messages.BackgroundImageUpdated);
        }

        public IResult Delete(Profile profile)
        {
            _profileDal.Delete(profile);
            return new SuccessResult();
        }

        public IDataResult<List<Profile>> GetAll()
        {
            return new SuccessDataResult<List<Profile>>(_profileDal.GetAll());
        }

        public IDataResult<Profile> GetByUserId(int id)
        {
            var profileModel = _profileDal.Get(p => p.Id == id);
            profileModel.BackgroundImage = GetDefaultBackgroundImage(profileModel);
            profileModel.ProfileImage = GetDefaultProfileImage(profileModel);
            return new SuccessDataResult<Profile>(profileModel);
           
        }

        public IResult ProfileImageUpdate(IFormFile formFile, Profile profile)
        {
            var profileModel = ProfileImageCheck(formFile, profile).Data;
            _profileDal.Update(profileModel);
            return new SuccessResult(Messages.ProfileImageUpdated);
        }

        public IResult Update(Profile profile)
        {
            _profileDal.Update(profile);
            return new SuccessResult();
        }

        private IDataResult<Profile> BackgroundImageCheck(IFormFile file,Profile profile)
        {
            var profileToCheck = _profileDal.Get(p => p.Id == profile.Id);
            if (profileToCheck.BackgroundImage != null)
            {
                profile.BackgroundImage = FileHelper.Update(profileToCheck.BackgroundImage, file);
            }
            else
            {
                profile.BackgroundImage = FileHelper.Add(file);
            }
            return new SuccessDataResult<Profile>(profile);
        }

        private IDataResult<Profile> ProfileImageCheck(IFormFile file,Profile profile)
        {
            var profileToCheck = _profileDal.Get(p => p.Id == profile.Id);
            if(profileToCheck.ProfileImage != null)
            {
                profile.ProfileImage = FileHelper.Update(profileToCheck.ProfileImage, file);
            }
            else
            {
                profile.ProfileImage = FileHelper.Add(file);
            }
            return new SuccessDataResult<Profile>(profile);
        }

        private string GetDefaultBackgroundImage(Profile profile)
        {
            if(profile.BackgroundImage == null)
            {
                return "DefaultBackgroundImage.jpg";
            }else
            {
                return profile.BackgroundImage;
            }

        }
        private string GetDefaultProfileImage(Profile profile)
        {
            if (profile.ProfileImage == null)
            {
                return "DefaultProfileImage.jpg";
            }
            else
            {
                return profile.ProfileImage;
            }
        }
    }
}
