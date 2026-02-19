using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ActionHandler))]

public class FloatChar : MonoBehaviour {
	public Animator anim;
	private Transform charGfx;
	private CharacterMotor pMotor;
	private PlayerMovement pMove;
	private ActionHandler actionHandler;
	private PlayerSwitch pSwitch;
	private Transform spirit;
	public bool floating;
	private bool on;

	private SceneFade sceneFade;
	private bool fall;
	public bool turnOnFloat;
	public bool turnOffFloat;
	private float startFallspeed;
	//public Vignetting vig;
	public GameObject leftCol;
	private AudioSource waterSound;
	public AudioSource stormSound;
	void Start () 
	{
		waterSound = GameObject.Find("WaterSound").GetComponent<AudioSource>();
		waterSound.enabled = false;
		//vig = Camera.main.GetComponent<Vignetting>();
		spirit = Transform.FindObjectOfType<SpiritMovement>().gameObject.transform;

		anim = GetComponentInChildren<Animator>();
		charGfx = anim.gameObject.transform;
		pMotor = GetComponent<CharacterMotor>();
		pMove = GetComponent<PlayerMovement>();
		startFallspeed = pMotor.movement.maxFallSpeed;
		pSwitch = GetComponent<PlayerSwitch>();
		actionHandler =  GetComponent<ActionHandler>();
		if(actionHandler)
		{
			actionHandler.TakeAction += Floater;
		}
		sceneFade = Camera.main.GetComponent<SceneFade>();
		leftCol.GetComponent<Collider>().enabled=false;
	}
	
	void Update () 
	{
		anim.SetBool("floating", floating);
		anim.SetBool("fall", fall);
	}

	void Floater(GameObject gObj, bool stop)
	{
		if(!on)
		{			
			on = true;

			StartCoroutine(FloatCR());


		}
	}

	IEnumerator FloatCR()
	{

		if(turnOnFloat)
		{
		yield return new WaitForSeconds(1.5f);
			pMotor.movement.maxForwardSpeed = 0;
			pMotor.movement.maxBackwardsSpeed = 0f;
			stormSound.enabled = false;
			waterSound.enabled = true;
			floating = true;
			pMotor.movement.maxFallSpeed = 0.2f;
			charGfx.eulerAngles = new Vector3(0, 180,0);
//			pMove.activeMovement = false;
			turnOnFloat =false;
			turnOffFloat = true;
			on = false;
			// vig.enabled = true;
			pSwitch.canGoSpirit = true;
			leftCol.GetComponent<Collider>().enabled =true;
		}
		else
		{

//			waterSound.enabled = false;

			floating = false;
			pMotor.movement.maxFallSpeed = startFallspeed;
				charGfx.eulerAngles = new Vector3(0, 90,0);
			
		
			turnOnFloat = false;
			turnOffFloat = false;
			transform.position = new Vector3(spirit.position.x, spirit.position.y, spirit.position.z);
			pSwitch.curState = !pSwitch.curState;
			// vig.enabled = false;
			fall = true;
			pMove.activeMovement = false;
			yield return new WaitForSeconds(0.5f);
			sceneFade.fadeWhite = true;
			sceneFade.fadeOutScene = true;
//			StartCoroutine("FadeToWhite");


		}
	}

	IEnumerator FadeToWhite()
	{
		float mTime = 0;
		bool onOff = true;
		while(onOff)
		{
			mTime += Time.deltaTime;
			if(mTime < 1)
			{
				//fade
			}
			else
			{
				//end scene
			}

			yield return null;
		}
	}
}
