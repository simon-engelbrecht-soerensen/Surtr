using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	private bool pauseVisible;
	// private UIPanel panel;
	public bool paused;

	public int menuSelection;
	// private UILabel menu1;
	// private UILabel menu2;
	// private UILabel menu3;
	// private UILabel menu4;

	public Color unSelected;
	public Color selected;
	public bool moved;
	void Start () {
		// panel = GetComponent<UIPanel>();
		// panel.alpha = 0;
		// menu1 = GameObject.Find("Menu1").GetComponent<UILabel>();
		// menu2 = GameObject.Find("Menu2").GetComponent<UILabel>();
		// menu3 = GameObject.Find("Menu3").GetComponent<UILabel>();
		// menu4 = GameObject.Find("Menu4").GetComponent<UILabel>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(paused)
		{
			if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				menuSelection ++;
			}
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				menuSelection --;
			}
			if(Input.GetAxis("Vertical") < 0.8f && Input.GetAxis("Vertical") > -0.8f)
			{
				moved = false;
			}
			if(Input.GetAxis("Vertical") > 0.8f)
			{
				if(!moved)
				{
					moved = true;
					menuSelection --;
				}
			}
			if(Input.GetAxis("Vertical") < -0.8f)
			{
				if(!moved)
				{
					moved = true;
					menuSelection ++;
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.Escape)) 
		{
			togglePause();
		}
		if(paused)
		{
			if(menuSelection == 0)
			{
				//menu 1
				if(Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.B))
				{
					togglePause();
				}
				// menu1.color = selected;
				// menu2.color = unSelected;
				// menu3.color = unSelected;
				// menu4.color = unSelected;

			}
			if(menuSelection == 1)
			{
				if(Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.B))
				{
					togglePause();
					Application.LoadLevel(Application.loadedLevel);
				}
				//menu 2
				// menu2.color = selected;
				// menu3.color = unSelected;
				// menu1.color = unSelected;
				// menu4.color = unSelected;

			}
			if(menuSelection == 2)
			{
				if(Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.B))
				{
					togglePause();
					Application.LoadLevel("House");
				}
				//menu 3
				// menu3.color = selected;
				// menu2.color = unSelected;
				// menu1.color = unSelected;
				// menu4.color = unSelected;

			}
			if(menuSelection == 3)
			{
					if(Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.B))
				{
					togglePause();
					Application.Quit();
				}
				//menu 3
				// menu3.color = unSelected;
				// menu2.color = unSelected;
				// menu1.color = unSelected;
				// menu4.color = selected;
				
			}
		}
		if(!paused)
		{
			if(menuSelection == 0)
			{
				//menu 1
				// menu1.color = unSelected;
			}
			if(menuSelection == 1)
			{
				//menu 2

				// menu2.color = unSelected;
			}
			if(menuSelection == 2)
			{
				//menu 3
				// menu3.color = unSelected;
			}
			if(menuSelection == 3)
			{
				//menu 3
				// menu4.color = unSelected;
			}
		}
		menuSelection = Mathf.Clamp(menuSelection, 0, 3);

	}

	bool togglePause()
	{
		if(Time.timeScale == 0.0001f)
		{
			Time.timeScale = 1f;
			paused = false;
			// panel.alpha = 0;
			return(false);
		}
		else
		{
			// panel.alpha = 1;
			Time.timeScale = 0.0001f;
			paused = true;
			return(true);    
		}
	}
}
