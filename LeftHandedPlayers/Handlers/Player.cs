using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using InventorySystem.Items.Firearms.Attachments;

namespace LeftHandedPlayers.Handlers
{
    class Player
    {
        public void OnChangingItem(ChangingItemEventArgs ev)
        {
            if (ev.NewItem.Type != ItemType.GunE11SR) return; 

            if (ev.NewItem.Scale[0] < 0) 
            {
                if (LeftHandedPlayers.Instance.ToRemoveList.Contains(ev.Player.UserId)) 
                {
                    ev.NewItem.Scale.Scale(new UnityEngine.Vector3(-1, 1, 1));
                }
                return;
            }

            if (!LeftHandedPlayers.Instance.LeftHandList.Contains(ev.Player.UserId)) return;
             
            Firearm firearm = (Firearm)ev.NewItem;

            foreach(FirearmAttachment st in firearm.Attachments)
            {
                if ((st.Name == AttachmentNameTranslation.NightVisionSight || st.Name == AttachmentNameTranslation.ScopeSight) && st.IsEnabled)
                {
                    firearm.Scale.Scale(new UnityEngine.Vector3(-1, 1, 1));
                    return;
                }
            }
        }
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
