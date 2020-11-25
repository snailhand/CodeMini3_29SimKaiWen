using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    float platformLimit = 42f;
    float speed = 10f;
    float startPos = 25f;
    public bool forwardMove = true;
    bool ridingCube = false;
    public bool crateOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        platformMove();
    }

    private void platformMove()
    {
        if (crateOn)
        {
            //moving platform

            if (transform.position.z < platformLimit && forwardMove)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            if (!forwardMove)
            {
                transform.Translate(Vector3.back * Time.deltaTime * speed);
            }


            if (transform.position.z <= startPos)
            {
                forwardMove = true;
            }
            if (transform.position.z >= platformLimit)
            {
                forwardMove = false;
            }
        }
    }

}
