using Assets.Scripts;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    public float interactionDistance = 2f;
    public LayerMask interactableMask;

    public PlayerController player;
    public TMPro.TextMeshProUGUI interactionText;

    void Start()
    {
        interactableMask = LayerMask.NameToLayer("Interactable");
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * interactionDistance);

        interactionText.text = string.Empty;

        if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out var hitInfo, interactionDistance, interactableMask.value))
        {
            var interactable = hitInfo.collider.gameObject.GetComponent<Interactable>();

            if (interactable != null && interactable.CanInteract(player))
            {
                interactionText.text = interactable.description;

                if (Input.GetAxis("Fire1") >= 1.0)
                {
                    var interaction = new InteractionEvent
                    {
                        timestamp = Time.time,
                        hit = hitInfo,
                        player = player
                    };

                    interactable.Interact(ref interaction);
                }
            }
        }
    }
}
