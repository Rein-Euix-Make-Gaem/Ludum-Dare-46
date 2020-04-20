namespace Assets.Scripts.Interactions
{
    using TMPro;

    public class ShieldInteraction : ToggleInteraction
    {
        public TMP_Text ActiveStatusText;
        public string activatedEvent = "event:/ShieldsActivated";
        public string deActivationEvent = "event:/ShieldsDeactivated";

        FMOD.Studio.EventInstance activatedSound;
        FMOD.Studio.EventInstance deActivationSound;

        protected override void OnStart()
        {
            base.OnStart();
            activatedSound = FMODUnity.RuntimeManager.CreateInstance(activatedEvent);
            activatedSound = FMODUnity.RuntimeManager.CreateInstance(deActivationEvent);
        }

        protected override void OnInteract(ref InteractionEvent ev)
        {
            var active = GameManager.Instance.IsShieldActive;
            var shieldState = !active;

            if (shieldState)
            {
                ActiveStatusText.text = "ACTIVE";
                deActivationSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                activatedSound.start();
            }
            else
            {
                ActiveStatusText.text = "INACTIVE";
                activatedSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                deActivationSound.start();
            }

            GameManager.Instance.SetShieldActive(shieldState);
        }
    }
}
