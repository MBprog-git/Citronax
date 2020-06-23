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
    public float TempsBonus;
    float timerBonus;
    public float minimalSwipeDistance;
    public int AddScoreSwipeBase;
    int count;
    public int transfoUn;
    public int transfoDeux;
    //public int transfo3;
    int AddScore;
    GameObject TransfoActif;
    public GameObject Forme0;
    public GameObject Forme1;
    public GameObject Forme2;
   // public GameObject Forme3;
    public GameObject Glagla;
    public GameObject Arsenic;
    public float BordX;
    public float MaxY;
    public float MinY;
    public GameObject Slash;

    private void Start()
    {
        AddScore = AddScoreSwipeBase;
    }
    void Update()
    {
        if (timer > 0) {
            Pop();
            timer -= Time.deltaTime;
            timerBonus -= Time.deltaTime;
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
                            bool CitronTouch = false;
                            bool BonusTouch = true;
                            List<GameObject> Bonus = new List<GameObject>();

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
                                if (hit2D.collider.tag == "Player")
                                {
                                    CitronTouch = true;
                                }
                                if (hit2D.collider.tag == "Player" && !EndTouch && !StartTouch)
                                {
                                    GameManager.instance.UpdateScore(AddScore);
                                    count++;
                                    Vector2 _dir = endPosition - startPosition;
                                    Vector2 Mid = startPosition + (endPosition - startPosition) / 2; ;
                                    float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;

                                    Instantiate(Slash, Mid, Quaternion.AngleAxis(angle - 45f, Vector3.forward));


                                    if (count >= transfoDeux &&  TransfoActif != Forme2)
                                    {
                                        TransfoActif.SetActive(false);
                                        TransfoActif = Forme2;
                                        TransfoActif.SetActive(true);
                                    }
                                    else if (count >= transfoUn && TransfoActif != Forme1 && count < transfoDeux)
                                    {
                                        TransfoActif.SetActive(false);
                                        TransfoActif = Forme1;
                                        TransfoActif.SetActive(true);
                                    }

                                }
                                if(hit2D.collider.GetComponent<ObjetPhysique>() != null)
                                {
                                    Bonus.Add (hit2D.collider.gameObject);
                                    BonusTouch = true;
                                    Vector2 _dir = endPosition - startPosition;
                                    Vector2 Mid = startPosition + (endPosition - startPosition) / 2; ;
                                    float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;

                                    Instantiate(Slash, Mid, Quaternion.AngleAxis(angle - 45f, Vector3.forward));
                                }
                                

                            }
                            if (BonusTouch && !CitronTouch)
                            {
                                for (int i = 0; i< Bonus.Count; i++)
                                {


                                    if (Bonus[i].GetComponent<ObjetPhysique>().IsBonus)
                                    {
                                        AddScore = AddScoreSwipeBase * 3;
                                        timerBonus = TempsBonus;
                                        FindObjectOfType<AudioManager>().Play("Glacon");
                                    }
                                    else
                                    {
                                        GameManager.instance.UpdateScore(-10);
                                        FindObjectOfType<AudioManager>().Play("Cyanure");
                                    }
                                    Destroy(Bonus[i]);
                                } }
                        }
                     

                        break;
                }
            }
            if (timerBonus < 0)
            {
                AddScore = AddScoreSwipeBase;
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
            float SpawnX;

            if(Bord == 1)
            {
               SpawnX  = -BordX;
            }
            else
            {
                SpawnX = BordX;
            }
        
            float y = Random.Range(MinY,MaxY);

            Vector2 SpawnPos = new Vector2(SpawnX, y);
            GameObject go;
            if (Type==4 || Type==3)
            {
                //Pop Malus
                 go = Instantiate(Arsenic,SpawnPos, transform.rotation );
                if (Bord == 1)
                {
                    go.GetComponent<ObjetPhysique>().ForceRota = -go.GetComponent<ObjetPhysique>().ForceRota;
                    go.GetComponent<ObjetPhysique>().ForcePropuX = -go.GetComponent<ObjetPhysique>().ForcePropuX;
                }
            }
            else
            {
                //Pop Bonus
                 go = Instantiate(Glagla, SpawnPos, transform.rotation);
                if (Bord == 1)
                {
                    go.GetComponent<ObjetPhysique>().ForceRota = -go.GetComponent<ObjetPhysique>().ForceRota;
                    go.GetComponent<ObjetPhysique>().ForcePropuX = -go.GetComponent<ObjetPhysique>().ForcePropuX;
                }
             
            }

          
            float T = Random.Range(0, 3) ;
            timerPop = PopRate + T;
        }
    }
}
