using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booklib.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Zorunlu alan doldurunuz")]
        [StringLength(20,ErrorMessage ="En fazla 20 karekterli kullanıcı adı alınabilir")]
        public string UserName { get; set; }

        public string? FullName { get; set; }

        [Required(ErrorMessage = "Zorunlu alan doldurunuz")]
        [MaxLength(30, ErrorMessage = "En fazla 30 karekterli şifre alınabilir")]
        [MinLength(6, ErrorMessage = "En az 6 karekterli şifre alınabilir")]
        public string Password { get; set; }

        public string Role { get; set; } = "user";

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public bool Penalty { get; set; } = false;
    }
}
