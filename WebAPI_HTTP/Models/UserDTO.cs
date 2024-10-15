using System.ComponentModel.DataAnnotations;
using WebAPI_HTTP.Models.Base_Models;

namespace WebAPI_HTTP.Models
{
    public class UserDTO 
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public string UserName { get; set; }

        public string? Email { get; set; }

        public string UserDescription { get; set; }
    }
}
