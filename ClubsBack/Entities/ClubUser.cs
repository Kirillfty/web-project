using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubsBack.Entities
{
    [Table("ClubsUsers")]
    public class ClubUser
    {
        [Key]
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int ClubId { get; set; }
        public required bool IsAdmin { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [ForeignKey(nameof(ClubId))]
        public Club Club { get; set; }
    }
}
