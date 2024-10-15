namespace WebAPI_HTTP.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using WebAPI_HTTP.DBContext;
    using WebAPI_HTTP.Models;
    using WebAPI_HTTP.Models.DboModels;
    using WebAPI_HTTP.Services;

    [Route("api/[controller]")]
    [ApiController] 
    public class UserController : ControllerBase
    {
        private readonly AddAppContext _addAppContext;

        public UserController(AddAppContext addAppContext) {
            
            _addAppContext = addAppContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DboUser>>> GetUsers()
        {
            var getUsers = await _addAppContext.DboUser.ToListAsync();

            return getUsers;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DboUser>> GetUser(int id)
        {
            var getUsers = await _addAppContext.DboUser.FirstOrDefaultAsync(x=>x.UserId == id);

            if (getUsers == null) {

                return NotFound();
            }

            return getUsers;
        }

        //add User
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDTO)
        {
            try
            {
                DboUser dboUser = new DboUser
                {
                    Email = userDTO.Email,
                    Name = userDTO.Name,
                    UserDescription = userDTO.UserDescription,
                    UserName = userDTO.UserName
                };

                await _addAppContext.DboUser.AddAsync(dboUser);

                await _addAppContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DboUser>> PutUser(int id, UserDTO userDTO)
        {

            var getUser = await _addAppContext.DboUser.FindAsync(id);

            if (getUser != null && getUser.UserId == id)
            {
                getUser.UserDescription = userDTO.UserDescription;
                getUser.Email = userDTO.Email;
                await _addAppContext.SaveChangesAsync();
            }
            else {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _addAppContext.DboUser.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _addAppContext.DboUser.Remove(user);

            await _addAppContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("/deleteAllWithPost")]
        public async Task<ActionResult> DeleteAllusersWithPosts()
        {
            List<DboUser> users = await _addAppContext.DboUser.ToListAsync();

            foreach (DboUser user in users)
            {
                _addAppContext.DboUser.Remove(user);

                await _addAppContext.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost("/addUsersFromJsonPlaceholder")]
        public async Task<ActionResult<UserDTO>> AddUsersFromJsonPlaceholder()
        {
            try
            {
                ApiService _apiService = new ApiService();

                List<UserDTO> users = await _apiService.GetAllUsersAsync();

                foreach (UserDTO user in users)
                {
                    DboUser dboUser = new DboUser
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                        UserDescription = "Fixed Description",
                        UserName = user.UserName
                    };
                    await _addAppContext.AddAsync(dboUser);
                    await _addAppContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return Ok();
        }

    }
}
