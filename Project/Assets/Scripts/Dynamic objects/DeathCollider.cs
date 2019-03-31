using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    Collider myCollider;

    public void SetEnable(bool isEnable)
    {
        myCollider.enabled = isEnable;
    }

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerInput.Player player = collision.collider.GetComponent<PlayerInput.Player>();

        if (player != null)
        {

            if (player.GetComponent<PlayerManager>().isKillable)
            {
                Dynamic_objects.IActivateObject nearestActivatable = Network.GameController.Manager.GetNearestActivatableObject(player.transform.position);

                if (nearestActivatable.LastPlayer != player && (Time.time - nearestActivatable.LastBadActionTime < 1f) ) {

                    PlayerManager playerManager = player.GetComponent<PlayerManager>();
                    playerManager.isGood = true;
                    foreach (var otherPlayerManager in FindObjectsOfType<PlayerManager>())
                    {
                        if ((otherPlayerManager as PlayerManager) != playerManager)
                        {
                            (otherPlayerManager as PlayerManager).isBad = true;
                        }
                    }

                    player.GetComponent<PlayerManager>().StartBadRoute();
                } else
                {
                    player.GetComponent<PlayerManager>().PlayerGoodDeath();
                }
            }
        }
    }
}
