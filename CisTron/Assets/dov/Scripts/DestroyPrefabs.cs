using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrefabs : MonoBehaviour
{
    private FirstPhase firstPhase;
    public float timer = 5;

    private void Start()
    {
        firstPhase = GameManager.instance.gameObject.GetComponent<FirstPhase>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            firstPhase.DeleteList(this.gameObject.GetComponent<Collider2D>());
            Destroy(this.gameObject);
        }
    }


}
