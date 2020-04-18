using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] public bool enabled = false;
        [SerializeField] public string description = string.Empty;
    }
}
