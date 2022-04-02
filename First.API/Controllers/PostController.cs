using First.App.Business.Abstract;
using First.App.Business.DTOs;
using First.App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace First.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [Route("Posts")]
        [HttpGet]
        public IActionResult Get()
        {
            var posts = postService.GetAllPost().Select(x => new PostDto
            {
                UserId = x.UserId,
                Title = x.Title,
                Body = x.Body 
            });
            return Ok();
        }

        public IActionResult Post([FromBody] PostDto model)
        {
            postService.AddPost(new Post
            {
                UserId = model.UserId,
                Title = model.Title,
                Body = model.Body,
                CreatedBy = "İncİ",
                CreatedAt = System.DateTime.Now,
                IsDeleted = false,
               
            });
            return Ok();
                
        }
    }
}
