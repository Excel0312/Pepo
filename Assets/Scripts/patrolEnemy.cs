using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class patrolEnemy : MonoBehaviour
{
    private Rigidbody2D rb;

    public float kecepatanGerak;

    public bool berbalik;
   
    void Start()
    {
        berbalik = true;
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        if(berbalik)
        {
        rb.velocity = new Vector2(kecepatanGerak, rb.velocity.y);
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
        rb.velocity = new Vector2(-kecepatanGerak, rb.velocity.y);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.gameObject.CompareTag("Balik"))
        {
            berbalik = !berbalik;
        }
    }
}


