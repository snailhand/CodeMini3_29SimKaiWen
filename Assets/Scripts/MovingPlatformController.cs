using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public GameObject Player;
    bool ridingPlatform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Landing on the moving platform
    private void OnCollisionEnter(Collision collision)
    {
        //collision.collider is player reference

        if(collision.gameObject.CompareTag("Player"))
        {
            ridingPlatform = true;
            collision.collider.transform.SetParent(transform);
        }
    }

    //Jumping off the moving platform
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ridingPlatform = false;
            collision.collider.transform.SetParent(null);
        }
    }

}
