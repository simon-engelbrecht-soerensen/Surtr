// MyScriptEditor.cs
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[InitializeOnLoad]
[CustomEditor(typeof(ActionHandler))] 
[System.SerializableAttribute]
public class ActionDoerEditor : Editor {
//	[System.Serializable]
	public delegate void OnActionBack(GameObject target);
	public static event OnActionBack OnClickedBack;

	public delegate void OnActionBackDelete(GameObject targett);
	public static event OnActionBackDelete OnClickedBackDelete;
	public GameObject curObj;

	[SerializeField]
	static public GameObject attatchedObj;
	[SerializeField]
	static public List<GameObject> attatchedObjs = new List<GameObject>();
	[SerializeField]
	static public GameObject[] attatchedObjs2;
	[SerializeField]
	private string name;
	private string test;
	[SerializeField]
	public static ActionHandler actionDoer;


	// [SerializeField]

	//handlers

	static ActionDoerEditor()
	{
		Debug.Log ("Up and running");
		InputTakerEditor.OnClicked += Reciever;
		InputTakerEditor.OnClickedDelete += Remover;

	}

	public override void OnInspectorGUI() {
		actionDoer = (ActionHandler)target;
		curObj = Selection.activeGameObject;
		actionDoer.takeInput = EditorGUILayout.ToggleLeft(" Recieve input?", actionDoer.takeInput);
//		inputTaker.inputType = (InputTaker.InputType)EditorGUILayout.EnumPopup(inputTaker.inputType);
//		on = EditorGUILayout.Toggle(on);
//		test = EditorGUILayout.SelectableLabel(test
//		EditorGUILayout.HelpBox("test",MessageType.None);
//		EditorGUILayout.LabelField("Input connections: " + attatchedObjs.Count);

		if(actionDoer.takeInput)
		{
			if(actionDoer.attatchedObjs.Count > 0)
			{
				for(int i = 0;i<actionDoer.attatchedObjs.Count;++i) 
				{
				
					Rect r = EditorGUILayout.BeginVertical();
					GUILayout.Label(actionDoer.attatchedObjs[i].name);
	//				actionDoer.attatchedObjs[i] = (GameObject)EditorGUILayout.ObjectField(actionDoer.attatchedObjs[i], typeof(GameObject), true);
					//				GUILayout.Label(inputTaker.attatchedObjs[i].name + "lala");
					if(GUI.Button(new Rect(r.x + 150, r.y, 150,15), "delete " + actionDoer.attatchedObjs[i].name))
					{
						OnClickedBackDelete(actionDoer.gameObject); 

						actionDoer.attatchedObjs.Remove(actionDoer.attatchedObjs[i]);

						SceneView.RepaintAll();
					}
					
					EditorGUILayout.EndVertical();

				}
			}


			if(GUILayout.Button("Recieve"))
			{

				for(int i = 0;i<attatchedObjs.Count;i++)
				{
					if(!actionDoer.attatchedObjs.Contains(attatchedObjs[i]))
					{
						actionDoer.attatchedObjs.Add(attatchedObjs[i]);
					}
				}
				actionDoer.attatchedObj = attatchedObj;
				OnClickedBack(curObj); 
				attatchedObjs.Clear();
				SceneView.RepaintAll();

			}
			if(GUILayout.Button("Remove Connection"))
			{
				if(actionDoer.attatchedObj.GetComponent<InputTaker>().attatchedObjs.Contains(actionDoer.gameObject))
				{
					actionDoer.attatchedObj.GetComponent<InputTaker>().attatchedObjs.Remove(actionDoer.gameObject);
				}
				SceneView.RepaintAll();
			}

		}


//		EditorGUILayout.Space();
//		GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
//		EditorGUILayout.Space();
//
//
//
//
//		//transform position
////		actionDoer.pause = EditorGUILayout.ToggleLeft(" pause", actionDoer.pause);
//		actionDoer.pausable = EditorGUILayout.ToggleLeft(" Pausable", actionDoer.pausable);
//		actionDoer.playOnce = EditorGUILayout.ToggleLeft(" Play Once", actionDoer.playOnce);
//
////		actionDoer.callFunction = EditorGUILayout.ToggleLeft("Invoke Function", actionDoer.callFunction);
////
////		if(actionDoer.callFunction)
////		{
////			actionDoer.dynFunctionName = EditorGUILayout.TextField("Invoke Func. Name",actionDoer.dynFunctionName);
////		}
//		EditorGUILayout.Space();
//		GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
//		EditorGUILayout.Space();

//		actionDoer.transformMove = EditorGUILayout.ToggleLeft(" Transform move", actionDoer.transformMove);
//		if(actionDoer.transformMove)
//		{
//			SceneView.RepaintAll(); 
////			actionDoer.resetMove = EditorGUILayout.ToggleLeft(" Reset", actionDoer.resetMove);
//			actionDoer.pingPongMove= EditorGUILayout.ToggleLeft(" Ping Pong", actionDoer.pingPongMove);
//			actionDoer.smoothMove= EditorGUILayout.ToggleLeft(" Smooth Move", actionDoer.smoothMove);
//			actionDoer.moveStartDelay = EditorGUILayout.ToggleLeft(" Start Delay", actionDoer.moveStartDelay);
//			if(actionDoer.moveStartDelay)
//			{
//				actionDoer.moveStartDelayTime = EditorGUILayout.FloatField("Start Delay Duration: ", actionDoer.moveStartDelayTime);
//			}
//			actionDoer.moveInbetweenDelay= EditorGUILayout.ToggleLeft(" Inbetween Delay ", actionDoer.moveInbetweenDelay);
//			if(actionDoer.moveInbetweenDelay)
//			{
//				actionDoer.moveInbetweenDelayTime = EditorGUILayout.FloatField("Inbetween Delay Duration: ", actionDoer.moveInbetweenDelayTime);
//			}
//			actionDoer.moveSpeed = EditorGUILayout.FloatField("Move Speed: ", actionDoer.moveSpeed);
//			
////			if(actionDoer.resetMove)
////			{
////				actionDoer.moveToPos = new Vector3(actionDoer.gameObject.transform.position.x,actionDoer.gameObject.transform.position.y, actionDoer.gameObject.transform.position.z);
////				actionDoer.resetMove = false;
////			}
//
//			EditorGUILayout.Vector3Field("Start from vector3:", actionDoer.moveStartPos);
//
//			EditorGUILayout.Vector3Field("Move to vector3:", actionDoer.moveToPos);
//
//			Rect r = EditorGUILayout.BeginVertical();
//
//			if(GUI.Button(new Rect(r.x, r.y, 120,15),"Set start position"))
//			{
//				actionDoer.moveStartPos = actionDoer.gameObject.transform.position;
//			}
//			if(GUI.Button(new Rect(r.x + 125, r.y, 150,15),"Move to start position"))
//			{
//				actionDoer.gameObject.transform.position = actionDoer.moveStartPos;
//			}
//			if(GUI.Button(new Rect(r.x + 280, r.y, 50,15),"Reset"))
//			{
//				actionDoer.moveStartPos = new Vector3(0,0,0);
//			}
//
//			EditorGUILayout.Space();
//			EditorGUILayout.Space();
//			EditorGUILayout.EndVertical();
//
//			EditorGUILayout.Space();
//
//			Rect r2 = EditorGUILayout.BeginVertical();
//			
//			if(GUI.Button(new Rect(r2.x, r2.y, 120,15),"Set end position"))
//			{
//				actionDoer.moveToPos = actionDoer.gameObject.transform.position;
//			}
//			if(GUI.Button(new Rect(r2.x + 125, r2.y, 150,15),"Move to end position"))
//			{
//				actionDoer.gameObject.transform.position = actionDoer.moveToPos;
//			}
//			if(GUI.Button(new Rect(r2.x + 280, r2.y, 50,15),"Reset"))
//			{
//				actionDoer.moveToPos = new Vector3(0,0,0);
//			}
//			
//			EditorGUILayout.Space();
//			EditorGUILayout.Space();
//			EditorGUILayout.EndVertical();
//
//
//			EditorGUILayout.Space();
//			GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));   
//			EditorGUILayout.Space();
//
//		}
//
//
//		//transform rotation
//		actionDoer.transformRotate = EditorGUILayout.ToggleLeft(" Transform rotate", actionDoer.transformRotate);
//
//		if(actionDoer.transformRotate)
//		{
//			actionDoer.smoothRotate = EditorGUILayout.ToggleLeft(" Smooth Rotate", actionDoer.smoothRotate);
//			actionDoer.pingPongRotate = EditorGUILayout.ToggleLeft(" Ping Pong", actionDoer.pingPongRotate);
////			actionDoer.rotateFloat = EditorGUILayout.FloatField(actionDoer.rotateFloat);
////			actionDoer.rotateToAngle = EditorGUILayout.Vector3Field("Rotate to: ",actionDoer.rotateToAngle);
//
//			actionDoer.rotateStartDelay = EditorGUILayout.ToggleLeft(" Start Delay", actionDoer.rotateStartDelay);
//			if(actionDoer.rotateStartDelay)
//			{
//				actionDoer.rotateStartDelayTime = EditorGUILayout.FloatField("Start Delay Duration: ", actionDoer.rotateStartDelayTime);
//			}
//			actionDoer.rotateInbetweenDelay = EditorGUILayout.ToggleLeft(" Inbetween Delay ", actionDoer.rotateInbetweenDelay);
//			if(actionDoer.rotateInbetweenDelay)
//			{
//				actionDoer.rotateInbetweenDelayTime = EditorGUILayout.FloatField("Inbetween Delay Duration: ", actionDoer.rotateInbetweenDelayTime);
//			}
//			actionDoer.rotateSpeed = EditorGUILayout.FloatField("Move Speed: ", actionDoer.rotateSpeed);
//
//
//			EditorGUILayout.Vector3Field("Rotate from vector3:", actionDoer.rotateStartAngle);
//			
//			EditorGUILayout.Vector3Field("Rotate to vector3:", actionDoer.rotateToAngle);
//			
//			Rect r = EditorGUILayout.BeginVertical();
//			
//			if(GUI.Button(new Rect(r.x, r.y, 120,15),"Set start rotation"))
//			{
//				actionDoer.rotateStartAngle = actionDoer.gameObject.transform.rotation.eulerAngles;
//			}
//			if(GUI.Button(new Rect(r.x + 125, r.y, 150,15),"Move to start rotation"))
//			{
//				actionDoer.gameObject.transform.rotation = Quaternion.Euler(actionDoer.rotateStartAngle);
//			}
//			if(GUI.Button(new Rect(r.x + 280, r.y, 50,15),"Reset"))
//			{
//				actionDoer.rotateStartAngle = new Vector3(0,0,0);
//			}
//			
//			EditorGUILayout.Space();
//			EditorGUILayout.Space();
//			EditorGUILayout.EndVertical();
//			
//			EditorGUILayout.Space();
//			
//			Rect r2 = EditorGUILayout.BeginVertical();
//			
//			if(GUI.Button(new Rect(r2.x, r2.y, 120,15),"Set end rotation"))
//			{
//				actionDoer.rotateToAngle = actionDoer.gameObject.transform.rotation.eulerAngles;
//			}
//			if(GUI.Button(new Rect(r2.x + 125, r2.y, 150,15),"Move to end rotation"))
//			{
//				actionDoer.gameObject.transform.rotation = Quaternion.Euler(actionDoer.rotateToAngle);
//			}
//			if(GUI.Button(new Rect(r2.x + 280, r2.y, 50,15),"Reset"))
//			{
//				actionDoer.rotateToAngle = new Vector3(0,0,0);
//			}
//			
//			EditorGUILayout.Space();
//			EditorGUILayout.Space();
//			EditorGUILayout.EndVertical();
//		}

	}

