using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Rigidbody))]
public class Enemy : MonoBehaviour {
	
	
	public enum EnemyType
	{
		INCOMING,
		SAMEDIRECTION
	}
	public EnemyType enemyType = EnemyType.SAMEDIRECTION;
	
	private float speed = 100.0f;
	
	private bool isPaused = false;
	
	private Hero heroObject;
	
	
	// Use this for initialization
	void Start () {
	
		heroObject = GameObject.Find("Hero") .GetComponent<Hero>();
		
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
		
		
		if (transform.position.z > Camera.main.transform.position.z)
		{
			transform.position-=new Vector3(0,0, Time.fixedDeltaTime*speed);
		}	
	}
	
	void OnTriggerEnter(Collider other) {
		

		if (other.gameObject.name == "Hero")
		{
			
			gameObject.transform.position = new Vector3(-800,0,0);//put the thing offscreenf
			
			heroObject.Hurt();
			
		}
	
		
		
	}
			
	
}
