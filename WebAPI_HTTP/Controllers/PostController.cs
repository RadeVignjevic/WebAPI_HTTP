using Microsoft.AspNetCore.Mvc;
using WebAPI_HTTP.DBContext;
using WebAPI_HTTP.Models.DboModels;
using WebAPI_HTTP.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI_HTTP.Services.Interfaces;
using WebAPI_HTTP.Services;

namespace WebAPI_HTTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly AddAppContext _addAppContext;

        public PostController(AddAppContext addAppContext)
        {
            _addAppContext = addAppContext;
        }

        [HttpPost]
        public async Task<ActionResult> AddPostsForUser()
        {
            try
            {
                ApiService _apiService = new ApiService();

                List<DboUser> dboUser = await  _addAppContext.DboUser.ToListAsync();
                 
                var posts = await _apiService.GetPostsAsync();

                Parallel.ForEach(dboUser, user =>
                {
                    using (var context = new AddAppContext())
                    {
                        foreach (var post in posts)
                        {
                            if(post.UserId == user.UserId)
                            {
                                DboPost dboPost = new DboPost
                                {
                                    Body = post.Body,
                                    Title = post.Title,
                                    UserId = user.UserId
                                };

                                context.DboPost.Add(dboPost);
                            }
                        }
                        context.SaveChanges();  
                    }
                });


            }
            catch (Exception ex)
            {
                throw;
            }

            return Ok();
        }
    }
}
