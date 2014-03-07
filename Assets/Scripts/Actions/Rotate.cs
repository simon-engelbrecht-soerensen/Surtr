﻿using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	[SerializeField]
	public bool transformRotate; 
	[SerializeField]
	public Vector3 rotateToAngle;
	[SerializeField]
	public Vector3 rotateStartAngle = Vector3.zero;
	[SerializeField]
	public bool rotateStartDelay = false;
	[SerializeField]
	public float rotateStartDelayTime;
	[SerializeField]
	public bool smoothRotate;
	[SerializeField]
	public bool pingPongRotate;
	[SerializeField]
	public float rotateSpeed = 1f;
	[SerializeField]
	public bool rotateInbetweenDelay;
	[SerializeField]
	public float rotateInbetweenDelayTime;

	public bool pauseable;
	public bool pause;
	public bool playOnce;
	public bool started;
	// Use this for initialization
	void Awake()
	{
		gameObject.transform.rotation = Quaternion.Euler(rotateStartAngle);
		ActionHandler actionHandler =  GetComponent<ActionHandler>();
		actionHandler.TakeAction += TransformRotationT;
		//		actionHandler = GetComponent<ActionHandler>();
		
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		if(started && pauseable)
		{
			if(PlayerSwitch.fadeFromForm)
			{
				pause = !pause;
			}
		}
	}
	private void TransformRotationT()
	{
		if(!started)
		{
			StartCoroutine(TransformRotation());
		}
		started = true;
	}
	IEnumerator TransformRotation()
	{
		//		Debug.Log ("TEST");
		bool onOff = true;
		float mTime = 0; 
		//		Vector3 startRot = Quaternion.Euler(this.transform.eulerAngles);
		//		Quaternion startRot = Quaternion.Euler(this.transform.eulerAngles);
		bool oneWay = false;
		bool delay = true;
		
		
		
		//		
		if(rotateStartDelay)
		{
			yield return new WaitForSeconds(rotateStartDelayTime);
		}
		if(!pingPongRotate)
		{
			while(onOff)
			{
				if(!pause)
				{
					if(mTime < 1f)
					{
						//						started = true;
						mTime += Time.deltaTime * rotateSpeed;
						if(!smoothRotate)
						{
							this.transform.rotation = Quaternion.Lerp(Quaternion.Euler(rotateStartAngle),Quaternion.Euler(rotateToAngle),mTime);
						}
						else
						{
							float smoothStep = Mathf.SmoothStep(0.0f, 1.0f, mTime);
							this.transform.rotation = Quaternion.Lerp(Quaternion.Euler(rotateStartAngle),Quaternion.Euler(rotateToAngle), smoothStep);
						}
					}
					else
					{
						if(!playOnce)
						{
							started = false;
						}
						onOff = false;
					}
				}
				yield return null;
			}
		}
		if(pingPongRotate)
		{
			while(onOff)
			{
				if(!pause)
				{
					if(mTime > 1 && !oneWay)
					{
						oneWay = true;
						mTime = 0;
						if(rotateInbetweenDelay)
						{
							delay = false;
							yield return new WaitForSeconds(rotateInbetweenDelayTime);
							delay = true;
						}
					}
					if(mTime > 1 && oneWay)
					{
						oneWay = false;
						mTime = 0;
						if(rotateInbetweenDelay)
						{
							delay = false;
							yield return new WaitForSeconds(rotateInbetweenDelayTime);
							delay = true;
						}
					}
					if(delay)
					{
						if(!oneWay)
						{
							mTime += Time.deltaTime * rotateSpeed;
							if(!smoothRotate)
							{
								this.transform.rotation = Quaternion.Lerp(Quaternion.Euler(rotateStartAngle),Quaternion.Euler(rotateToAngle),mTime);
								//							this.transform.position = Vector3.Lerp(moveStartPos, moveToPos, mTime);
							}
							else
							{
								float smoothStep = Mathf.SmoothStep(0.0f, 1.0f, mTime);
								this.transform.rotation = Quaternion.Lerp(Quaternion.Euler(rotateStartAngle),Quaternion.Euler(rotateToAngle), smoothStep);
								//							this.transform.position = new Vector3(Mathf.SmoothStep(moveStartPos.x, moveToPos.x, mTime), Mathf.SmoothStep(moveStartPos.y, moveToPos.y, mTime), Mathf.SmoothStep(moveStartPos.z, moveToPos.z, mTime));
							}
						}
						if(oneWay)
						{
							mTime += Time.deltaTime * rotateSpeed;
							if(!smoothRotate)
							{
								this.transform.rotation = Quaternion.Lerp(Quaternion.Euler(rotateToAngle),Quaternion.Euler(rotateStartAngle),mTime);
								//							this.transform.position = Vector3.Lerp(moveToPos, moveStartPos, mTime);
							}
							else
							{
								float smoothStep = Mathf.SmoothStep(0.0f, 1.0f, mTime);
								this.transform.rotation = Quaternion.Lerp(Quaternion.Euler(rotateToAngle),Quaternion.Euler(rotateStartAngle), smoothStep);
								//							this.transform.position = new Vector3(Mathf.SmoothStep(moveToPos.x, moveStartPos.x, mTime), Mathf.SmoothStep(moveToPos.y, moveStartPos.y, mTime), Mathf.SmoothStep(moveToPos.z, moveStartPos.z, mTime));
							}
							
						}
					}
				}
				
				yield return null;
			}
		}
		
	}
}
