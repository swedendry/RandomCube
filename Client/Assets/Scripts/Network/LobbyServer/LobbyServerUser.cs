using Network;
using Network.LobbyServer;

public partial class LobbyServer
{
    public Payloader<UserViewModel> GetUser(string id)
    {
        var url = string.Format("api/users/{0}", id);

        return http.Get<UserViewModel>(GetUri(url)).Callback(
            success: (data) =>
            {
                ServerInfo.User = data;
            });
    }

    public Payloader<UserViewModel> CreateUser(CreateUserBody body)
    {
        var url = string.Format("api/users");

        return http.Post<UserViewModel>(GetUri(url), body).Callback(
            success: (data) =>
            {
                ServerInfo.User = data;
            });
    }

    public Payloader<int[]> UpdateEntry(string id, int[] slots)
    {
        var url = string.Format("api/users/{0}/entries", id);

        return http.Put<int[]>(GetUri(url), slots).Callback(
            success: (data) =>
            {
                ServerInfo.User.Entry.Slots = slots;
            });
    }

    public Payloader<int> UpdateMoney(string id, int money)
    {
        var url = string.Format("api/users/{0}/money", id);

        return http.Put<int>(GetUri(url), money).Callback(
            success: (data) =>
            {
                ServerInfo.User.Money = data;
            });
    }
}
