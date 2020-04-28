using AutoMapper;
using Network.GameServer;
using Network.LobbyServer;

public static class MapperExtension
{
    public static TDestination Map<TDestination>(this object source)
    {
        return MapperFactory.Mapper.Map<TDestination>(source);
    }

    //public static List<TDestination> MapList<TDestination>(this object source)
    //{
    //    var enumerable = source as IEnumerable;
    //    var sources = enumerable.Cast<object>();

    //    return sources.Select(x => x.Map<TDestination>()).ToList();
    //}
}

public static class MapperFactory
{
    public static IMapper Mapper { get; set; }
    public static MapperConfiguration config { get; set; }

    public static void Register()
    {
        config = new MapperConfiguration(cfg =>
        {
            CreateMap<UserViewModel>(cfg);
            CreateMap<CubeDataXml.Data, CubeDataViewModel>(cfg);

            CreateMap<RoomUser, GameUser>(cfg);
        });

        Mapper = config.CreateMapper();
    }

    private static void CreateMap<TSource>(IMapperConfigurationExpression config)
    {
        config.CreateMap<TSource, TSource>();
    }

    private static void CreateMap<TSource, TDestination>(IMapperConfigurationExpression config)
    {
        config.CreateMap<TSource, TDestination>();
        config.CreateMap<TDestination, TSource>();
    }
}