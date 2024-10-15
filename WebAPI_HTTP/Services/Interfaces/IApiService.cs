using WebAPI_HTTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_HTTP.Services.Interfaces
{
    public interface IApiService {

        Task<UserDTO> GetRandomUser();

        Task<List<UserDTO>> GetAllUsersAsync();

        /// <summary>
        /// Print All the users
        /// </summary>
        /// <param name="users"></param>
        void PrintAllUsers(List<UserDTO> users);

        Task<List<Post>> GetPostsAsync(int userId);

        Task<List<Post>> GetPostsAsync();
    }
}
