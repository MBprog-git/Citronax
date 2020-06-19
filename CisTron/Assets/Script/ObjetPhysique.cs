using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetPhysique : MonoBehaviour
{
    Rigidbody2D rb;
   public float ForceRota;
    public float ForcePropuX;
    public float ForcePropuY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(ForcePropuX, ForcePropuY));
        rb.AddTorque(ForceRota);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     if(collision.tag == "Respawn")
        {
            Destroy(this.gameObject);
        }   
    }
}
