namespace Assets.Scripts.Interactions
{
    using TMPro;

    public class ShieldInteraction : ToggleInteraction
    {
        public TMP_Text ActiveStatusText;

        private string inactive = "INACTIVE";
        private string active = "ACTIVE";

        protected override void OnInteract(ref InteractionEvent ev)
        {
            var active = GameManager.Instance.IsShieldActive;
            var shieldState = !active;

            this.ActiveStatusText.text = GameManager.Instance.IsShieldActive ? this.active : this.inactive;

            GameManager.Instance.SetShieldActive(shieldState);
        }
    }
}
