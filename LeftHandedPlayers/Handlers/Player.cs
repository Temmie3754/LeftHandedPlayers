using Exiled.Events.EventArgs;

namespace LeftHandedPlayers.Handlers
{
    class Player
    {
        public void OnVerified(VerifiedEventArgs ev)
        {
            // Checks if they are on the list of left handed players
            if (LeftHandedPlayers.Instance.LeftHandList.Contains(ev.Player.UserId))
            {
                // Sets them to be left handed
                ev.Player.Scale = new UnityEngine.Vector3(-1, 1, 1);
            }
        }
    }
}
