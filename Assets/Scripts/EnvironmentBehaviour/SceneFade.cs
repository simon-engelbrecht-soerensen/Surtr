using UnityEngine;
using System.Collections;

public class SceneFade : MonoBehaviour {

	private bool startFade;

	public bool fadeAtSceneStart;
//	[HideInInspector]
	public bool fadeOutScene;
//	[HideInInspector]
	public bool fadeInScene;
	public bool fadeBlack;
	public bool fadeWhite;
	private GameObject whitePlane;
	private GameObject blackPlane;
	private Color whiteColorStart;
	private Color blackColorStart;
	public float fadeSpeed =3;
	public string nextScene;
	private ActionHandler actionHandler;
	//private UIPanel nguiUI;
	void Start () 
	{
		if(GameObject.Find("UI Root"))
		{
			
			//nguiUI = GameObject.Find("UI Root").GetComponent<UIPanel>();
		}

		whitePlane = GameObject.Find("WhiteFade");
		blackPlane = GameObject.Find("BlackFade");
		if(fadeAtSceneStart)
		{
			if(fadeBlack)
			{
//				blackPlane.renderer.material.color = Color.black;
				blackPlane.GetComponent<Renderer>().enabled = true;
				StartCoroutine("FadeInScene");

			}
			if(fadeWhite)
			{
//				whitePlane.renderer.material.color = Color.white;
				whitePlane.GetComponent<Renderer>().enabled = true;
				StartCoroutine("FadeInScene");
			}
		}
		whiteColorStart = whitePlane.GetComponent<Renderer>().material.color;
		blackColorStart = blackPlane.GetComponent<Renderer>().material.color;
		actionHandler =  GetComponent<ActionHandler>();
		if(actionHandler)
		{

			actionHandler.TakeAction += FadeTrigger;
		}

	}
	

	void Update () 
	{
		if(!actionHandler)
		{
			if(!startFade)
			{

				if(fadeOutScene)
				{
					startFade = true;
					StartCoroutine("FadeOutScene");

				}
				if(fadeInScene)
				{
					startFade = true;
					StartCoroutine("FadeInScene");
				}
			}
		}


	}
	void FadeTrigger(GameObject gObj, bool stop)
	{

			if(!startFade)
			{
				
				if(fadeOutScene)
				{
					startFade = true;
					StartCoroutine("FadeOutScene");
					
				}
				if(fadeInScene)
				{
					startFade = true;
					StartCoroutine("FadeInScene");
				}
			}
		
	}

	IEnumerator FadeInScene()
	{
		bool onOff = true;
		float mTime = 0;

		while(onOff)
		{
			mTime += Time.deltaTime	/ fadeSpeed;
			if(mTime< 1)
			{
//				if(nguiUI)
//				{
//					nguiUI.alpha = Mathf.Lerp(0, 1, mTime);
//				}
				if(fadeWhite)
				{
					whitePlane.GetComponent<Renderer>().material.color = Color.Lerp(new Color(Color.white.r, Color.white.g, Color.white.b, 1), new Color(whiteColorStart.r, whiteColorStart.g, whiteColorStart.b, 0), mTime);
				}
				if(fadeBlack)
				{
					blackPlane.GetComponent<Renderer>().material.color = Color.Lerp(new Color(Color.black.r, Color.black.g, Color.black.b, 1), new Color(blackColorStart.r, blackColorStart.g, blackColorStart.b, 0), mTime);
				}
			}
			else
			{
				if(fadeAtSceneStart)
				{
					fadeAtSceneStart = false;
				}
				fadeInScene = false;
				onOff = false;
				startFade = false;
				if(fadeWhite)
				{
					whitePlane.GetComponent<Renderer>().enabled = false;
					fadeWhite =	false;
				}
				if(fadeBlack)
				{
					blackPlane.GetComponent<Renderer>().enabled = false;
					fadeBlack = false;
				}
			}
			yield return null;
		}
		//fade plane væk
	}

	IEnumerator FadeOutScene()
	{
		//fade plane frem
		bool onOff = true;
		float mTime = 0;

		if(fadeWhite)
		{
			whitePlane.GetComponent<Renderer>().enabled = true;
			whitePlane.GetComponent<Renderer>().material.color = new Color(whiteColorStart.r, whiteColorStart.g, whiteColorStart.b, 0);
		}
		if(fadeBlack)
		{
			blackPlane.GetComponent<Renderer>().enabled = true;
			blackPlane.GetComponent<Renderer>().material.color = new Color(blackColorStart.r, blackColorStart.g, blackColorStart.b, 0);
		}
		while(onOff)
		{
			mTime += Time.deltaTime	/ fadeSpeed;
			if(mTime< 1)
			{
				// if(nguiUI)
				// {
				// 	if(nguiUI.alpha != 0)
				// 	{
				// 		nguiUI.alpha = Mathf.Lerp(1, 0, mTime);
				// 	}
				// }
				if(fadeWhite)
				{
					whitePlane.GetComponent<Renderer>().material.color = Color.Lerp(new Color(Color.white.r, Color.white.g, Color.white.b, 0), new Color(whiteColorStart.r, whiteColorStart.g, whiteColorStart.b, 1), mTime);
				}
				if(fadeBlack)
				{
					blackPlane.GetComponent<Renderer>().material.color = Color.Lerp(new Color(Color.black.r, Color.black.g, Color.black.b, 0), new Color(blackColorStart.r, blackColorStart.g, blackColorStart.b, 1), mTime);
				}
			}
			else
			{
				fadeOutScene = false;
				startFade = false;
				if(fadeWhite)
				{
					fadeWhite = false;
				}
				if(fadeBlack)
				{
					fadeBlack = false;
				}
				onOff = false;
				if(nextScene != null)
				{
					Application.LoadLevel(nextScene);
				}
//				
			}
			yield return null;
		}
	}

}
