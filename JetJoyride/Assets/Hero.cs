using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (SwipeControl))]
public class Hero : MonoBehaviour {
	
	public int START_SHIELDS = 0;
	public int MAX_SHIELDS = 5;
	
	public static int selectedShip = 0;//the selected index which is set in the settings menu
	public GameObject[] shipPrefabs;
	public GameObject deathPrefab;
	
	
	float gridSize = 1.0f;
	
	bool isHigh = false; 
	
	Vector2 targetPosition;
	
	private static bool isPaused = false;
	
	[HideInInspector]
	public int shields = 0;
	

	

	float smoothTime = 0.2f;
	
	float velocityX;
	float velocityY;
	
	float velocityAngle;
	
	float currAngle;
	
	float defaultSpeed = 200.0f;
	float boostSpeed = 600.0f;
	float currentSpeed = 200.0f;
	
	float speedAdjustTime = 5.0f;//time to take to adjust speed to target speed
	
	float magnetTime = 0.0f;
	float boostTime = 0.0f;
	
	Item[] coins; //list of all the coins
	
	[HideInInspector]
	public bool isDead = false;
	
	
	private GameObject shipObject;
	
	void Start()
	{
		Time.fixedDeltaTime = 0.03f;
		shields = START_SHIELDS;
		coins = FindObjectsOfType(typeof(Item)) as Item[];
		
		
		shipObject = GameObject.Instantiate(shipPrefabs[selectedShip]) as GameObject;
		shipObject.layer = LayerMask.NameToLayer("Default");
		shipObject.transform.parent = transform;
		shipObject.transform.localPosition = Vector3.zero;
		shipObject.transform.rotation = Quaternion.identity;
		
	}
	
	void Update()
	{
		
	}
	
	public void Hurt()
	{
	
		if (boostTime <= 0)
		{
			if (shields > 0)
			{
				
				shields--;
			}
			else
			{
				
				GameObject explosion = GameObject.Instantiate(deathPrefab,transform.position, Quaternion.identity) as GameObject;
				
				GameObject.Destroy(explosion, 1.5f);
				
				isDead = true;
				transform.position = new Vector3(-900,0,0);
				Debug.Log ("Hurtttt");
				GameObject.Find("GameManager").SendMessage("OnGameOver",SendMessageOptions.DontRequireReceiver);	
			}
		}
		else
		{
			//show an explosion to show that we killed a bird	
			
		}
	}
	
	
	
	void HitBoost(float timeValue)
	{
		boostTime = timeValue;		
	}
	
	void HitMagnet(float timeValue)
	{
		magnetTime = timeValue;
	}
	
	void UpdateBoost()
	{
		if (boostTime > 0)
		{
			boostTime-=Time.fixedDeltaTime;	
		}
		
		
	}
	
	void UpdateMagnet()
	{
		if (magnetTime >0)
		{
			//move all of the coins closer to the hero!
			foreach(Item coin in coins)
			{
				if (coin.fuelType == Item.ItemType.COIN)
				{
					Vector3 difference = transform.position - coin.transform.position;	
					float magnitude = difference.magnitude;
					
					if (magnitude < 2000)
					{
						coin.transform.position+=difference.normalized*12.0f;
					}
				}
			}
			magnetTime-=Time.fixedDeltaTime;
		}
		
		
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (isPaused || isDead)
		{
			gameObject.rigidbody.isKinematic = true;
			return;
		}
		else
		{
			gameObject.rigidbody.isKinematic = false;	
		}
		
		
		UpdateMagnet();
		UpdateBoost();
		
		float xSpeed = 0.0f;
		
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			xSpeed = Input.GetAxis("Horizontal")*3.0f;
		}
		else
		{
			xSpeed = Input.acceleration.x*2.0f;
		}	
	
		
		if (boostTime > 0)
		{
			currentSpeed = Mathf.Lerp(currentSpeed, boostSpeed, speedAdjustTime*Time.fixedDeltaTime);
		}
		else
		{
			currentSpeed = Mathf.Lerp(currentSpeed, defaultSpeed, speedAdjustTime*Time.fixedDeltaTime);
		}
		
		this.rigidbody.velocity = new Vector3(0, 0,currentSpeed);
		
		float newPosY = Mathf.SmoothDamp( this.transform.position.y, targetPosition.y,  ref velocityY, smoothTime);
		float newPosX = Mathf.SmoothDamp( this.transform.position.x, targetPosition.x,  ref velocityX, smoothTime);
		
		float rotation = 0.0f;
		
		this.transform.position = new Vector3(newPosX, newPosY, this.transform.position.z);
		
		this.transform.rotation = Quaternion.Euler(0,0,-xSpeed*4);
	}
	
	void OnPauseGame()
	{
		isPaused = true;
		Debug.Log("Paused Hero");
		
	}
	
	void OnResumeGame()
	{
		isPaused = false;	
	}
	
	
	void OnSwipeRight()
	{
		if (targetPosition.x < GameManager.middlePosX)
		{
			targetPosition = new Vector2(GameManager.middlePosX, targetPosition.y);
		}
		else if (targetPosition.x < GameManager.rightPosX)
		{
			targetPosition = new Vector2(GameManager.rightPosX, targetPosition.y);
		}
	}
	
	void OnSwipeLeft()
	{
		if (targetPosition.x > GameManager.middlePosX)
		{
			targetPosition = new Vector2(GameManager.middlePosX, targetPosition.y);
		}
		else if (targetPosition.x > GameManager.leftPosX)
		{
			targetPosition = new Vector2(GameManager.leftPosX, targetPosition.y);
		}
	}
	
	void OnSwipeUp()
	{
		targetPosition = new Vector2(targetPosition.x, GameManager.highPosY);
	}
	
	void OnSwipeDown()
	{
		targetPosition = new Vector2(targetPosition.x, GameManager.lowPosY);
	}
		
}
