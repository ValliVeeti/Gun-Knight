using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool doingSetup;
    private Text startText;
    public GameObject player;
    public GameObject spawner;
    public Transform[] positions;
    public Transform playerSpawn;
    // Start is called before the first frame update
    void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        StartCoroutine(InitiliazeGame());
    }
    public IEnumerator InitiliazeGame()
    {
        doingSetup = true;
        startText = GameObject.Find("startText").GetComponent<Text>();
        yield return WaitforMouse();
        
    }
    private IEnumerator WaitforMouse()
    {
        bool done = false;
        while (done != true)
        {
            if (Input.GetMouseButtonDown(0) == true)
            {
                Destroy(startText);
                doingSetup = false;
                done = true;

                for(int i= 0; i<positions.Length; i++)
                {
                    Instantiate(spawner, positions[i].transform.position, Quaternion.identity);
                    Debug.Log("Spawner active");
                }
            }
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
