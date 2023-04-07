using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        IProfileService _profileService;

        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost("add")]
        public IActionResult Add(Profile profile)
        {
            var result = _profileService.Add(profile);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("delete")]
        public IActionResult Delete(Profile profile)
        {
            var result = _profileService.Delete(profile);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Profile profile)
        {
            var result = _profileService.Update(profile);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _profileService.GetAll();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId(int id)
        {
            var result = _profileService.GetByUserId(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("backgroundimageupdate")]
        public IActionResult BackgroundImageUpdate([FromForm(Name = ("Image"))] IFormFile file, [FromForm] Profile profile)
        {
            var result = _profileService.BackgroundImageUpdate(file, profile);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("profileimageupdate")]
        public IActionResult ProfileImageUpdate([FromForm(Name = ("Image"))] IFormFile file, [FromForm] Profile profile)
        {
            var result = _profileService.ProfileImageUpdate(file, profile);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
