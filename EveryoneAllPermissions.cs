using Networking;
using static ModLoader;
using Pipliz;

namespace grasmanek94.EveryoneAllPermissions
{
    [ModManager]
    public static class EveryoneAllPermissions
    {
        [ModCallback(EModCallbackType.OnPlayerConnectedLate, "OnPlayerConnectedLate")]
        static void OnPlayerConnectedLate(Players.Player player)
        {
            NetworkSteam networkSteam = NetworkWrapper.Network as NetworkSteam;
            NetworkSteam.PlayerConnectionState playerConnectionState;
            if (networkSteam != null && networkSteam.TryGetState(player.ID, out playerConnectionState))
            {
                if (playerConnectionState != null)
                {
                    if (RCONHandler.Authenticateds == null)
                    {
                        RCONHandler.Authenticateds = new System.Collections.Generic.List<Pipliz.Networking.TCPWrapper>();
                    }

                    if (!RCONHandler.Authenticateds.Contains(playerConnectionState.TcpWrapper))
                    {
                        RCONHandler.Authenticateds.Add(playerConnectionState.TcpWrapper);
                    }
                }
                else
                {
                    Log.WriteWarning("EveryoneAllPermissions: invalid playerConnectionState, can be ignored on SinglePlayer");
                }
            }
            else
            {
                Log.WriteWarning("EveryoneAllPermissions: invalid NetworkSteam, can be ignored on SinglePlayer");
            }

            PermissionsManager.EnsureLoaded();

            PermissionsManager.Permission perm_dbg = new PermissionsManager.Permission("debug");
            PermissionsManager.Permission perm_localhost = new PermissionsManager.Permission("cheats.enable");

            if (PermissionsManager.HasPermission(player, perm_dbg) &&
                PermissionsManager.HasPermission(player, perm_localhost))
            {
                return;
            }

            PermissionsManager.AddGroupToUser(null, player, "peasant");
            PermissionsManager.AddGroupToUser(null, player, "localhost");
            PermissionsManager.AddGroupToUser(null, player, "king");
            PermissionsManager.AddGroupToUser(null, player, "god");
            PermissionsManager.AddGroupToUser(null, player, "emperor");
            PermissionsManager.AddGroupToUser(null, player, "godemperor");

            PermissionsManager.AddPermissionToUser(null, player, perm_dbg);
        }
    }
}
