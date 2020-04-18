using Assets.Scripts;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    public float interactionDistance = 2f;
    public LayerMask interactableMask;

    public PlayerController player;
    public TMPro.TextMeshProUGUI interactionText;
    public RadialProgress holdProgressBar;

    private float accumulatedHoldTime;

    void Start()
    {
        interactableMask = LayerMask.NameToLayer("Interactable");
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * interactionDistance);

        // update ui

        interactionText.text = string.Empty;


        if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out var hitInfo, interactionDistance, interactableMask.value))
        {
            var interactable = hitInfo.collider.gameObject.GetComponent<Interactable>();

            if (interactable != null && interactable.CanInteract(player))
            {
                interactionText.text = interactable.description;

                if (interactable.holdTime > 0)
                {
                    var progress = Mathf.Clamp(accumulatedHoldTime / interactable.holdTime, 0, 1);
                    holdProgressBar.progress = progress;
                }

                if (Input.GetAxis("Fire1") > 0f)
                {
                    accumulatedHoldTime += Time.deltaTime;

                    if (CanInteract(interactable))
                    {
                        var interaction = new InteractionEvent
                        {
                            timestamp = Time.time,
                            hit = hitInfo,
                            player = player,
                        };

                        interactable.Interact(ref interaction);
                        accumulatedHoldTime = 0;
                        holdProgressBar.progress = 0;
                    }
                }
                else
                {
                    accumulatedHoldTime = 0;
                    holdProgressBar.progress = 0;
                }
            }
        }
    }

    private bool CanInteract(Interactable interactable)
    {
        if (!interactable.CanInteract(player))
        {
            return false;
        }

        if (interactable.holdTime > 0f && accumulatedHoldTime < interactable.holdTime)
        {
            return false;
        }

        return true;
    }
}
