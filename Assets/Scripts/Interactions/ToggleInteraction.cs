using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactions
{
    public class ToggleInteraction: Interactable
    {
        public string objectName;
        public string activeTemplate;
        public string inactiveTemplate;
        public bool toggled = false;

        public override string description => toggled
            ? $"activate {objectName ?? string.Empty}"
            : $"deactivate {objectName ?? string.Empty}";

        protected override void OnInteract(ref InteractionEvent ev)
        {
            toggled = !toggled;
        }

    }
}
