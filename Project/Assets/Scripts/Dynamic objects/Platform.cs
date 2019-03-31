using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInput;

namespace Dynamic_objects
{
    public class Platform : EmissiveObject, IActivateObject
    {
        [SerializeField] private Transform notActiveObject;
        [SerializeField] private Transform activeObject;
        [SerializeField] private GameObject platformObject;
        [SerializeField] private float speed = 1f;

        public float LastBadActionTime { get; private set; }
        public Player LastPlayer { get; private set; }

        private bool state = false;
        private float lerpParam = 0;

        public void Activate(Player player, bool activeState)
        {
            state = activeState;

            if (activeState ^ PlayerManager.isBadRoute)
            {
                LastBadActionTime = Time.time;
                LastPlayer = player;
            }

            SetEmission(activeState, player.PlayerColor);
        }

        private void Start()
        {
            Init(platformObject);

            if (PlayerManager.isBadRoute)
                platformObject.transform.position = activeObject.position;
            else
                platformObject.transform.position = notActiveObject.position;
        }

        private void Update()
        {
            if (state ^ PlayerManager.isBadRoute)
            {
                if (lerpParam < 1)
                {
                    lerpParam += speed * Time.deltaTime;
                    lerpParam = Mathf.Min(lerpParam, 1);

                    platformObject.transform.position = Vector3.Slerp(notActiveObject.position, activeObject.position, lerpParam);
                }
            } else
            {
                if (lerpParam > 0)
                {
                    lerpParam -= speed * Time.deltaTime;
                    lerpParam = Mathf.Max(lerpParam, 0);

                    platformObject.transform.position = Vector3.Slerp(notActiveObject.position, activeObject.position, lerpParam);
                }
            }
        }
    }
}
