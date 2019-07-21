using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.mass = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D Enemy)
    {
        if (Enemy.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
        }

    }
    private void OnBecameInvisible()
    {
    //Deletes the bullet if it goes off screen
        Destroy(gameObject);
    }
}
