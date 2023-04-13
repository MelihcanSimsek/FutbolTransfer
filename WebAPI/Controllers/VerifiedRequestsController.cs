using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifiedRequestsController : ControllerBase
    {
        IVerifiedRequestService _request;

        public VerifiedRequestsController(IVerifiedRequestService request)
        {
            _request = request;
        }

        [HttpPost("add")]
        public IActionResult Add(VerifiedRequest request)
        {
            var result = _request.Add(request);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(VerifiedRequest request)
        {
            var result = _request.Delete(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(VerifiedRequest request)
        {
            var result = _request.Update(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("getall")]
        public IActionResult GetAll()
        {
            var result = _request.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("getbyrequestid")]
        public IActionResult GetByRequestId(int id)
        {
            var result = _request.GetByRequestId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
