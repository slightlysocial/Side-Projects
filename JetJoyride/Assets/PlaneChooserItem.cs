using UnityEngine;
using System.Collections;

public class PlaneChooserItem : MonoBehaviour {
	
	[HideInInspector]
	public Vector3 targetPosition;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate()
	{
		transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.fixedDeltaTime*7.0f);
	}
	
}
