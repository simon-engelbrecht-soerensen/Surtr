using UnityEngine;
using System.Collections;

public class ShowOnTrigger : MonoBehaviour {

	// Use this for initialization
	public GameObject thingToShow;
	private Color startCol;
	// private UILabel label;
	void Start () 
	{
		// if(thingToShow.GetComponent<UILabel>())
		// {
		// 	// label = thingToShow.GetComponent<UILabel>();
		// 	// startCol = label.color;
		// 	// label.color = new Color(startCol.r, startCol.g, startCol.b, 0);
		// }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			StartCoroutine("FadeIn");
//			thingToShow.renderer.enabled = true;

		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			StartCoroutine("FadeOut");
			//			thingToShow.renderer.enabled = true;
			
		}
	}

	IEnumerator FadeIn()
	{
		bool onOff = true;
		float mTime = 0;
		while(onOff)
		{
			if(mTime< 1)
			{
				mTime += Time.deltaTime;
				// label.color = Color.Lerp(new Color(startCol.r, startCol.g, startCol.b, 0), startCol, mTime);
			}
			else
			{
				onOff = false;
			}
			yield return null;
		}
	}
	IEnumerator FadeOut()
	{
		bool onOff = true;
		float mTime = 0;
		while(onOff)
		{
			if(mTime< 1)
			{
				mTime += Time.deltaTime;
				// label.color = Color.Lerp(startCol, new Color(startCol.r, startCol.g, startCol.b, 0), mTime);
			}
			else
			{
				onOff = false;
			}
			yield return null;
		}
	}
}
