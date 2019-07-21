using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    //Smooth factor breaks movement into smaller steps to smooth it
    public float smoothFactor = 0.04f;
    public int opposite = -1;
    public int bulletSpeed = 10;
    public GameObject projectile;
    public int hitPoints = 10;
    public GameObject[] gameObjects;
    public int score=0;
    public Transform playerSpawn;
    private Text hP;
    public Text Score;
    private SpriteRenderer sR;
    bool facingRight;
    bool invincible;
    Rigidbody2D rb;

    private Animator anim;
    


    private bool isMoving;


    // Start is called before the first frame update


    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GameObject projectile = gameObject.GetComponent<GameObject>();
        rb.mass = 0.1f;
        hP = GameObject.Find("hP").GetComponent<Text>();
        Score = GameObject.Find("Score").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        hP.text = "HP: " + hitPoints;
        Score.text = "Score: " + score;
        if (Input.GetMouseButtonDown(0) && isMoving == false)
        {

            StartCoroutine(Recoil());

        }

    }
    public IEnumerator Recoil()
    {
//The movement of the game happens into the opposite direction of the mouse click
        isMoving = true;

        float step = speed * smoothFactor;
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
       
        Shoot(mouse);

        mouse *= opposite;




        for (int steps = 0; steps < 5; steps++)
        {
            yield return new WaitForSeconds(smoothFactor);
            rb.MovePosition(Vector3.MoveTowards(transform.position, mouse, step));
            //transform.position = Vector3.MoveTowards(transform.position, mouse, step);
        }
        isMoving = false;






    }
    void Shoot(Vector2 mouse)
    {     
    //Spawns the bullet and turns the sprite according to direction of the click
        GameObject Bullet = Instantiate(projectile, transform.position, Quaternion.identity);

        if (mouse.x < transform.position.x)
        { 
            if (facingRight == true)
            {
                sR.flipX = true;
                facingRight = false;
            }
            anim.SetTrigger("Fire left");
        }
        else
        {
            if (facingRight == false)
            {
                facingRight = true;
                sR.flipX = false;
            }
            anim.SetTrigger("Fire right");
        }
        Rigidbody2D rigidb = Bullet.GetComponent<Rigidbody2D>();
        rigidb.velocity = mouse * bulletSpeed;
            
           
    }
    public void getDamage()
    {
    //Counts damage and activates small invincibility window when done so

        if (invincible == true)
        {
            Debug.Log("Invulnerable");
            StartCoroutine(InvulnerabilityTimer());
            return;
        }
        if (invincible == false)
        {
            hitPoints -= 1;
            anim.SetTrigger("Hit");
            Debug.Log("Hp: " + hitPoints);
            invincible = true;
            if (hitPoints == 0)
            {
                gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

                for (var i = 0; i < gameObjects.Length; i++)
                {
                    Destroy(gameObjects[i]);
                }
                transform.position = Vector3.MoveTowards(transform.position, playerSpawn.position, 50);
                GameObject GameManager = GameObject.Find("GameManager");
                GameManager GM = GameManager.GetComponent<GameManager>();
                hitPoints = 5;
                StartCoroutine(GM.InitiliazeGame());

            }
        }
    }
    public IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(0.5f);
        invincible = false;
        Debug.Log(invincible);
        yield break; 
    }
}


