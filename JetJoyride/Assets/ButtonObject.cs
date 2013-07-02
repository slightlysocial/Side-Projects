using UnityEngine;
using System.Collections;

public class ButtonObject : MonoBehaviour {
	
	public string message;
	public GameObject target;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
	
	void Selected()
	{
		target.SendMessage(message,SendMessageOptions.DontRequireReceiver);
		Debug.Log("sent message: " + message);
	}
}
