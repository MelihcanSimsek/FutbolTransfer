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
    public interface IProfileImageService
    {
        IResult Add(IFormFile file, ProfileImage profileImage);
        IResult Delete(ProfileImage profileImage);
        IResult Update(IFormFile file, ProfileImage profileImage);
        IDataResult<ProfileImage> GetByUserId(int id);
    }
}
