using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipo : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 endPosition;

    public float Temps;
        float timer;

    public float minimalSwipeDistance;
    public int AddScoreSwipe;



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
                        startPosition = touch.position;
                        break;
                    case TouchPhase.Ended:

                        endPosition = touch.position;

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
                                }

                            }
                        }

                        break;
                }
            }
            else if (GameManager.instance.Phase == 2) 
            {
                EndPhase2();
            }

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
    }
    void EndPhase2()
    {
        if (GameManager.instance.PlayerOneTurn) {
            GameManager.instance.ChangePlayer();
            StartPhase2();
        }
        else
        {
            GameManager.instance.ChangePlayer();
            GameManager.instance.ChangePhase();
        }
    }
}