	static public void Reciever(GameObject target)
	{
		attatchedObjs.Clear();
		if(!attatchedObjs.Contains(target))
		{
			attatchedObjs.Add(target);
		}
//		Debug.Log (attatchedObjs2.ToString());
		attatchedObj = target;
		Debug.Log (attatchedObj.name + "yolo");		 
	}

	static public void Remover(GameObject targett)
	{
		Debug.Log (targett);
		if(actionDoer.attatchedObjs.Contains(targett))
		{
//			Debug.Log ("REMOVE"); 
			
			actionDoer.attatchedObjs.Remove(targett);
		}
	}

//	public void OnSceneGUI () {
//		if(actionDoer.transformMove)
//		{
//			Handles.color = Color.red;
//			Handles.DrawLine(actionDoer.transform.position, actionDoer.moveToPos);
//			actionDoer.moveToPos = Handles.PositionHandle(actionDoer.moveToPos, Quaternion.identity);
////			actionDoer.moveToPos = Handles.FreeMoveHandle(actionDoer.moveToPos, Quaternion.identity, 1, Vector3.zero, Handles.DrawRectangle);
//		}
//
//		if(actionDoer.transformRotate)
//		{
//			Handles.color = new Color(1,1,1,0.5f);
////			Handles.DrawLine(actionDoer.transform.position, actionDoer.rotateToAngle);
//
////			Handles.DrawSolidArc(actionDoer.transform.position, actionDoer.transform.forward, actionDoer.transform.right, 15, 5);
//		}
//	}



}