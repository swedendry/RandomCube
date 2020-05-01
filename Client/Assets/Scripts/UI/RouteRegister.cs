using System.Linq;
using UnityEngine;

namespace UI
{
    public class RouteRegister : MonoBehaviour
    {
        private void Awake()
        {
            var childs = GetComponentsInChildren<Route>(true).ToList();

            childs.ForEach(x =>
            {
                var parents = x.GetComponentsInParent<Route>(true).ToList();
                parents.Reverse();
                x.path = string.Join("/", parents.Select(c => c.name));
                x.Close();

                Router.Register(x);
            });
        }

        private void OnDestroy()
        {
            Router.UnRegister();
        }
    }
}