using UnityEngine;

namespace Assets.Scripts.Interactions
{
    public class DoorInteraction : ToggleInteraction
    {
        public GameObject door;
        public float targetHeight = 2.25f;

        private Vector3 basePosition;

        public override string description => "toggle door";

        private void Start()
        {
            basePosition = door.transform.position;
        }

        void Update()
        {
            var height = toggled ? targetHeight : 0;
            var offset = new Vector3(0, height, 0);

            door.transform.position = Vector3.Lerp(
                door.transform.position, basePosition + offset, Time.deltaTime * 2);
        }
    }
}
