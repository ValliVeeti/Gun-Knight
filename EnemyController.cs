using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 1.0f;
    public float smoothFactor = 0.04f;
    public float hitRange = 1.0f;
    public int damage = 1;

    public bool walking;
    public bool dealingDamage;
    public int score;
    Rigidbody2D rb;






    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 1000f;
        dealingDamage = false;
        walking = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (walking == false && dealingDamage == false)
        {
            StartCoroutine(Walk());
        }

    }
    public IEnumerator Walk()
    {
        GameObject Player = GameObject.Find("Player");
        PlayerController player = Player.GetComponent<PlayerController>();
        if (Vector2.Distance(player.transform.position, transform.position) < (hitRange - 0.5f))
        {
            StartCoroutine(Damage());
            yield break;
        }
        else if(Vector2.Distance(player.transform.position, transform.position) > (hitRange - 0.5f))
        {
            walking = true;
            float step = speed * smoothFactor;

            for (int steps = 0; steps < 5; steps++)
            {

                yield return new WaitForSeconds(smoothFactor);
                rb.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, step));
                //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

            }
            walking = false;

            yield break;
        }

    }
    public IEnumerator Damage()
    {
        dealingDamage = true;
        yield return new WaitForSeconds(0.5f);
        GameObject Player = GameObject.Find("Player");
        PlayerController player = Player.GetComponent<PlayerController>();
        if (Vector2.Distance(player.transform.position, transform.position) < (hitRange - 0.5f))
        {
            player.getDamage();
            StartCoroutine(DamageCooldown());
            yield break;

        }
        else
        {

            StartCoroutine(DamageCooldown());


            yield break;
        }

        


    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            GameObject Player = GameObject.Find("Player");
            PlayerController player = Player.GetComponent<PlayerController>();
            player.score += 1;
            Destroy(col.gameObject);
            Destroy(gameObject);

        }

    }
    public IEnumerator DamageCooldown()
    {
       
        yield return new WaitForSeconds(3);

        dealingDamage = false;

        yield break;
    }
}
