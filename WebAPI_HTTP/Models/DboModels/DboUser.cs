
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace WebAPI_HTTP.Models.DboModels
{
    public class DboUser
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        public string? Email { get; set; }

        public string UserDescription { get; set; }


        /// <summary>
        /// We need this that one user is connected with several posts
        /// </summary>
        public ICollection<DboPost> Posts { get; set; }    
    }
}
