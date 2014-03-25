﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[HideInInspector]
	public bool activeMovement = true;
	private CharacterMotor motor;
	public float pushPower = 2.0f;
	public float weight = 6.0f;
	void Start () 
	{
		activeMovement = true;
		motor = GetComponent<CharacterMotor>();

	}
	

	void Update () 
	{

		// til hoppet skal jeg finde input dir retning(venstre el. højre).
		// i meget kort tid skal jeg checke om hop knappen er hold inde i kort eller længere tid for at finde ud af hvor langt hopet skal være(ddete skal ske undervejs i hoppet)
		// der skal være en smule styring iblandet kraften fra hoppet

		if(activeMovement)
		{
			motor.inputMoveDirection = Vector3.right * Input.GetAxis("Horizontal");
			motor.inputJump = Input.GetKey(KeyCode.JoystickButton0);

		}
		else
		{
			motor.inputMoveDirection = Vector3.zero;
		}


	}

	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		Rigidbody body = hit.collider.attachedRigidbody;
		
		Vector3 force = Vector3.zero;
		
		// no rigidbody
		if (body == null || body.isKinematic) { return; }
		
		// We use gravity and weight to push things down, we use
		// our velocity and push power to push things other directions
//		if (hit.moveDirection.y < -0.3f) {
//			force = new Vector3 (0, -0.5f, 0) * motor.movement.gravity * weight;
//		} else {
			force = hit.controller.velocity * pushPower;
//		}
		
		// Apply the push
		body.AddForceAtPosition(force, hit.point);
	}
	
}
		



