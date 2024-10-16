namespace WebAPI_HTTP.Controllers
{
    using Azure.Core.Serialization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json;
    using WebAPI_HTTP.DBContext;
    using WebAPI_HTTP.Extensions;
    using WebAPI_HTTP.Models;
    using WebAPI_HTTP.Models.DboModels;
    using WebAPI_HTTP.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AddAppContext _addAppContext;
        private readonly ILogger<UserController> _logger;

        public UserController(AddAppContext addAppContext, ILogger<UserController> logger) {
            
            _addAppContext = addAppContext;
            _logger = logger;
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

            string userData = JsonSerializer.Serialize(getUsers);

            ///
            /// enkripcija na DATA vo random kontrolna akcija
            ////
            #region
            RSA rsa = RSA.Create();
            string privateKey = rsa.ToXmlString(true);
            System.IO.File.WriteAllText(@"C:\Users\rvignjevic\source\repos\WebAPI_HTTP\WebAPI_HTTP\privatekey.xml", privateKey);

            byte[] encripted = rsa.Encrypt(Encoding.UTF8.GetBytes(userData), RSAEncryptionPadding.Pkcs1);

            string fullPath = @"C:\Users\rvignjevic\source\repos\WebAPI_HTTP\WebAPI_HTTP\encriptedData.bin";

            System.IO.File.WriteAllBytes(fullPath, encripted);
            #endregion

            if (getUsers == null) {

                return NotFound();
            }

            return getUsers;
        }

        //add User
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDTO)
        {
            bool validationCheck = ModelState.IsValid;

            ///
            /// Dekripcija na DATA vo random kontrolna akcija
            ////
            #region
            RSA rsa = RSA.Create();

            string privateKey = System.IO.File.ReadAllText(@"C:\Users\rvignjevic\source\repos\WebAPI_HTTP\WebAPI_HTTP\privatekey.xml");

            rsa.FromXmlString(privateKey);

            string fullPath = @"C:\Users\rvignjevic\source\repos\WebAPI_HTTP\WebAPI_HTTP\encriptedData.bin";

            byte[] enrciptedData = System.IO.File.ReadAllBytes(fullPath);

            byte[] decripted = rsa.Decrypt(enrciptedData, RSAEncryptionPadding.Pkcs1);

            var decriptedData = Encoding.UTF8.GetString(decripted);
            #endregion

            var validationError = this.ValidateModel();

            if (validationError != null)
            {
                return validationError;
            }

            try
            {
                DboUser dboUser = new DboUser
                {
                    Email = userDTO.Email,
                    Name = userDTO.Name,
                    UserDescription = userDTO.UserDescription,
                    UserName = userDTO.UserName
                };
                try
                {
                    _logger.LogCustomInformation("test message34234");

                }
                catch (Exception ex)
                {

                    throw ex;
                }

                await _addAppContext.DboUser.AddAsync(dboUser);

                await _addAppContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "exception happend");
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
            using(var transaction = await _addAppContext.Database.BeginTransactionAsync())
            {
                try
                {
                   await _addAppContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.DboUser ON");

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

                    await _addAppContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.DboUser OFF");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }

            }

            return Ok();
        }

    }
}
