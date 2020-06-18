using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPhase : MonoBehaviour
{
    public GameObject[] fruitTab;
    public bool isActive;
    private GameObject theObject;
    private int score = 0;
    private int count = 0;
    Collider2D col;

    private void Update()
    {
        StartCoroutine(FruitInstance());

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if (col == touchedCollider)
                {
                    Destroy(col.gameObject);
                }
            }
        }
    }

    IEnumerator FruitInstance()
    {
        yield return new WaitForSeconds(2);
        theObject = fruitTab[Random.Range(0, 5)];
        Vector3 pos = new Vector3(Random.Range(-2, 2), Random.Range(-3, 3), 0);
        GameObject ob = Instantiate(theObject, pos, Quaternion.identity);
        col = ob.GetComponent<Collider2D>();
    }
}