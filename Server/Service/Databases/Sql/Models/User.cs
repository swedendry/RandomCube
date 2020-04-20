using Service.Databases.Sql.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.Databases.Sql.Models
{
    public class User : IEntity
    {
        [Key]
        [MaxLength(60)]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual Entry Entry { get; set; } = new Entry();
        public virtual IList<Cube> Cubes { get; set; } = new List<Cube>();
    }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public EntryViewModel Entry { get; set; }
        public List<CubeViewModel> Cubes { get; set; }
    }
}
