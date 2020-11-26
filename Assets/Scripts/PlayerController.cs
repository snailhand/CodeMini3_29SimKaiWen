using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //player variables
    float gravityMod = 2.5f;
    float speed = 6;
    float jumpHeight = 10;
    float jumpCount;
    bool isOnGround;

    //Timer variables
    float timeCount = 5;
    int timeCountInt;
    float activeBridge;

    //powerup variables
    int collectedPower;
    bool coneActive;

    //moving platform variables
    bool crateActive;
    bool forwardMove = true;
    float maxLimit = 32.5f;
    float startPos = 50f;
    float platformSpeed = 4f;

    //Object and Component references 
    public GameObject timerText;
    public GameObject rotatePlane;
    public GameObject movingPlane;
    public Animator PlayerAnim;
    Rigidbody playerRb;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMod;
    }

    // Update is called once per frame
    void Update()
    {
        //die check
        if(transform.position.y < -5)
        {
            SceneManager.LoadScene("GameOver");
        }

        //Player Movements_

        //Start running
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            PlayerAnim.SetBool("isRun", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            PlayerAnim.SetBool("isRun", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            PlayerAnim.SetBool("isRun", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            PlayerAnim.SetBool("isRun", true);
        }

        //Stop Running
        if (Input.GetKeyUp(KeyCode.W))
        {
            PlayerAnim.SetBool("isRun", false);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            PlayerAnim.SetBool("isRun", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            PlayerAnim.SetBool("isRun", false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            PlayerAnim.SetBool("isRun", false);
        }

        //jumping controls

        if (Input.GetKey(KeyCode.Space) && jumpCount == 0)
        {
            if (isOnGround == true)
            {
                PlayerAnim.SetTrigger("jump");
                playerRb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                isOnGround = false;
                jumpCount = 1;
                PlayerAnim.SetBool("landed", false);
            }
        }


        //Checking for all PowerUp collected
        if (coneActive)
        {
            timerCountDown();
        }
        //Checking for contact with crate
        if (crateActive)
        {
            MovingStart();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            isOnGround = true;
            PlayerAnim.SetBool("landed", true);
        }
    }

    private void timerCountDown()
    {
        if (timeCount > 0 && timeCount < 6)
        {
            timeCount -= Time.deltaTime;
            timeCountInt = Mathf.RoundToInt(timeCount);
        }
        else if (timeCount < 1 && activeBridge == 1)
        {
            rotatePlane.transform.Rotate(new Vector3(0, -90, 0));
            activeBridge = 0;
            coneActive = false;
        }

        timerText.GetComponent<Text>().text = "Timer: " + timeCountInt;

    }

    //In contact with objects
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PowerUp"))
        {
            collectedPower += 1;
            Destroy(other.gameObject);
            print("PowerUp Collected: " + collectedPower);
           
        }

        if(other.gameObject.CompareTag("Cone"))
        {
            if (collectedPower == 4 && activeBridge == 0)
            {
                //Rotating the platform
                rotatePlane.transform.Rotate(new Vector3(0, 90, 0));
                activeBridge = 1;

                coneActive = true;
            }

        }

        if(other.gameObject.CompareTag("Crate"))
        {
            crateActive = true;
        }

        if(other.gameObject.CompareTag("Mark"))
        {
            SceneManager.LoadScene("Victory");
        }
    }

    //Moving platform controls
    private void MovingStart()
    {
        if(movingPlane.transform.position.z > maxLimit && forwardMove)
        {
            movingPlane.transform.Translate(Vector3.back * Time.deltaTime * platformSpeed);
        }
        else if(movingPlane.transform.position.z < startPos &&!forwardMove)
        {
            movingPlane.transform.Translate(Vector3.forward * Time.deltaTime * platformSpeed);
        }

        if (movingPlane.transform.position.z <= maxLimit)
        {
            forwardMove = false;
        }
        if (movingPlane.transform.position.z >= startPos)
        {
            forwardMove = true;
        }
    }
}
