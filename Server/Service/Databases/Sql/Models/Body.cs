namespace Service.Databases.Sql.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class CreateUserBody
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Cube
    /// </summary>
    public class UpdateCubeLvBody
    {
        public CubeViewModel Cube { get; set; }
        public int Money { get; set; }
    }
}
