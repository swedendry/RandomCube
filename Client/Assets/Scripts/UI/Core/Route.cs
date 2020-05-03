using Extension;
using UnityEngine;

namespace UI
{
    public class Route : MonoBehaviour
    {
        public string path;

        public void Close(params object[] values)
        {
            gameObject?.SetVisible(false);
        }

        public void Open(params object[] values)
        {
            gameObject?.SetVisible(true);
        }

        public void Refresh()
        {
            if (gameObject.activeSelf)
                gameObject?.GetComponent<UIView>()?.Upsert();
        }
    }
}