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

        public float MaxCharge;
        public float RechargeSpeed;
        public float DepletionSpeed;
        public float CurrentCharge;

        public Doozy.Engine.Progress.Progressor Progressor;

        public Color ChargingColor;
        public Color ReadyColor;
        public Color ActiveColor;

        private string charging = "CHARGING";
        private string ready = "READY";
        private string active = "ACTIVE";
        private bool isCurrentlyPowered = true;

        private FMOD.Studio.EventInstance activatedSound;
        private FMOD.Studio.EventInstance deActivationSound;

        protected override void OnStart()
        {
            base.OnStart();

            this.CurrentCharge = 0;

            activatedSound = FMODUnity.RuntimeManager.CreateInstance(activatedEvent);
            deActivationSound = FMODUnity.RuntimeManager.CreateInstance(deActivationEvent);

            // HACK: adjust sounds since the sound guy is busy
            activatedSound.setVolume(0.20f);
            deActivationSound.setVolume(0.20f);

            this.ActiveStatusText.text = this.charging;
        }

        private void Update()
        {
            if (GameManager.Instance.IsPowerActive != this.isCurrentlyPowered)
            {
                this.isCurrentlyPowered = GameManager.Instance.IsPowerActive;
                this.ScreenObject.SetActive(this.isCurrentlyPowered);
                base.canInteract = this.isCurrentlyPowered;
            }

            if (this.isCurrentlyPowered && !GameManager.Instance.IsShieldActive && this.CurrentCharge < this.MaxCharge)
            {
                this.CurrentCharge += Time.deltaTime * this.RechargeSpeed;
                base.canInteract = false;
            }
            else if(this.isCurrentlyPowered && !GameManager.Instance.IsShieldActive && this.CurrentCharge >= this.MaxCharge)
            {
                base.canInteract = true;
                this.ActiveStatusText.text = this.ready;
            }
            else if(this.isCurrentlyPowered && GameManager.Instance.IsShieldActive)
            {
                if (this.CurrentCharge > 0)
                {
                    this.CurrentCharge -= Time.deltaTime * this.DepletionSpeed;
                }
                else
                {
                    this.canInteract = false;
                    this.ActiveStatusText.text = this.charging;
                    GameManager.Instance.SetShieldActive(false);
                }
            }

            this.Progressor.SetValue(this.CurrentCharge);
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
                ActiveStatusText.text = this.charging;
                activatedSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                deActivationSound.start();
            }

            GameManager.Instance.SetShieldActive(shieldState);
        }
    }
}
