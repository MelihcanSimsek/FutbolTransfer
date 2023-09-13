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
                var postWithId = _postService.GetPostId(result.Data);
                return Ok(postWithId);
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

        [HttpGet("getpostdetailbyid")]
        public IActionResult GetPostDetailById(int id)
        {
            var result = _postService.GetPostDetailById(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("gettransferpostbyid")]
        public IActionResult GetTransferPostById(int id)
        {
            var result = _postService.GetTransferPostById(id);
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
        public IActionResult GetMainPost(int id)
        {
            var result = _postService.GetMainPostWithAlgorithm(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcommentsbypostid")]
        public IActionResult GetCommentByPostId(int postId,int userId)
        {
            var result = _postService.GetCommentsByPostId(postId,userId);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("gettransferposts")]
        public IActionResult GetTransferPosts()
        {
            var result = _postService.GetTransferPosts();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("gettransferpostsbyuserid")]
        public IActionResult GetTransferPostsByUserId(int id)
        {
            var result = _postService.GetTransferPostsWithAlgorithm(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getdailytransferpost")]
        public IActionResult GetDailyTransferPost()
        {
            var result = _postService.GetDailyTransferPost();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getweeklytransferpost")]
        public IActionResult GetWeeklyTransferPost()
        {
            var result = _postService.GetWeeklyTransferPost();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getmonthlytransferpost")]
        public IActionResult GetMonthlyTransferPost()
        {
            var result = _postService.GetMonthlyTransferPost();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
