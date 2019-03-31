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
            player.GetComponent<PlayerManager>().StartBadRoute();
        }
    }
}
