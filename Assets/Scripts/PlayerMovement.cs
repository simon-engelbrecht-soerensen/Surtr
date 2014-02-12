﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	//input to movement stuffs
	private Vector3 inputDir;
	private float speed;
	public float accel = 1f;
	public float drag = 1f;
	public float movementMax = 1f;
	private float moveMaxStart;

	//grounded?
	public bool grounded;	
	public float groundedCheckRange = 0.5f;

	//gravity && jump
	public float gravity = 0f;
	public float gravityForce = 6f;
	public float jumpForce = 0;
	private float jumpPower;
	public bool jumpKeyDown;
	private KeyCode jumpKey = KeyCode.Space;
	private KeyCode jumpKey2 = KeyCode.Joystick1Button0;
	private bool jumping;
	public float jumpMaxDuration = 0.3f;
	public  float jumpSlow;
	//onetime coRoutine
	private bool oneTimeCoR;
	void Start () 
	{
		moveMaxStart = movementMax;
	}
	

	void Update () 
	{

		// til hoppet skal jeg finde input dir retning(venstre el. højre).
		// i meget kort tid skal jeg checke om hop knappen er hold inde i kort eller længere tid for at finde ud af hvor langt hopet skal være(ddete skal ske undervejs i hoppet)
		// der skal være en smule styring iblandet kraften fra hoppet
		if(!grounded)
		{
			gravity = gravityForce;
		}
		else
		{
			gravity = 0;
		}

		grounded = IsGrounded();
		inputDir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

		if(Input.GetKey(jumpKey2) && !jumpKeyDown && grounded)
		{
			jumpKeyDown = true;
		}
		if(!Input.GetKey(jumpKey2))
		{
			jumpKeyDown = false;
			jumping = false;
		}

		if(!oneTimeCoR && jumpKeyDown)
		{
			StartCoroutine(Jump ());
		}

		if(jumping)
		{
			jumpPower = jumpForce;
			movementMax = jumpSlow;
		}
		else
		{
			movementMax = moveMaxStart;
			if(jumpPower > 0 && !grounded)
			{
			jumpPower -= Time.deltaTime * 20;
			}
			else
			{
				jumpPower = 0;
			}
//			jumpSlow = 0;

		}
//		if(jumpPower > 0 && !grounded)
//		{
//			jumpPower -= Time.deltaTime * 50;
//		}


	}

	void FixedUpdate()
	{
		speed = speed + accel * inputDir.magnitude * Time.deltaTime;
		speed = Mathf.Clamp(speed, 0f, movementMax);
		speed = speed - speed * Mathf.Clamp01(drag * Time.deltaTime);

		//input
		rigidbody.velocity = new Vector3(inputDir.x * speed, -gravity + jumpPower,0);

	}
		
	IEnumerator Jump()
	{
		oneTimeCoR = true;
		jumping = true;
		yield return new WaitForSeconds(jumpMaxDuration);
		jumpKeyDown = false;
		oneTimeCoR = false;
		jumping = false;
	}

	bool IsGrounded()
	{
		return  Physics.Raycast(transform.position, -Vector3.up,groundedCheckRange);
	}
}
