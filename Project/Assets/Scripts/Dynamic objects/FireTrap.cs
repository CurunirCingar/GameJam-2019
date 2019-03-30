using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInput;

namespace Dynamic_objects
{
    public class FireTrap : EmissiveObject, IActivateObject
    {
        [SerializeField] private GameObject fireTrapObject;
        [SerializeField] private ParticleSystem fire;

        public void Activate(Player player, bool activeState)
        {
            if (activeState)
                fire.Play();
            else
                fire.Stop();

            SetEmission(activeState, player.PlayerColor);
        }

        private void Start()
        {
            Debug.LogError("ОГНЕННАЯ ЛОВУШКА НЕ НАНОСИТ СМЕРТЬ! БЛЕАТЬ!");

            if (fireTrapObject == null)
            {
                fireTrapObject = gameObject;
            }

            if (fire == null)
            {
                fireTrapObject.GetComponent<ParticleSystem>();
            }
            
            Init(fireTrapObject);
        }
    }
}
