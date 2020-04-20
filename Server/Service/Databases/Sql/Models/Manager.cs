using Service.Databases.Sql.Core;
using System.ComponentModel.DataAnnotations;

namespace Service.Databases.Sql.Models
{
    public class Manager : IEntity
    {
        [Key]
        [DataType(DataType.EmailAddress)]
        public string Id { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ManagerViewModel
    {
        public string Id { get; set; }
        public string Password { get; set; }
    }
}
