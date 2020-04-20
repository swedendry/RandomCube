using System.Collections.Generic;
using System.Linq;

namespace UI
{
    public static class RouterExtension
    {
        public static List<string> ToPaths(this string path)
        {
            var paths = new List<string>();

            while (true)
            {
                paths.Add(path);

                int location = path.LastIndexOf("/");
                if (location < 0)
                    break;

                path = path.Substring(0, location);
            }

            return paths;
        }
    }

    public static class Router
    {
        private static readonly List<Route> routes = new List<Route>();

        public static void Register(Route route)
        {
            routes.Add(route);
        }

        public static void UnRegister(Route route)
        {
            routes.Remove(route);
        }

        public static void CloseAndOpen(params string[] paths)
        {
            Close();
            Open(paths);
        }

        public static void Open(params string[] paths)
        {
            var allPaths = paths.ToList().SelectMany(x => x.ToPaths()).Distinct().ToList();
            allPaths.ForEach(x => routes.Find(c => c.path == x).Open());
        }

        public static void Close(params string[] paths)
        {
            if (paths.Length <= 0)
            {   //전체 끄기
                routes.ForEach(x => x.Close());
                return;
            }

            var allPaths = paths.Distinct().ToList();
            allPaths.ForEach(x => routes.Find(c => c.path == x).Close());
        }
    }
}