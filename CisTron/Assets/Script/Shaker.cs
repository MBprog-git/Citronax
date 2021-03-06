﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
        Vector3 AccelForce;


    public float Temps;
    float timer;

    public float MinAccel;
    bool ingame;

    float Multip;
   public GameObject Bouteille;

    AudioSource source;

    private void Start()
    {
        source = FindObjectOfType<AudioManager>().sounds[8].source;
    }
    void Update()
    {
        if (timer > 0)
        {

            timer -= Time.deltaTime;


            AccelForce = Input.acceleration;


            if (AccelForce.sqrMagnitude > MinAccel && GameManager.instance.Phase == 3)
            {
                Handheld.Vibrate();
                if (!source.isPlaying)
                {
                    FindObjectOfType<AudioManager>().Play("Shaker");
                }
                
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
        else if(GameManager.instance.Phase == 3 && ingame)
        {
            EndPhase3();
        }

    }

   public  void StartPhase3()
    {
        timer = Temps;
        ingame = true;
        Bouteille.SetActive(true);
    }

    void EndPhase3()
    {
        FindObjectOfType<AudioManager>().Stop("Shaker");
        ingame = false;
        Bouteille.SetActive(false);
            float Final;
            if (GameManager.instance.PlayerOneTurn)
            {
                Final = GameManager.instance.scoreP1 * Multip;
            GameManager.instance.UpdateScore((int)Final);
            GameManager.instance.ChangePlayer();
          
            }
            else
            {
                Final = GameManager.instance.scoreP2 * Multip;
            GameManager.instance.UpdateScore((int)Final);


            GameManager.instance.Phase = 4;
            GameManager.instance.ChangePhase();
            }



    }
    

}


