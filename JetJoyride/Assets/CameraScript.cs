using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))]
public class CameraScript : MonoBehaviour {
	
		
	public float followDistance = 90.0f;
	
	private Hero targetHero;
	
	// Use this for initialization
	void Start () {
		
		targetHero = GameObject.Find("Hero").GetComponent<Hero>();
	}
	
	float rotSpeed = 2.5f;
	float moveSpeed = 8.0f;
	void FixedUpdate()
	{
		if (targetHero != null)
		{
			if (!targetHero.isDead)
			{
			transform.position = new Vector3(transform.position.x,transform.position.y, targetHero.transform.position.z - followDistance);
			}
			//transform.position = new Vector3(targetHero.transform.position.x,targetHero.transform.position.y + 100, targetHero.transform.position.z - followDistance);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
