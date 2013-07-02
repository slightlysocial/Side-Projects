using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {
		
	public GameObject[] enemyList;
	
	public GameObject[] incomingList;
	
	private int numEnemies = 10;
	
	// Use this for initialization
	void Start () {
		
	}
	
	int currentEnemyIndex = 0;//the enemy that is current next in line in the pool
	
	float spawnInterval = 3.0f;//have a bird spawn every 2 seconds
	
	float spawnTimer = 4.0f;
	
	int currentIncomingIndex = 0;
	
	float incomingInterval = 2.0f;
	
	float incomingTimer = 0.0f;
	
	void FixedUpdate()
	{
		
		spawnTimer+=Time.fixedDeltaTime;
		
		if (spawnTimer > spawnInterval)
		{
			spawnTimer = 0.0f;
			
			SpawnEnemy();
		}
		
		incomingTimer+=Time.fixedDeltaTime;
		
		if (incomingTimer > incomingInterval)
		{
			incomingTimer = 0.0f;
			
			
		}
	}	
	
	void SpawnIncoming()
	{
		if (incomingList.Length <= 0)
			return;
				
		GameObject currentEnemyObject = enemyList[currentIncomingIndex];
		
		float xPosition = 0.0f;
		
		int randomNumber = Random.Range(0,3);
		
		if (randomNumber == 0)
		{
			xPosition = GameManager.leftPosX;
		}	
		else if (randomNumber == 1)
		{
			xPosition = GameManager.middlePosX;
		}
		else if (randomNumber == 2)
		{
			xPosition = GameManager.rightPosX;	
		}
		
		currentEnemyObject.transform.position = new Vector3(xPosition,0, Camera.main.transform.position.z + Camera.main.farClipPlane);
		currentEnemyObject.SetActive(true);
		
		currentIncomingIndex++;
		currentIncomingIndex%=incomingList.Length;
	}
	
	
	void SpawnEnemy()
	{
		if (enemyList.Length <= 0)
			return;
				
			
		GameObject currentEnemyObject = enemyList[currentEnemyIndex];
		
		
		float xPosition = 0.0f;
		
		int randomNumber = Random.Range(0,3);
		
		if (randomNumber == 0)
		{
			xPosition = GameManager.leftPosX;
		}	
		else if (randomNumber == 1)
		{
			xPosition = GameManager.middlePosX;
		}
		else if (randomNumber == 2)
		{
			xPosition = GameManager.rightPosX;	
		}
		
		currentEnemyObject.transform.position = new Vector3(xPosition,0, Camera.main.transform.position.z + Camera.main.farClipPlane);
		currentEnemyObject.SetActive(true);
		
		currentEnemyIndex++;
		currentEnemyIndex%=enemyList.Length;
	
	}
	
	// Update is called once per frame
	void Update () {
	
				
	}
}
