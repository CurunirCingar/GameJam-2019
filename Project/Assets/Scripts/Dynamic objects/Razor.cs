using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInput;


namespace Dynamic_objects
{
    public class Razor : EmissiveObject, IActivateObject
    {
        [SerializeField] private List<GameObject> wheelObjects;
        [SerializeField] private float speed = 1;
        [SerializeField] private Vector3 axis = new Vector3(0, 1, 0);

        public float LastBadActionTime { get; private set; }
        public Player LastPlayer { get; private set; }

        private bool state = false;


        public void Activate(Player player, bool activeState)
        {
            state = activeState;

            if (activeState == PlayerManager.isBadRoute)
            {
                LastBadActionTime = Time.time;
                LastPlayer = player;
            }

            SetEmission(activeState, player.PlayerColor);
        }


        private void Start()
        {            
            Init(wheelObjects);
        }

        private void Update()
        {
            if (state == PlayerManager.isBadRoute)
            {
                foreach (var gameObj in wheelObjects)
                {
                    gameObj.transform.Rotate(axis, speed * Time.deltaTime);
                }
            } 
        }
    }
}
