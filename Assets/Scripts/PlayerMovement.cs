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
        horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        transform.position += transform.right * horizontalInput * horizontalSpeed *Time.deltaTime;
        if (CrossPlatformInputManager.GetButtonDown("Jump") && onGround)
        {
            Jump();
        }
    }

    private void Jump()
    { 
            onGround = false;
            Vector2 jumpPower = new Vector2(0, jumpSpeed);
            GetComponent<Rigidbody2D>().AddForce(jumpPower, ForceMode2D.Impulse);
            Debug.Log("Player Jumping");
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
            Vector2 bounceDir = other.gameObject.transform.position - gameObject.transform.position;
            Vector2 shootForce;
            Debug.Log(bounceDir);
            bounceDir.Normalize();
            shootForce = bounceDir * 100f;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(shootForce);
            Debug.Log("Shooting Ball: " + shootForce);
        }
    }
}
