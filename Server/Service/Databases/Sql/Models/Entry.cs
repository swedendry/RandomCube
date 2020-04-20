using Service.Databases.Sql.Core;
using Service.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Service.Databases.Sql.Models
{
    public class Entry : IEntity
    {
        [Key]
        [MaxLength(60)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [NotMapped]
        public int[] Slots { get; set; }

        [Required]
        [Column("Slots")]
        public string SlotsJoin
        {
            get => Slots.Join(',');
            set => Slots = value.Split<int>(',');
        }
    }

    public class EntryViewModel
    {
        public int[] Slots { get; set; }
    }
}
