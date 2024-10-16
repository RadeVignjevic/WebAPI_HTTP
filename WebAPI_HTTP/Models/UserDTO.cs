using System.ComponentModel.DataAnnotations;
using WebAPI_HTTP.CustomAttributes;
using WebAPI_HTTP.Models.Base_Models;

namespace WebAPI_HTTP.Models
{
    public class UserDTO 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [UserName("Vnesete validan name koj ima pomegju 3 i 13 karakteri")]
        public string UserName { get; set; }

        [ValidEmail("Vnesete validan email addresa")]
        public string Email { get; set; }

        public string UserDescription { get; set; }
    }
}
