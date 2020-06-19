using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipo : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 endPosition;

    public float Temps;
        float timer;
    public float PopRate;
    float timerPop;
    bool Ingame;
    public float minimalSwipeDistance;
    public int AddScoreSwipe;
    int count;
    int transfoUn;
    int transfoDeux;
    int transfo3;
    GameObject TransfoActif;
    public GameObject Forme0;
    public GameObject Forme1;
    public GameObject Forme2;
    public GameObject Forme3;
    public GameObject Glagla;
    public GameObject Arsenic;
    public float BordX;
    public float MaxY;
    public float MinY;
    void Update()
    {
        if (timer > 0) { 
            timer -= Time.deltaTime;
            if (Input.touchCount == 1)
            {
                var touch = Input.touches[0];
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        break;
                    case TouchPhase.Ended:

                        endPosition = Camera.main.ScreenToWorldPoint(touch.position);

                        if (Vector2.Distance(startPosition, endPosition) > minimalSwipeDistance && GameManager.instance.Phase == 2)
                        {
                            //si end(+start?) pas dans citron mais que la distance passe dans citron
                            bool EndTouch = false;
                            bool StartTouch = false;

                            RaycastHit2D[] hits2DStart = Physics2D.RaycastAll(startPosition, Vector3.forward);
                            foreach (RaycastHit2D hit2DStart in hits2DStart)
                            {
                                if (hit2DStart.collider.tag == "Player")
                                {
                                    StartTouch = true;
                                }

                            }
                            RaycastHit2D[] hits2DEnd = Physics2D.RaycastAll(endPosition, Vector3.forward);
                            foreach (RaycastHit2D hit2DEnd in hits2DEnd)
                            {
                                if (hit2DEnd.collider.tag == "Player")
                                {
                                    EndTouch = true;
                                }

                            }
                            RaycastHit2D[] hits2D = Physics2D.RaycastAll(startPosition, (endPosition - startPosition).normalized);
                            foreach (RaycastHit2D hit2D in hits2D)
                            {
                                if (hit2D.collider.tag == "Player" && !EndTouch && !StartTouch)
                                {
                                    GameManager.instance.UpdateScore(AddScoreSwipe);
                                    count++;
                                    if (count >= transfo3)
                                    {
                                        TransfoActif.SetActive(false);
                                        TransfoActif = Forme3;
                                        TransfoActif.SetActive(true);

                                    }else if (count >= transfoDeux)
                                    {
                                        TransfoActif.SetActive(false);
                                        TransfoActif = Forme2;
                                        TransfoActif.SetActive(true);
                                    }
                                    else if (count >= transfoUn)
                                    {
                                        TransfoActif.SetActive(false);
                                        TransfoActif = Forme1;
                                        TransfoActif.SetActive(true);
                                    }

                                }

                            }
                        }

                        break;
                }
            }

        }
            else if (GameManager.instance.Phase == 2 && Ingame ) 
            {
                EndPhase2();
            }
        /* Click Version pour test
         if (Input.GetMouseButtonDown(0)) 
         {
             startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             Debug.Log("ClickDown");
         }
         if (Input.GetMouseButtonUp(0))
         {
             endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             Debug.Log("ClickUp");

            // if (Vector2.Distance(startPosition, endPosition) > minimalSwipeDistance && GameManager.instance.Phase == 2)


                 bool EndTouch = false;
                 bool StartTouch = false;

                 RaycastHit2D[] hits2DStart = Physics2D.RaycastAll(startPosition, Vector3.forward);
                 foreach (RaycastHit2D hit2DStart in hits2DStart)
                 {
                     if (hit2DStart.collider.tag == "Player")
                     {
                         StartTouch = true;
                     }

                 }
                 RaycastHit2D[] hits2DEnd = Physics2D.RaycastAll(endPosition, Vector3.forward);
                 foreach (RaycastHit2D hit2DEnd in hits2DEnd)
                 {
                     if (hit2DEnd.collider.tag == "Player")
                     {
                         EndTouch = true;
                     }

                 }
                 RaycastHit2D[] hits2D = Physics2D.RaycastAll(startPosition, (endPosition - startPosition).normalized);
                 foreach (RaycastHit2D hit2D in hits2D)
                 {
                     if (hit2D.collider.tag == "Player" && !EndTouch && !StartTouch)
                     {
                     Debug.Log("OKay");
                     }

                 }
             }
         */


    }

   public  void StartPhase2()
    {
        timer = Temps;
        Ingame = true;
        TransfoActif = Forme0;
        count = 0;
        TransfoActif.SetActive(true);
    }
    void EndPhase2()
    {
        TransfoActif.SetActive(false);
        Ingame = false;
        if (GameManager.instance.PlayerOneTurn) {
            GameManager.instance.ChangePlayer();
            
        }
        else
        {
            GameManager.instance.ChangePlayer();
            GameManager.instance.SetInstruc();
        }
    }

    void Pop()
    {
        timerPop -= Time.deltaTime;
        if (timerPop < 0)
        {

            int Type = Random.Range(0, 5);
            int Bord = Random.Range(0, 2);

            if(Bord == 1)
            {
               BordX  = -BordX;
            }
        
            float y = Random.Range(MinY,MaxY);
          
            
            Vector2 SpawnPos = new Vector2(BordX, y);
            if (Type==4 || Type==3)
            {
                //Pop Malus
                GameObject go = Instantiate(Arsenic,SpawnPos, transform.rotation );
            }
            else
            {
                //Pop Bonus
                GameObject go = Instantiate(Glagla, SpawnPos, transform.rotation);
            }

            float T = Random.Range(0, 5) ;
            timerPop = PopRate+ T;
        }
    }
}
