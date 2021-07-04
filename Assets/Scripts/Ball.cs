using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int whichSide = 0;
    public GameObject ball;
    public int playerTouchCountLimit = 3;
    public int opponentTouchCountLimit = 3;

    [SerializeField] float ballMaxSpeed = 8.0f;
    [SerializeField] Vector2 lastVelocity;

    void Start()
    {

    }


    void FixedUpdate()
    {
        CheckBallSpeed();
        lastVelocity = ball.GetComponent<Rigidbody2D>().velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);

        if (other.gameObject.tag == "Player")
        {
            ball.GetComponent<Rigidbody2D>().gravityScale = 1f;

            Debug.Log("Ball Collided with player");

        }
        else if (other.gameObject.tag == "Opponent")
        {
            ball.GetComponent<Rigidbody2D>().gravityScale = 1f;

        }
        if (other.gameObject.tag == "OpponentSide")
        {
            PlayerScored();
        }
        else if (other.gameObject.tag == "PlayerSide")
        {
            OpponentScored();
        }
        else if (other.gameObject.tag == "Net")
        {
           // Vector2 bounceDir = other.gameObject.transform.position - gameObject.transform.position;
            Vector2 shootForce;
            // Debug.Log(bounceDir);
            // bounceDir.Normalize();
            direction.Normalize();
            // shootForce = bounceDir * 100f;
            shootForce = direction * 100f;
            transform.gameObject.GetComponent<Rigidbody2D>().AddForce(shootForce);
            Debug.Log("Net bounce: " + shootForce);
        }

    }

    void CheckBallSpeed()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude > ballMaxSpeed)
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * ballMaxSpeed;
    }

    void TouchCount()
    {

    }

    void PlayerScored()
    {
        GameManager.instance.DestroyBall();
        whichSide = 1;
        GameManager.instance.Score(whichSide);
    }
    void OpponentScored()
    {
        GameManager.instance.DestroyBall();
        whichSide = 2;
        GameManager.instance.Score(whichSide);
    }
}
