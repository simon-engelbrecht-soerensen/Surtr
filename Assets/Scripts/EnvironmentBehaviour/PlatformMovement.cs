using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody>().linearVelocity = new Vector3(0.3f, 0f, 0f);
	}
}
