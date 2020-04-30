using Network.LobbyServer;

namespace UI.Cube
{
    public class MainView : UIView
    {
        public MaterialView materialView;

        protected override void Awake()
        {
            materialView.OnEventProps = MaterialEvent;
        }

        public override void Upsert()
        {

        }

        private void MaterialEvent(bool isSelected, Props<CubeViewModel> props)
        {

        }
    }
}
