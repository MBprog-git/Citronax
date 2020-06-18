using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPhase : MonoBehaviour
{
    public GameObject[] fruitTab;
    private bool isActive;
    private GameObject theObject;
    public float waitInstance = 0;
    public int score = 0;
    public int count = 0;
    List<Collider2D> listCol = new List<Collider2D>();
    List<Vector3> listPos = new List<Vector3>();
    private bool startGame;

    private void Start()
    {
        isActive = true;
        startGame = true;
    }

    private void Update()
    {
        StartCoroutine(TimeFirstPhase());

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
                                if (count >= 5)
                                    WinStrick();

                                else
                                    score += 1;
                            }
                            else
                            {
                                if (count >= 5)
                                    LoseStrick();

                                else
                                    score -= 1;
                            }
                            count += 1;

                            Destroy(listCol[i].gameObject);
                            listCol.RemoveAt(i);
                            listPos.RemoveAt(i);
                        }
                    }
                }
            }
        }
    }

    void WinStrick()
    {
        score += 2;
    }

    void LoseStrick()
    {
        score -= 2;
    }

    IEnumerator FruitInstance()
    {
        isActive = false;
        theObject = fruitTab[Random.Range(0, 7)];
        Vector3 pos = new Vector3(Random.Range(-3, 3), Random.Range(-2, 2), 0);
        
        for (int i = 0; i < listPos.Count; i++)
        {
            if (listCol[i] == null)
            {
                listCol.RemoveAt(i);
                listPos.RemoveAt(i);
            }
            if (pos == listPos[i])
            {
                isActive = true;
                yield break;
            }
        }
        GameObject ob = Instantiate(theObject, pos, Quaternion.identity);
        listCol.Add(ob.GetComponent<Collider2D>());
        listPos.Add(pos);
        yield return new WaitForSeconds(waitInstance);
        isActive = true;
    }


    IEnumerator TimeFirstPhase()
    {
        yield return new WaitForSeconds(25);
        startGame = false;
    }

    public void DeleteList(Collider2D collider)
    {
        for (int i = 0; i < listCol.Count; i++)
        {
            if (listCol[i] == collider)
            {
                listCol.RemoveAt(i);
                listPos.RemoveAt(i);
            }
        }
    }
}