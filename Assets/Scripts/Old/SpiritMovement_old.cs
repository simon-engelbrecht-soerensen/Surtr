using UnityEngine;
using System.Collections;

public class SpiritMovement_old : MonoBehaviour {
	
	//input to movement stuffs
	private Vector3 inputDir;
	public float speed = 5;
	public float accel = 1f;
	public float drag = 1f;
	public float movementMax = 1f;
	private float moveMaxStart;

	private float curVelX;
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

	private Quaternion rightRot;
	private Quaternion leftRot;
	//onetime coRoutine
	private bool oneTimeCoR;
	private int dirIndicator = 1;
	public float diluteInput = 6;
	void Start () 
	{
		rightRot = Quaternion.Euler(0,-90,0);
		leftRot = Quaternion.Euler(0,90,0);
		jumpSlow = movementMax;
		moveMaxStart = movementMax;
		gravity = gravityForce;
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
//		inputDir = inputDir.normalized;
		
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
//						jumpSlow = 0;
			
		}
//				if(jumpPower > 0 && !grounded)
//				{
//					jumpPower -= Time.deltaTime * 50;
//				}

		if(inputDir.x > 0)
		{
			dirIndicator = 1;
		}
		if(inputDir.x < 0)
		{
			dirIndicator = -1;
		}
		
	}
	
	void FixedUpdate()
	{

			GetComponent<Rigidbody>().AddForce(new Vector3(inputDir.x * speed, -gravity, 0), ForceMode.VelocityChange);
//			rigidbody.AddForce(inputDir.normalized * speed, ForceMode.Impulse);
			GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpPower,0), ForceMode.Impulse);

		if(GetComponent<Rigidbody>().linearVelocity.y < 0)
		{
			//falling
			GetComponent<Rigidbody>().AddForce(new Vector3(0, inputDir.y/diluteInput,0), ForceMode.VelocityChange);
		}
		Debug.Log(GetComponent<Rigidbody>().linearVelocity.y);

//		}
//		speed = speed + accel * inputDir.magnitude * Time.deltaTime;
//		speed = Mathf.Clamp(speed, 0f, movementMax);
//		speed = speed - speed * Mathf.Clamp01(drag * Time.deltaTime);
		
		//input
//		rigidbody.velocity = new Vector3(inputDir.x * speed, -gravity + jumpPower,0);
//		rigidbody.velocity = new Vector3(dirIndicator * speed, inputDir.y * speed + -gravity + jumpPower,0);

//		rigidbody.velocity = new Vector3(-transform.forward.x * speed, inputDir.y * speed,0);
//		Debug.Log ((int)inputDir.normalized.x);

//		rigidbody.velocity = new Vector3(Mathf.SmoothDamp(rigidbody.velocity.x, inputDir.x * speed,ref curVelX,0.1f), inputDir.y * speed + -gravity,0);
		
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
