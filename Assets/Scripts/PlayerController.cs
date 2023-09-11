using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rb;

    bool mouseButtonDown;
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
            if(collision.gameObject.tag == "enemy")
            {
                Destroy(collision.transform.parent.gameObject);
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
