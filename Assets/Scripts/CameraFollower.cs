using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Vector3 cameraPos;
    private Transform player, win;

    private float cameraOffset = 4f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(win == null)
        {
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();
        }

        if(transform.position.y > player.position.y && transform.position.y > win.position.y + cameraOffset)
        {
            cameraPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, cameraPos.y, -5);
        }
    }
}
