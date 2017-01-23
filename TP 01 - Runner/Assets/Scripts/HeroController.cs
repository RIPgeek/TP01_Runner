using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
	public float speed = 3f;
	public float jumpPower = 2f;

	public Rigidbody2D rb;
	public GameObject head, feet;
	public LayerMask ground;
	public Animator animator;
	public Collider2D generalCollider;
	public Collider2D slidingCollider;
	public GameController gameController;
	public float slideDuration = 1f;

	//private float jumpTime = 0f;
	private float slideTime = 0f;
	private bool isJumping = false,
				 isSliding = false,
				 spaceFreed = true,
				 shiftFreed = true;

	private Vector3 startingPosition;

	private bool touchGround;
	private bool dead;

	// Use this for initialization
	void Start()
	{
		startingPosition = transform.position;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Collider2D collider = collision.collider;

		if (collider.tag == "Ground")
		{
			Vector2 contactNormal = collision.contacts[0].normal;
			
			if (contactNormal.x < -0.25f || contactNormal.y < -0.25f)
				die();
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "CheckPoint")
		{
			startingPosition = transform.position;
		}
		if(collider.tag == "Finish")
		{
			gameController.win();
		}
	}

	public void restart()
	{
		transform.position = startingPosition;
		dead = false;
		isJumping = false;
		isSliding = false;
		spaceFreed = true;
		shiftFreed = true;
		slideTime = 0.0f;
		animator.SetBool("Restart", true);
		//transform.eulerAngles = startingPosition.eulerAngles;
		//transform.localScale = startingPosition.localScale;
	}

	void die()
	{
		gameController.die();
		dead = true;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (dead)
		{
			animator.SetBool("Dead", true);
			return;
		}

		animator.SetBool("Restart", false);
		animator.SetBool("Dead", false);
		touchGround = Physics2D.OverlapCircle(feet.transform.position, 0.51f, ground);

		rb.velocity = new Vector2(speed, rb.velocity.y);

		if (transform.position.y < -5)
			die();

		if (Input.GetKey(KeyCode.Space) && !isJumping && spaceFreed)
		{
			spaceFreed = false;
			if (touchGround)
			{
				rb.velocity = new Vector2(rb.velocity.x, jumpPower);
				isJumping = true;
			}
		}

		if (!Input.GetKey(KeyCode.Space))
			spaceFreed = true;

		if (Input.GetKey(KeyCode.LeftShift) && !isJumping)
		{
			if (touchGround)
			{
				slideTime = slideDuration;
				isSliding = true;
			}
		}

		if (rb.velocity.y <= 0 && isJumping)
			isJumping = false;

		if (!touchGround)
			slideTime = 0;

		if (slideTime > 0)
		{
			slideTime -= Time.fixedDeltaTime;
		}
		else
			isSliding = false;

		if (isSliding)
		{
			slidingCollider.enabled = true;
			generalCollider.enabled = false;
		}
		else
		{
			slidingCollider.enabled = false;
			generalCollider.enabled = true;
		}
		//if (jumpTime > 0)
		//{
		//	rb.velocity += new Vector2(0, jumpPower);
		//	jumpTime -= Time.fixedDeltaTime;
		//}

		animator.SetBool("Ground", touchGround);
		animator.SetBool("Moving", true);
		animator.SetBool("Sliding", isSliding);
	}
}
