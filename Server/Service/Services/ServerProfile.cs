using AutoMapper;
using Service.Databases.Sql.Models;
using Service.Xmls;

namespace Service.Services
{
    public class ServerProfile : Profile
    {
        public ServerProfile()
        {
            CreateMap<Manager, ManagerViewModel>();

            CreateMap<CubeDataXml.Data, CubeData>();
            CreateMap<CubeData, CubeDataViewModel>();

            CreateMap<User, UserViewModel>();
            CreateMap<Entry, EntryViewModel>();
            CreateMap<Cube, CubeViewModel>();
        }
    }
}
