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
    public class BackgroundImageManager : IBackgroundImageService
    {
        IBackgroundImageDal _backgroundImageDal;

        public BackgroundImageManager(IBackgroundImageDal backgroundImageDal)
        {
            _backgroundImageDal = backgroundImageDal;
        }

        public IResult Add(IFormFile file, BackgroundImage backgroundImage)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(BackgroundImage backgroundImage)
        {
            _backgroundImageDal.Delete(backgroundImage);
            return new SuccessResult();
        }

        public IDataResult<BackgroundImage> GetByUserId(int id)
        {
            return new SuccessDataResult<BackgroundImage>(_backgroundImageDal.Get(p => p.UserId == id));
        }

        public IResult Update(IFormFile file, BackgroundImage backgroundImage)
        {
            throw new NotImplementedException();
        }
    }
}
