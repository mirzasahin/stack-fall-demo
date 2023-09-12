using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    bool mouseButtonDown;

    float currenTime;
    bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mouseButtonDown = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            mouseButtonDown=false;
        }

        if(invincible)
        {
            currenTime -= Time.deltaTime * .35f;
        }

        else
        {
            if (mouseButtonDown)
            {
                currenTime += Time.deltaTime * 0.8f;
            }
            else
            {
                currenTime -= Time.deltaTime * 0.5f;
            }
        }

        if(currenTime >= 1)
        {
            currenTime = 1;
            invincible = true;
            Debug.Log(invincible);
        }
        else if(currenTime <= 0)
        {
            currenTime = 0;
            invincible = false;
            Debug.Log(invincible);
        }
    }

    private void FixedUpdate()
    {
        if(mouseButtonDown)
        {
            rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
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
                    Destroy(collision.transform.parent.gameObject);
                }
            }
            else
            {
                if (collision.gameObject.tag == "enemy")
                {
                    Destroy(collision.transform.parent.gameObject);
                }
            }
            
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!mouseButtonDown)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }


}
