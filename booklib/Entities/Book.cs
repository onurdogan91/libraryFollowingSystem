using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booklib.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string BookName { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Writer { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public DateTime PublishingDate { get; set; }

        public DateTime AddBookDate { get; set; } = DateTime.Now;
        //public bool Check { get; set; } = true; stok durumuna göre front-end de durum ekleriz
    }
}
