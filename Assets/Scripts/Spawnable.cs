using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Spawnable : MonoBehaviour
    {
        public virtual bool CanSpawn() => true;
        public virtual void Spawn() { }
    }
}
