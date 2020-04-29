using Extension;
using System.Linq;
using UnityEngine.UI;

namespace UI.Game
{
    public class ResultComponent : UIComponent
    {
        public Text result_text;

        public override void Upsert()
        {
            base.Upsert();

            var users = ServerInfo.GameUsers.OrderBy(x => x.Rank).ToList();
            var isWin = users.FirstOrDefault()?.Id == ServerInfo.User.Id;
            var result = isWin ? "WIN" : "LOSE";
            result_text.SetText(result);
        }
    }
}