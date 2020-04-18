using UnityEngine;

namespace Assets.Scripts
{
    public struct InteractionEvent
    {
        public float timestamp;
        public PlayerController player;
        public RaycastHit hit;
    }

    public abstract class Interactable : MonoBehaviour
    {
        public bool canInteract = true;
        public float cooldown = 1f;

        private float nextInteraction;

        public abstract string description { get; }

        protected virtual void OnInteract(ref InteractionEvent ev)
        {
            Debug.Log("undefined interaction");
        }

        public virtual bool CanInteract(PlayerController player)
        {
            return Time.time >= nextInteraction;
        }

        public void Interact(ref InteractionEvent ev)
        {
            OnInteract(ref ev);
            nextInteraction = Time.time + cooldown;
        }
    }
}
