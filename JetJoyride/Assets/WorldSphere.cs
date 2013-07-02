using UnityEngine;
using System.Collections;

public class WorldSphere : MonoBehaviour {

	private float rotationSpeed = -30.0f;
	
	private bool isPaused = false;
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
			return;
		
		this.transform.Rotate(rotationSpeed*Time.fixedDeltaTime,0,0);
		
	
	}
	
	
}
