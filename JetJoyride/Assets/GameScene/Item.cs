using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
		
	//INSPECTOR VARIABLES
	//if there is no name then it is not a collectables
	
	public GameObject pfb_Popup;
	
	public AudioClip collectSound;
	
		
	public enum ItemType
	{
		COIN,
		FUEL,
		COLLECTABLE,
		BOOST,
		MAGNET
	}
	public ItemType fuelType = ItemType.COIN;
		
	public string collectableName = "";//for the objective system (to be implemented)
	
	//
	
	
	private float rotateSpeed = 5.0f;
	private Vector3 startLocalPosition;
	private float scoreValue = 1.0f;
	
	public void Reset()
	{
		transform.localPosition = startLocalPosition;
	}
	

	public void Hide()
	{
		transform.position = new Vector3(-8000,0,-8000);
	}
	
	void Awake()
	{
		collider.isTrigger = true;
	}
	
	// Use this for initialization
	void Start () {
		
		startLocalPosition = transform.localPosition;
		
		//this is where the score values are set for each item
		if (fuelType == ItemType.COIN)
		{
			scoreValue = 10.0f;
		}
		else if (fuelType == ItemType.FUEL)
		{
			scoreValue = 50.0f;	
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Rotate()
	{
		transform.Rotate(Vector3.up, rotateSpeed);
	}
	
	
	
	void Move()
	{
		//transform.position+= new Vector3(0,0,-GameManager.gameSpeed*Time.fixedDeltaTime);
	}
	
	
	void CheckPosition()
	{
		float fuelDepth = transform.position.z - Camera.main.transform.position.z;
		
		if (fuelDepth < -100)
		{
			if (fuelType == ItemType.FUEL)
			{
				GameObject.Find("GameManager").SendMessage("ResetMultiplier");	
			}
		}
	}
	
	void FixedUpdate()
	{
		Rotate();
		CheckPosition();
		
	}
	
	
	void ShowPopup()
	{
		if (pfb_Popup !=null)
		{
			Vector3 popupPosition = transform.position + new Vector3(0,3,1);
			
			GameObject textPopup = GameObject.Instantiate(pfb_Popup, popupPosition, Quaternion.identity) as GameObject;
			
			textPopup.transform.parent = Camera.main.transform;
			
			TextMesh textMesh = textPopup.GetComponent<TextMesh>();
			textMesh.text =  "+"+(scoreValue);
			
			GameObject.Destroy(textPopup, 1.0f);
		}
		
	}
	
	void PlayCollectSound()
	{
		
	}
	
	void OnTriggerEnter(Collider other) {
	
		if (other.gameObject.name == "Hero")
		{
			if (collectSound)
			{
				Debug.Log ("played sound");
				AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position);	
			}
			ShowPopup();
			if (fuelType == ItemType.COIN)
			{
				Debug.Log ("hit coin");
				
				GameObject.Find("GameManager").SendMessage("IncreaseScore", 50.0f);
			}
			else if (fuelType == ItemType.FUEL)
			{
				Debug.Log ("hit fuel");
				GameObject.Find("GameManager").SendMessage("IncreaseScore", 100.0f);
			}
			else if (fuelType == ItemType.BOOST)
			{
				other.gameObject.SendMessage("HitBoost", 2.0f);
			}
			else if (fuelType == ItemType.MAGNET)
			{
				other.gameObject.SendMessage("HitMagnet", 3.0f);	
			}
			
			Hide();
			//GameObject.Destroy(gameObject);//removed because instantiating again is slow...
		}
    }
	
}
