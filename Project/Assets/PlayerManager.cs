using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

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

    // Start is called before the first frame update
    void Start()
    {
        isGood = true;
        isKillable = true;
        isBad = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (BadDeath)
            isBad = true;

        if (Input.GetButtonDown("E") && CanisKillable == false)
            isKillable = false;
        if (Input.GetButtonUp("E") && CanisKillable == false)
            isKillable = true;

    }
}
