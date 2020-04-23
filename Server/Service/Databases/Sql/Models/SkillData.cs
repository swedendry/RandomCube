using Service.Databases.Sql.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Databases.Sql.Models
{
    public class SkillData : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SkillId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Percent { get; set; }

        [Required]
        public float Duration { get; set; }
    }

    public class SkillDataViewModel
    {
        public string Name { get; set; }
        public float Percent { get; set; }
        public float Duration { get; set; }
    }
}
