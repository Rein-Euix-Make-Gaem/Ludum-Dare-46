using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interactions
{
    public class ShieldInteraction : ToggleInteraction
    {
        protected override void OnInteract(ref InteractionEvent ev)
        {
            var active = GameManager.Instance.IsShieldActive;
            var shieldState = !active;

            GameManager.Instance.SetShieldActive(shieldState);
        }
    }
}
