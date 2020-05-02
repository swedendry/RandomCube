using Network.LobbyServer;

namespace UI.Cube
{
    public class MainView : UIView
    {
        private MaterialContainer materialContainer;

        protected override void Awake()
        {
            base.Awake();

            materialContainer = GetUIContainer<MaterialContainer>();
            materialContainer.OnEventProps = MaterialEvent;
        }

        public override void Upsert()
        {
            materialContainer?.Upsert();
        }

        private void MaterialEvent(bool isSelected, Props<CubeViewModel> props)
        {
            LobbyServer.sInstance?.UpdateCubeLv(ServerInfo.User.Id, props.data.CubeId).Callback(
            success: (data) =>
            {
                materialContainer.Upsert();
            });
        }
    }
}
