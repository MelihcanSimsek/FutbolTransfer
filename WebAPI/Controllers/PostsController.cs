using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("add")]
        public IActionResult Add(Post post)
        {
            var result = _postService.Add(post);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Post post)
        {
            var result = _postService.Delete(post);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Post post)
        {
            var result = _postService.Update(post);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _postService.GetAll();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _postService.GetById(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId(int id)
        {
            var result = _postService.GetByUserId(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getmainpost")]
        public IActionResult GetMainPost()
        {
            var result = _postService.GetMainPost();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcommentsbypostid")]
        public IActionResult GetCommentByPostId(int id)
        {
            var result = _postService.GetCommentsByPostId(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
