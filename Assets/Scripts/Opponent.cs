using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
	//private AI parameters
	private bool canMove = true;
	[SerializeField] float moveSpeed = 13.0f;
	[SerializeField] float jumpSpeed = 300.0f;
	private float minJumpDistance = 2f;
	[SerializeField] bool canJump = true;
	private bool preventDoubleJump = true;
	private float jumpDelay = 2.0f;
	public float accuracy = 0.5f;
	private float adjustingPosition;

	private Vector2 cpuFieldLimits = new Vector2(0.55f, 9.45f);

	void Awake()
	{
		StartCoroutine(resetPosition());
	}
	void FixedUpdate()
	{
		if (canMove)
		{
			playSoccer();
		}
	}

	void playSoccer()
	{

		if (GameManager.instance.ballClone.transform.position.x > cpuFieldLimits.x && GameManager.instance.ballClone.transform.position.x < cpuFieldLimits.y)
		{
			//move the opponent towards the ball
			transform.position = new Vector2(Mathf.SmoothStep(transform.position.x, GameManager.instance.ballClone.transform.position.x + adjustingPosition, Time.deltaTime * moveSpeed),
											 transform.position.y);
			//if opponent is close enough to the ball, make it jump
			//Debug.Log("Ball position "+Vector2.Distance(transform.position, GameManager.instance.ballClone.transform.position));
			if (Vector2.Distance(transform.position, GameManager.instance.ballClone.transform.position) < minJumpDistance && canJump)
			{
				canJump = false;
				Vector2 jumpPower = new Vector3(0.5f, jumpSpeed - Random.Range(0, 50), 0);
				GetComponent<Rigidbody2D>().AddForce(jumpPower);
				Debug.Log("Jumping");
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("AI colliding with "+other.gameObject.tag);
		Vector2 bounceDir = other.gameObject.transform.position - gameObject.transform.position;
		Vector2 shootForce;
		Debug.Log(bounceDir);
		bounceDir.Normalize();
		shootForce = bounceDir * 100f;
		other.gameObject.GetComponent<Rigidbody2D>().AddForce(shootForce);
		if (other.gameObject.tag == "OpponentSide" && preventDoubleJump)
		{
			StartCoroutine(jumpActivation());
			Debug.Log("OpponentSide");
		}

	}
	public IEnumerator resetPosition()
	{
		canMove = false;
		adjustingPosition = Random.Range(0.1f, accuracy);
		yield return new WaitForSeconds(0.75f);
		canMove = true;
	}
	IEnumerator jumpActivation()
	{
		Debug.Log("AI Starts jumping");
		preventDoubleJump = false;
		yield return new WaitForSeconds(jumpDelay);
		//print ("Cpu jump activated at: " + Time.timeSinceLevelLoad);
		canJump = true;
		preventDoubleJump = true;
	}
}
