using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInput;

namespace Dynamic_objects
{
    public class Razor : EmissiveObject, IActivateObject
    {
        [SerializeField] private GameObject wheelObject;
        [SerializeField] private float speed = 1;
        [SerializeField] private Vector3 axis = new Vector3(0, 1, 0);

        private bool state = false;


        public void Activate(Player player, bool activeState)
        {
            state = activeState;

            SetEmission(activeState, player.PlayerColor);
        }


        private void Start()
        {
            if (wheelObject == null)
            {
                wheelObject = gameObject;
            }
            
            Init(wheelObject);
        }

        private void Update()
        {
            if (state)
            {
                wheelObject.transform.Rotate(axis, speed * Time.deltaTime);
            } 
        }
    }
}
