using UnityEngine;

namespace Assets.Scripts.Interactions
{
    public class DoorInteraction : ToggleInteraction
    {
        public DoorController door;

        public DoorInteraction()
        {
            cooldown = 1f;
            objectName = "Door";
            activeTemplate = "Open Door";
            inactiveTemplate = "Close Door";
        }

        //public override bool CanInteract(PlayerController player)
        //{
        //    return true; //  !door.IsTransitioning;
        //}

        protected override void OnInteract(ref InteractionEvent ev)
        {
            base.OnInteract(ref ev);

            door.Toggle(!door.open);
        }
    }
}
