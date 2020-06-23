using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPhase : MonoBehaviour
{
    public GameObject[] fruitTab;
    private bool isActive;
    private GameObject theObject;
    public float waitInstance = 0;
    public float TempsGame = 25;
   // public int score = 0;
    public int countWin = 0;
    public int countLose = 0;
    List<Collider2D> listCol = new List<Collider2D>();
    //List<Vector3> listPos = new List<Vector3>();
    public bool startGame;

    public GameObject panelWinStrick;
    public GameObject panelLoseStrick;

    private void Start()
    {
        isActive = true;
       // startGame = true;
    }

    private void Update()
    {

        if (startGame)
        {
            
            if (isActive)
            {
                StartCoroutine(FruitInstance());
            }


            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Began)
                {
                    Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                    for (int i = 0; i < listCol.Count; i++)
                    {
                        if (listCol[i] == touchedCollider)
                        {
                            if (listCol[i].tag == "CitronVert")
                            {
                                FindObjectOfType<AudioManager>().Play("BonFruit");
                                if (countWin >= 5)
                                {
                                    WinStrick();
                                    panelWinStrick.SetActive(true);
                                }

                                else
                                    GameManager.instance.UpdateScore(1);
                                countWin += 1;
                                countLose = 0;
                                panelLoseStrick.SetActive(false);
                            }
                            else if (listCol[i].tag == "RainbowCitron")
                            {
                                FindObjectOfType<AudioManager>().Play("BonFruit");
                                if (countWin >= 5)
                                {
                                    GameManager.instance.UpdateScore(8);
                                    panelWinStrick.SetActive(true);
                                }

                                else
                                    GameManager.instance.UpdateScore(4);
                                countWin += 1;
                                countLose = 0;
                                panelLoseStrick.SetActive(false);
                            }
                            else
                            {
                                FindObjectOfType<AudioManager>().Play("MauvaisFruit");
                                if (countLose >= 5)
                                {
                                    LoseStrick();
                                    panelLoseStrick.SetActive(true);
                                }

                                else
                                    GameManager.instance.UpdateScore(-1);
                                countLose += 1;
                                countWin = 0;
                                panelWinStrick.SetActive(false);
                            }

                            Destroy(listCol[i].gameObject);
                            listCol.RemoveAt(i);
                            //listPos.RemoveAt(i);
                        }
                    }
                }
            }
        }
    }

    void WinStrick()
    {
        GameManager.instance.UpdateScore(2);
    }

    void LoseStrick()
    {
        GameManager.instance.UpdateScore(-2);
    }

    IEnumerator FruitInstance()
    {
        isActive = false;
        theObject = fruitTab[Random.Range(0, 12)];
        Vector3 pos = new Vector3(Random.Range(-5, 6), Random.Range(4, 4), 0);
        
        //for (int i = 0; i < listPos.Count; i++)
        //{
        //    if (listCol[i] == null)
        //    {
        //        listCol.RemoveAt(i);
        //        listPos.RemoveAt(i);
        //    }
        //    if (pos == listPos[i])
        //    {
        //        isActive = true;
        //        yield break;
        //    }
        //}
        GameObject ob = Instantiate(theObject, pos, Quaternion.identity);
        ob.transform.parent = GameManager.instance.PanelPhase1.transform;
        listCol.Add(ob.GetComponent<Collider2D>());
        //listPos.Add(pos);
        yield return new WaitForSeconds(waitInstance);
        isActive = true;
    }


  public  IEnumerator TimeFirstPhase()
    {
        yield return new WaitForSeconds(TempsGame);
        startGame = false;
        countWin = 0;
        countLose = 0;
        panelLoseStrick.SetActive(false);
        panelWinStrick.SetActive(false);

        if (GameManager.instance.PlayerOneTurn)
        {
            GameManager.instance.ChangePlayer();
           
        }
        else
        {
            GameManager.instance.ChangePlayer();
            GameManager.instance.SetInstruc();
        }
        ClearAll();
    }

    public void DeleteList(Collider2D collider)
    {
        for (int i = 0; i < listCol.Count; i++)
        {
            if (listCol[i] == collider)
            {
                listCol.RemoveAt(i);
                //listPos.RemoveAt(i);
            }
        }
    }

    public void ClearAll()
    {
        //listPos.Clear();
        foreach(Collider2D child in listCol)
        {
            Destroy(child.gameObject);
        }
        listCol.Clear();
    }
}