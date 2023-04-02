using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBackgroundImageService
    {
        IResult Add(IFormFile file, BackgroundImage backgroundImage);
        IResult Delete(BackgroundImage backgroundImage);
        IResult Update(IFormFile file, BackgroundImage backgroundImage);
        IDataResult<BackgroundImage> GetByUserId(int id);
    }
}
