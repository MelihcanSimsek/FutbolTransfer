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
    public interface IProfileService
    {
        IResult Add(Profile profile);
        IResult Delete(Profile profile);
        IResult Update(Profile profile);
        IDataResult<List<Profile>> GetAll();
        IDataResult<Profile> GetByUserId(int id);
        IResult ProfileImageUpdate(IFormFile formFile, Profile profile);
        IResult BackgroundImageUpdate(IFormFile formFile, Profile profile);
    }
}
