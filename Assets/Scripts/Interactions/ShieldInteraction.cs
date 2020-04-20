namespace Assets.Scripts.Interactions
{
    using TMPro;
    using UnityEngine;

    public class ShieldInteraction : ToggleInteraction
    {
        public TMP_Text ActiveStatusText;
        public GameObject ScreenObject;

        public string activatedEvent = "";
        FMOD.Studio.EventInstance activatedSound;

        public string deActivationEvent = "";
        FMOD.Studio.EventInstance deActivationSound;

        private string inactive = "INACTIVE";
        private string active = "ACTIVE";

        private bool isCurrentlyPowered = true;

        private void Update()
        {
            if(GameManager.Instance.IsPowerActive != this.isCurrentlyPowered)
            {
                this.isCurrentlyPowered = GameManager.Instance.IsPowerActive;
                this.ScreenObject.SetActive(this.isCurrentlyPowered);
                this.canInteract = this.isCurrentlyPowered;
            }
        }

        private void Start()
        {
            this.ActiveStatusText.text = this.inactive;
            activatedSound = FMODUnity.RuntimeManager.CreateInstance(activatedEvent);
            deActivationSound = FMODUnity.RuntimeManager.CreateInstance(deActivationEvent);
        }

        protected override void OnInteract(ref InteractionEvent ev)
        {
            var active = GameManager.Instance.IsShieldActive;
            var shieldState = !active;

            this.ActiveStatusText.text = GameManager.Instance.IsShieldActive ? this.active : this.inactive;

            GameManager.Instance.SetShieldActive(shieldState);
        }
    }
}
