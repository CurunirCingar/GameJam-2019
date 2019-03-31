using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEATHWALL : MonoBehaviour
{
    public Vector3 StartOfLocation;
    public Vector3 EndOfLocation;
    [SerializeField] float speed = 1;


    public void SetWallToStart()
    {        
        transform.position = StartOfLocation;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartOfLocation = transform.position;
        SetWallToStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < EndOfLocation.z)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            other.GetComponent<PlayerManager>().PlayerGoodDeath();
        }               
    }


}
