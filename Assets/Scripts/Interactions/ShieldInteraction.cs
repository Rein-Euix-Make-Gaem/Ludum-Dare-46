namespace Assets.Scripts.Interactions
{
    using TMPro;
    using UnityEngine;

    public class ShieldInteraction : ToggleInteraction
    {
        public TMP_Text ActiveStatusText;
        public GameObject ScreenObject;
        public string activatedEvent = "event:/ShieldsActivated";
        public string deActivationEvent = "event:/ShieldsDeactivated";

        private string inactive = "INACTIVE";
        private string active = "ACTIVE";
        private bool isCurrentlyPowered = true;
        private FMOD.Studio.EventInstance activatedSound;
        private FMOD.Studio.EventInstance deActivationSound;

        protected override void OnStart()
        {
            base.OnStart();

            activatedSound = FMODUnity.RuntimeManager.CreateInstance(activatedEvent);
            deActivationSound = FMODUnity.RuntimeManager.CreateInstance(deActivationEvent);

            // HACK: adjust sounds since the sound guy is busy
            activatedSound.setVolume(0.20f);
            deActivationSound.setVolume(0.20f);

            this.ActiveStatusText.text = this.inactive;
        }


        private void Update()
        {
            if (GameManager.Instance.IsPowerActive != this.isCurrentlyPowered)
            {
                this.isCurrentlyPowered = GameManager.Instance.IsPowerActive;
                this.ScreenObject.SetActive(this.isCurrentlyPowered);
                this.canInteract = this.isCurrentlyPowered;
            }
        }

        protected override void OnInteract(ref InteractionEvent ev)
        {
            base.OnInteract(ref ev);

            var active = GameManager.Instance.IsShieldActive;
            var shieldState = !active;

            if (shieldState)
            {
                ActiveStatusText.text = this.active;
                deActivationSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                activatedSound.start();
            }
            else
            {
                ActiveStatusText.text = this.inactive;
                activatedSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                deActivationSound.start();
            }

            GameManager.Instance.SetShieldActive(shieldState);
        }
    }
}
