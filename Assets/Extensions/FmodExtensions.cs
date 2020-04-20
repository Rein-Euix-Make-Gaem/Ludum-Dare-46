using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Extensions
{
    public static class FmodExtensions
    {
        public static FMOD.VECTOR ToFModVector(this Vector3 vec)
        {
            return new FMOD.VECTOR
            {
                x = vec.x,
                y = vec.y,
                z = vec.z
            };
        }

        public static FMOD.ATTRIBUTES_3D ToFModAttributes(this Transform transform)
        {
            return new FMOD.ATTRIBUTES_3D
            {
                forward = transform.forward.ToFModVector(),
                position = transform.position.ToFModVector(),
                up = transform.up.ToFModVector(),
                velocity = Vector3.zero.ToFModVector()
            };
        }
    }
}
