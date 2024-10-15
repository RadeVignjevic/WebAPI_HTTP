using WebAPI_HTTP.Models;
using WebAPI_HTTP.Services.Interfaces;

namespace WebAPI_HTTP.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient httpClient;

        public ApiService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            List<UserDTO> allUsers = await httpClient.GetFromJsonAsync<List<UserDTO>>("https://jsonplaceholder.typicode.com/users");

            return allUsers;
        }

        public async Task<UserDTO> GetRandomUser()
        {
            try
            {
                var randomUser = await httpClient.GetFromJsonAsync<UserDTO>($"https://jsonplaceholder.typicode.com/users/1");
                return randomUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                return null;
            }
        }

        public async Task<List<Post>> GetPostsAsync(int userId)
        {

            var allPosts = await httpClient.GetFromJsonAsync<List<Post>>("https://jsonplaceholder.typicode.com/posts");

            return allPosts.Where(x => x.UserId == userId).ToList();
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<Post>>("https://jsonplaceholder.typicode.com/posts");
        }

        public void PrintAllUsers(List<UserDTO> users)
        {
            foreach (var user in users) {
                Console.WriteLine($"{user.Name}");
            }
        }
    }
}
