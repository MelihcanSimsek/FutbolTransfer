using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        IFollowerService _followerService;
        public FollowersController(IFollowerService followerService)
        {
            _followerService = followerService;
        }

        [HttpPost("add")]
        public IActionResult Add(Follower follower)
        {
            var result = _followerService.Add(follower);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Follower follower)
        {
            var result = _followerService.Delete(follower);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Follower follower)
        {
            var result = _followerService.Update(follower);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getfollowerslist")]
        public IActionResult GetFollowersList(int id)
        {
            var result = _followerService.GetFollowersList(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getfollowedlist")]
        public IActionResult GetFollowedList(int id)
        {
            var result = _followerService.GetFollowedList(id);
            if(result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
