using UnityEngine;

namespace Extension
{
    public static class GameObjectExtension
    {
        public static void SetVisible(this GameObject element, bool value)
        {
            if (element == null)
                return;

            element.SetActive(value);
        }
    }
}