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
        public float Power { get; set; }

        [Required]
        public float Speed { get; set; }
    }

    public class CubeDataViewModel
    {
        public string Name { get; set; }
        public float Power { get; set; }
        public float Speed { get; set; }
    }
}
