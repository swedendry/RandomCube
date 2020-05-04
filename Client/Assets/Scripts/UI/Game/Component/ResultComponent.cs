using Extension;
using UnityEngine.UI;

namespace UI.Game
{
    public class ResultComponent : UIComponent
    {
        public Text result_text;
        public Text money_text;

        public override void Upsert()
        {
            base.Upsert();

            var user = ServerInfo.MyGameUser();
            var result = user.Rank == 0 ? "WIN" : "LOSE";
            result_text.SetText(result);
            money_text.SetText(user.Money.ToString());

            //switch (ServerInfo.Room.GameMode)
            //{
            //    case GameMode.Single:
            //        {
            //            var user = ServerInfo.MyGameUser();
            //            var time = TimeSpan.FromSeconds(ServerInfo.Room.ProgressTime);
            //            result_text.SetText(string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds));
            //            money_text.SetText(user.Money.ToString());
            //        }
            //        break;
            //    case GameMode.Multi:
            //        {
            //            var user = ServerInfo.MyGameUser();
            //            var result = user.Rank == 0 ? "WIN" : "LOSE";
            //            result_text.SetText(result);
            //            money_text.SetText(user.Money.ToString());
            //        }
            //        break;
            //}
        }
    }
}