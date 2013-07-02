using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour {
	
	public Camera cloudCamera;
	
	public static float farPosition;

	private float cloudSpeed = 100.0f;
	
	private bool isPaused = false;
	
	
	// Use this for initialization
	void Start () {
		farPosition = cloudCamera.farClipPlane;
	}
	
	void ResetCloud()
	{
	
		transform.position = new Vector3(transform.position.x, transform.position.y, farPosition);
		
		
		
	}
	
	void OnPauseGame()
	{
		isPaused = true;	
	}
	
	
	void OnResumeGame()
	{
		isPaused = false;
	}
	
	
	void FixedUpdate()
	{
		
		if (isPaused)
		{
			return;
		}
		
		
		if (transform.position.z < cloudCamera.transform.position.z - cloudCamera.nearClipPlane)
		{
			ResetCloud();
		}
		else
		{
			transform.position-=new Vector3(0,0, Time.fixedDeltaTime*cloudSpeed);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
