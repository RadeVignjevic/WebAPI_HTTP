namespace WebAPI_HTTP.Models.DboModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DboPost
    {
        [Key]
        public int PostId { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Body { get; set; }

        /// <summary>
        /// Ni treba kako navigaciska klasa za ForeingKey od User
        /// </summary>
        public DboUser User { get; set; }
    }
}
