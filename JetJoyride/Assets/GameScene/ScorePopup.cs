using UnityEngine;
using System.Collections;

public class ScorePopup : MonoBehaviour {
	
	public float upSpeed = 5.0f;
	
	private float upAcc = 0.08f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	

		
	}
	
	void FixedUpdate()
	{
		upSpeed+=upAcc;
		transform.position += new Vector3(0, upSpeed*Time.fixedDeltaTime, 0);
	}
}
