using System;
using CommandSystem;
using Player = Exiled.API.Features.Player;
using RemoteAdmin;

namespace LeftHandedPlayers.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class LeftHanded : ICommand
    {
        public string Command { get; } = "lefthanded";

        public string[] Aliases { get; } = { "left", "lefthand" };

        public string Description { get; } = "Makes you left handed";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            // Converts the command sender to a player object
            Player player = Player.Get((PlayerCommandSender)sender);

            // Ensures they are not left handed before applying the change
            if (player.Scale.x > 0)
            {
                // Applies the left hand change
                player.Scale = UnityEngine.Vector3.Scale(player.Scale, new UnityEngine.Vector3(-1, 1, 1));

                // Checks the player does not have DNT on before adding them to the saved list of left handed players
                if (!player.DoNotTrack) LeftHandedPlayers.Instance.LeftHandList.Add(player.UserId);

                // Removes them from the removal list if on it
                LeftHandedPlayers.Instance.ToRemoveList.Remove(player.UserId);

                // Informs the player that the command worked
                response = "You are now left handed";
                return true;
            }
            else
            {
                // Makes the player no longer appear left handed
                player.Scale = UnityEngine.Vector3.Scale(player.Scale, new UnityEngine.Vector3(-1, 1, 1));

                // Adds them to the removal list if they don't have DNT on and removes from left handed list if on it
                if (!player.DoNotTrack) LeftHandedPlayers.Instance.ToRemoveList.Add(player.UserId);
                LeftHandedPlayers.Instance.LeftHandList.Remove(player.UserId);

                // Informs the player that the command worked
                response = "You are no longer left handed";
                return true;
            }
        }
    }
}
