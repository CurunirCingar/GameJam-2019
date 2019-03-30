using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESPAWN : MonoBehaviour
{

    [SerializeField] GameObject Wall;
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;
    [SerializeField] PlayerManager Player1Manager;
    [SerializeField] PlayerManager Player2Manager;
    [SerializeField] Transform Player1StartPosition;
    [SerializeField] Transform Player2StartPosition;
    [SerializeField] Transform BadPlayerStartPosition;





    private void StartBadRoute()
    {
        Wall.transform.position = Wall.GetComponent<DEATHWALL>().StartOfLocation;

        if (Player1Manager.isGood)
        {
            Player1.transform.position = Player1StartPosition.position;
            Player1Manager.GetSkills();
        }

        if (Player1Manager.isBad)
            Player1.transform.position = BadPlayerStartPosition.position;

        if (Player2Manager.isGood)
        {
            Player2.transform.position = Player2StartPosition.position;
            Player2Manager.GetSkills();
        }

        if (Player2Manager.isBad)
            Player2.transform.position = BadPlayerStartPosition.position;
    }



    public void Player1GoodDeath()
    {
        Player1.transform.position = Player2.transform.position;
    }


    public void Player2GoodDeath()
    {
        Player2.transform.position = Player1.transform.position;
    }

   
    
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //if (Player1.GetComponent<PlayerManager>().BadDeath || Player2.GetComponent<PlayerManager>().BadDeath)
        //    StartBadRoute();

       

    }
}
