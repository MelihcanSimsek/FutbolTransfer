using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifiesController : ControllerBase
    {
        IVerifyService _verifyService;

        public VerifiesController(IVerifyService verifyService)
        {
            _verifyService = verifyService;
        }



        [HttpPost("add")]
        public IActionResult Add(Verify verify)
        {
            var result = _verifyService.Add(verify);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Verify verify)
        {
            var result = _verifyService.Delete(verify);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(Verify verify)
        {
            var result = _verifyService.Update(verify);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getall")]
        public IActionResult GetAll(int id)
        {
            var result = _verifyService.GetAll(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getverify")]
        public IActionResult GetVerify(int userid,int postid)
        {
            var result = _verifyService.GetVerify(userid, postid);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
