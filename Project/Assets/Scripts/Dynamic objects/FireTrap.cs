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
        [SerializeField] private DeathCollider deathCollider;

        public float LastBadActionTime { get; private set; }
        public Player LastPlayer { get; private set; }

        bool state = false;

        public void Activate(Player player, bool activeState)
        {
            state = activeState;

            if (activeState == PlayerManager.isBadRoute)
            {
                fire.Play();
                LastBadActionTime = Time.time;
                LastPlayer = player;
                deathCollider.SetEnable(true);
            } else
            {
                fire.Stop();
                deathCollider.SetEnable(false);
            }

            SetEmission(activeState, player.PlayerColor);
        }

        private void Start()
        {
            if (fireTrapObject == null)
            {
                fireTrapObject = gameObject;
            }

            if (fire == null)
            {
                fire = fireTrapObject.GetComponent<ParticleSystem>();
            }
            if (deathCollider == null)
            {
                deathCollider = fire.GetComponent<DeathCollider>();
            }
            
            Init(fireTrapObject);
        }

        private void Update()
        {
            if (state ^ PlayerManager.isBadRoute)
            {
                fire.Stop();
                deathCollider.SetEnable(false);
            } else
            {
                fire.Play();
                deathCollider.SetEnable(true);
            }
        }
    }
}
