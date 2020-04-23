using Service.Databases.Sql.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Databases.Sql.Models
{
    public class Cube : IEntity
    {
        [Key]
        public int CubeId { get; set; }

        [Key]
        [MaxLength(60)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("CubeId")]
        public CubeData CubeData { get; set; }

        [Required]
        public byte Lv { get; set; }

        [Required]
        public int Parts { get; set; }
    }

    public class CubeViewModel
    {
        public int CubeId { get; set; }
        public byte Lv { get; set; }
        public int Parts { get; set; }

        public CubeDataViewModel CubeData { get; set; }
    }
}
