using UnityEngine;

namespace Assets.Scripts
{
    public struct InteractionEvent
    {
        public Interactable sender;
        public bool proxied;
        public float timestamp;
        public PlayerController player;
        public RaycastHit hit;
    }

    public abstract class Interactable : MonoBehaviour
    {
        public bool canInteract = true;
        public float cooldown = 1f;
        public Interactable[] proxies;
        public float holdTime;

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

            if (proxies != null)
            {
                foreach (var proxy in proxies)
                {
                    var proxyEvent = new InteractionEvent
                    {
                        proxied = true,
                        sender = this,
                        hit = ev.hit,
                        player = ev.player,
                        timestamp = ev.timestamp
                    };

                    if (proxy.CanInteract(ev.player))
                    {
                        proxy.Interact(ref proxyEvent);
                    }
                }
            }
        }
    }
}
