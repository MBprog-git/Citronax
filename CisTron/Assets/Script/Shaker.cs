﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
        Vector3 AccelForce;


    public float Temps;
    float timer;

    public float MinAccel;


    float Multip;
 
    void Update()
    {
        if (timer > 0)
        {

            timer -= Time.deltaTime;


            AccelForce = Input.acceleration;


            if (AccelForce.sqrMagnitude > MinAccel && GameManager.instance.Phase == 3)
            {
                float Magnitude = AccelForce.sqrMagnitude;

                if (Magnitude > MinAccel + 6 && Multip < 2)
                {
                    Multip = 2;
                }
                else if (Magnitude > MinAccel + 4 && Multip < 1.75f)
                {
                    Multip = 1.75f;
                }
                else if (Magnitude > MinAccel + 2 && Multip < 1.5f)
                {
                    Multip = 1.5f;
                }
                else
                {
                    Multip = 1.25f;
                }
            }
        }
        else if(GameManager.instance.Phase == 3)
        {
            EndPhase3();
        }

    }

     void StartPhase3()
    {
        timer = Temps;
    }

    void EndPhase3()
    {
        float Final;
        if (GameManager.instance.PlayerOneTurn)
        {
            Final = GameManager.instance.scoreP1 * Multip;
        }
        else
        {
            Final = GameManager.instance.scoreP2 * Multip;

        }
        
        GameManager.instance.UpdateScore((int)Final);
        GameManager.instance.ChangePhase();

        
    }

}

