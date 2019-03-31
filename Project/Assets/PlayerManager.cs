using System.Collections;
using System.Collections.Generic;
using Network;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool isBadRoute = false;

    private GameObject Wall;
    Transform Player1StartPosition;
    Transform BadPlayerStartPosition;
    public Transform Checkpoint;
    public bool CanisKillable; //Есть ли неуязвимость
    public bool isKillable; //Включаем, отключаем бессмертие
    public bool GoodDeath; //НеУбилДругого  
    public bool BadDeath; //УбилДругого
    public bool isGood; //Хороший
    public bool isBad;  //Плохой
    public float Acceleration = 2f;
    public float DoubleJump = 3f;


    public void GetSkills()
    {
        GetComponent<FlyBehaviour>().enabled = true; //Может летать
        CanisKillable = false; //Неубиваем
        GetComponent<MoveBehaviour>().runSpeed = Acceleration; // Быстро бегает
        GetComponent<MoveBehaviour>().jumpHeight = DoubleJump; // Двойной прыжок
    }

    
    public void StartBadRoute()
    {
        isBadRoute = true;

        Wall.transform.position = Wall.GetComponent<DEATHWALL>().StartOfLocation;
        if (isGood)
        {
            transform.position = Player1StartPosition.position;
            GetSkills();
        }
        if (isBad)
            transform.position = BadPlayerStartPosition.position;
    }

    public void PlayerGoodDeath()
    {
        transform.position = Checkpoint.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        Wall = GameObject.FindWithTag("WALL");
        BadPlayerStartPosition = GameObject.FindWithTag("BadPosition")?.transform;
        Player1StartPosition = GameObject.FindWithTag("GoodPosition")?.transform;

        
        isGood = true;
        isKillable = true;
        isBad = false;
        
        NetworkManager.Manager.AddPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (BadDeath)
            isBad = true;

        if (Input.GetKeyDown(KeyCode.E) && CanisKillable == false)
            isKillable = false;
        if (Input.GetKeyUp(KeyCode.E) && CanisKillable == false)
            isKillable = true;

    }
}
