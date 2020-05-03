using System;
using UnityEngine.UI;

namespace UI.Game
{
    public class TimeConponent : UIComponent
    {
        public Text time_text;

        private void Update()
        {
            var time = TimeSpan.FromSeconds(ServerInfo.Room.ProgressTime);
            time_text.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
        }
    }
}
