using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInput;

namespace Dynamic_objects
{
    public class Hammer : EmissiveObject, IActivateObject
    {
        [SerializeField] private Transform notActiveObject;
        [SerializeField] private Transform activeObject;
        [SerializeField] private GameObject hammerObject;
        [SerializeField] private float speed = 1f;

        private bool state = false;
        private float lerpParam = 0;

        public void Activate(Player player, bool activeState)
        {
            state = activeState;

            SetEmission(activeState, player.PlayerColor);
        }

        private void Start()
        {
            if (hammerObject == null)
            {
                hammerObject = gameObject;
            }

            hammerObject.transform.position = notActiveObject.position;
            hammerObject.transform.rotation = notActiveObject.rotation;
            Init(hammerObject);
        }

        private void Update()
        {
            if (state)
            {
                if (lerpParam < 1)
                {
                    lerpParam += speed * Time.deltaTime;
                    lerpParam = Mathf.Min(lerpParam, 1);

                    hammerObject.transform.position = Vector3.Slerp(notActiveObject.position, activeObject.position, lerpParam);
                    hammerObject.transform.rotation = Quaternion.Slerp(notActiveObject.rotation, activeObject.rotation, lerpParam);
                }
            } else
            {
                if (lerpParam > 0)
                {
                    lerpParam -= speed * Time.deltaTime;
                    lerpParam = Mathf.Max(lerpParam, 0);

                    hammerObject.transform.position = Vector3.Slerp(notActiveObject.position, activeObject.position, lerpParam);
                    hammerObject.transform.rotation = Quaternion.Slerp(notActiveObject.rotation, activeObject.rotation, lerpParam);
                }
            }
        }
    }
}
