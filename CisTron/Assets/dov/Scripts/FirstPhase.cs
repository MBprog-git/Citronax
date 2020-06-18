using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPhase : MonoBehaviour
{
    public GameObject[] fruitTab;
    public bool isActive;
    private GameObject theObject;
    public float waitInstance = 0;
    //private int score = 0;
    //private int count = 0;
    List<Collider2D> listCol = new List<Collider2D>();
    List<Vector3> listPos = new List<Vector3>();

    private void Start()
    {
        isActive = true;
    }

    private void Update()
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
                        Destroy(listCol[i].gameObject);
                        listCol.RemoveAt(i);
                        listPos.RemoveAt(i);
                    }
                }
            }
        }
    }

    IEnumerator FruitInstance()
    {
        isActive = false;
        theObject = fruitTab[Random.Range(0, 5)];
        Vector3 pos = new Vector3(Random.Range(-3, 3), Random.Range(-2, 2), 0);
        
        for (int i = 0; i < listPos.Count; i++)
        {
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
}