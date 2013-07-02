using UnityEngine;
using System.Collections;

public class AutoScaleBG : MonoBehaviour {
	
	
	
	Vector3 startScale;
	
	// Use this for initialization
	void Start () {
		
		startScale = transform.localScale;
		
		float screenRatio = Screen.currentResolution.height/Screen.currentResolution.height;
		
		Debug.Log("screen ratio:" + screenRatio);
		
		float scale = screenRatio / 0.666f ;
		
		Debug.Log("scale" + scale);
		
		transform.localScale = new Vector3(startScale.x*scale, startScale.y, startScale.z);
			
		
		
		
	}
	
	// Update is called once per frame
	void Update () {

		
		
	}
}
