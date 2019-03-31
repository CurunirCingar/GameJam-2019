using System.Collections;
using System.Collections.Generic;
using Network;
using PlayerInput;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour
{
    public static bool isBadRoute = false;

    [SerializeField] private FlyBehaviour fly;
    private GameObject Wall;
    Transform Player1StartPosition;
    Transform BadPlayerStartPosition;
    private GameObject Barier;
    public Transform checkpoint;
    public bool CanisKillable; //Есть ли неуязвимость
    public bool isKillable; //Включаем, отключаем бессмертие
    public bool GoodDeath; //НеУбилДругого  
    public bool BadDeath; //УбилДругого
    public bool isGood; //Хороший
    public bool isBad;  //Плохой
    public float Acceleration = 2f;
    public float DoubleJump = 3f;

    public GameObject goodEndingTitles;
    public GameObject badEndingTitles;


    public void GetSkills()
    {
        if (!isLocalPlayer)
            return;
        fly.enabled = true; //Может летать
        CanisKillable = false; //Неубиваем
        GetComponent<MoveBehaviour>().runSpeed = Acceleration; // Быстро бегает
        GetComponent<MoveBehaviour>().jumpHeight = DoubleJump; // Двойной прыжок
        GetComponent<Player>().RayEnabled = false;
    }

    
    public void StartBadRoute()
    {
        isBadRoute = true;
        Barier.SetActive(true);

       
        
        if (isGood)
        {
            transform.position = Player1StartPosition.position;
            GetSkills();
        }

        if (isBad)
        {
            Wall.transform.position = Wall.GetComponent<DEATHWALL>().StartOfLocation;
            Wall.GetComponent<DEATHWALL>().speed *= 2;
            transform.position = BadPlayerStartPosition.position;

            if (isLocalPlayer)
                fly.enabled = true;
        }
    }

    public void PlayerGoodDeath()
    {
        transform.position = checkpoint.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        Barier = GameController.Manager.Barrier;
        Wall = GameObject.FindWithTag("WALL");
        BadPlayerStartPosition = GameController.Manager.BadSpawnPosition;
        Player1StartPosition = GameController.Manager.GoodSpawnPosition;

        
        isGood = true;
        isKillable = true;
        isBad = false;
        
        GameController.Manager.AddPlayer(this);
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

    [ClientRpc]
    public void RpcUltimateKill()
    {
        if(isGood)
        {
            goodEndingTitles.SetActive(true);
        }
        else if (isBad)
        {
            badEndingTitles.SetActive(true);
        }
    }
}
