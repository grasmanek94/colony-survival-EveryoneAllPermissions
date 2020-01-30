using Networking;

namespace grasmanek94.EveryoneAllPermissions
{
    [ModLoader.ModManager]
    public static class EveryoneAllPermissions
    {
        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnPlayerConnectedLate, "OnPlayerConnectedLate")]
        static void OnPlayerConnectedLate(Players.Player player)
        {
            PermissionsManager.EnsureLoaded();
            PermissionsManager.AddGroupToUser(null, player, "peasant");
            PermissionsManager.AddGroupToUser(null, player, "localhost");
            PermissionsManager.AddGroupToUser(null, player, "king");
            PermissionsManager.AddGroupToUser(null, player, "god");
            PermissionsManager.AddGroupToUser(null, player, "emperor");
            PermissionsManager.AddGroupToUser(null, player, "godemperor");

            
            NetworkSteam networkSteam = NetworkWrapper.Network as NetworkSteam;
            NetworkSteam.PlayerConnectionState playerConnectionState;
            if (networkSteam.TryGetState(player.ID, out playerConnectionState))
            {
                if(RCONHandler.Authenticateds == null)
                {
                    RCONHandler.Authenticateds = new System.Collections.Generic.List<Pipliz.Networking.TCPWrapper>();
                }

                if (!RCONHandler.Authenticateds.Contains(playerConnectionState.TcpWrapper))
                {
                    RCONHandler.Authenticateds.Add(playerConnectionState.TcpWrapper);
                }
            };

            PermissionsManager.Permission perm = new PermissionsManager.Permission("debug");

            PermissionsManager.AddPermissionToUser(null, player, perm);
        }
    }
}
