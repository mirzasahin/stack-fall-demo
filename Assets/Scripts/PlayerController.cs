using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    bool mouseButtonDown;

    float currenTime;
    bool invincible;
    bool isOnEnemy;

    public GameObject fireShield;

    public enum PlayerState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }

    [HideInInspector]
    public PlayerState playerState = PlayerState.Prepare;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerState == PlayerState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseButtonDown = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                mouseButtonDown = false;
            }

            if (invincible)
            {
                currenTime -= Time.deltaTime * .35f;
                if (!fireShield.activeInHierarchy)
                {
                    fireShield.SetActive(true);
                }
            }

            else
            {
                if (fireShield.activeInHierarchy)
                {
                    fireShield.SetActive(false);
                }

                if (mouseButtonDown && isOnEnemy)
                {
                    currenTime += Time.deltaTime * 0.8f;
                }
                else
                {
                    currenTime -= Time.deltaTime * 0.5f;
                }
            }

            if (currenTime >= 1)
            {
                currenTime = 1;
                invincible = true;
            }
            else if (currenTime <= 0)
            {
                currenTime = 0;
                invincible = false;
            }
        }

        if(playerState == PlayerState.Prepare)
        {
            if(Input.GetMouseButton(0))
            {
                playerState = PlayerState.Playing;
            }
        }

        if(playerState == PlayerState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().NextLevel();
            }
        }
    }

    private void FixedUpdate()
    {
        if(playerState == PlayerState.Playing)
        {
            if (mouseButtonDown)
            {
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!mouseButtonDown)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
        else
        {
            if (invincible)
            {
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "plane")
                {
                    //Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                }
            }
            else
            {
                if (collision.gameObject.tag == "enemy")
                {
                    isOnEnemy = true;
                    //Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                }
                else
                {
                    isOnEnemy = false;
                }
            }
        }

        if(collision.gameObject.tag == "Finish" && playerState == PlayerState.Playing)
        {
            playerState = PlayerState.Finish;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!mouseButtonDown || collision.gameObject.tag == "Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }


}
