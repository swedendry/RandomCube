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

        private void MaterialEvent(bool isSelected, Props<CubeViewModel> props)
        {
            LobbyServer.sInstance?.UpdateCubeLv(ServerInfo.User.Id, props.data.CubeId).Callback(
            success: (data) =>
            {
                materialView.Upsert();
            });
        }
    }
}
