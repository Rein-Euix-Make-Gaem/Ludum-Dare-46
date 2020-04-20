using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class DoorPart
    {
        public Transform transform;
        public Vector3 openPosition = Vector3.up;
        public Vector3 closePosition = Vector3.zero;
    }

    [RequireComponent(typeof(Collider))]
    public class DoorController : MonoBehaviour
    {
        public DoorPart[] parts;
        public float openSpeed = 1f;
        public float closeSpeed = 1f;
        public bool autoClose = true;
        public bool open = false;

        private bool previousState;
        private bool triggering;

        public void Toggle(bool value)
        {
            open = value;
        }

        private bool IsCollidingWithPlayer(Collider collider)
        {
            return collider.gameObject.tag == "Player";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsCollidingWithPlayer(other))
            {
                open = !open;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsCollidingWithPlayer(other))
            {
                if (autoClose && open)
                {
                    open = false;
                }
            }
        }


        private void Update()
        {
            if (previousState != open) 
            {
                previousState = open;
                triggering = true;
            }

            if (triggering)
            {
                for (var i = 0; i < parts.Length; i++)
                {
                    var part = parts[i];
                    var targetPosition = open ? part.openPosition : part.closePosition;
                    var speed = open ? openSpeed : closeSpeed;
                    var direction = targetPosition - part.transform.localPosition;
                    var distance = Math.Abs(direction.magnitude);

                    if (distance >= 0.001f)
                    {
                        part.transform.localPosition += direction * speed * Time.deltaTime;
                    }
                    else
                    {
                        triggering = false;
                    }
                }

            }
        }
    }
}
