using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubsBack.Entities
{
    [Table("Clubs")]
    public class Club
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        public List<User> Users { get; set; }
    }
}