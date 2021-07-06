using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float horizontalSpeed = 10f;
    [SerializeField] float jumpSpeed = 50f;
    
    float verticalInput;
    float horizontalInput;
    Rigidbody2D playerRigidbody;
    [SerializeField] private bool onGround = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.position += transform.right * horizontalInput * horizontalSpeed *Time.deltaTime;
        if (Input.GetButtonDown("Jump") && onGround)
        {
            Jump();
        }
    }

    private void Jump()
    { 
            onGround = false;
            Vector2 jumpPower = new Vector2(0, jumpSpeed);
            GetComponent<Rigidbody2D>().AddForce(jumpPower, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PlayerSide")
        {
            onGround = true;
            Debug.Log("On Ground :"+onGround);
        }

        if (other.gameObject.tag == "Ball")
        {
            int randomX = 2; //UnityEngine.Random.Range(1,3);
            int randomY = UnityEngine.Random.Range(2, 8);
            Vector2 bounceDir = new Vector2(randomX, randomY);                 //other.gameObject.transform.position - gameObject.transform.position;
            Vector2 shootForce;
            Debug.Log(bounceDir);
            bounceDir.Normalize();
            shootForce = bounceDir * UnityEngine.Random.Range(300, 600);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(shootForce);
            Debug.Log("Player Shooting Ball: " +"("+randomX+","+randomY+")"+" Force: "+shootForce);
        }
    }
}
