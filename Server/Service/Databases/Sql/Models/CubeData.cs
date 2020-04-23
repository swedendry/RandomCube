using Service.Databases.Sql.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Databases.Sql.Models
{
    public class CubeData : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CubeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float AD { get; set; }   //attack damage

        [Required]
        public float AS { get; set; }   //attack speed

        [Required]
        public int SkillId { get; set; }
    }

    public class CubeDataViewModel
    {
        public string Name { get; set; }
        public float AD { get; set; } //attack damage
        public float AS { get; set; } //attack speed
        public int SkillId { get; set; }
    }
}
