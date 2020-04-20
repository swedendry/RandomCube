using UnityEngine.UI;

namespace Extension
{
    public static class TextExtension
    {
        public static void SetText(this Text element, string text)
        {
            if (element == null)
                return;

            element.text = text;
        }
    }
}
