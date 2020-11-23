using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float speed = 10f;
    float jumpHeight = 8f;
    float gravityMod = 2.5f;
    float jumpCount = 0;
    bool isOnGround;

    public GameObject PlaneRotate;  
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
        //Start Running

        if(Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            PlayerStartRun();
        }       
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            PlayerStartRun();
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

        PlayerJump();
    }

    private void PlayerStartRun()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        PlayerAnim.SetBool("isRun", true);
        PlayerAnim.SetFloat("startRun", 0);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayerAnim.SetTrigger("landed");
            isOnGround = true;
            jumpCount = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Cone"))
        {
            PlaneRotate.transform.rotate
        }
    }

    private void PlayerJump()
    {
        if(Input.GetKey(KeyCode.Space) && jumpCount == 0)
        {
            if (isOnGround == true)
            {
                PlayerAnim.SetTrigger("jumpTrigger");
                playerRb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                isOnGround = false;
                jumpCount = 1;
            }
        }
    }
}
