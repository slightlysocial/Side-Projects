using UnityEngine;
using System.Collections;

public class Segment : MonoBehaviour {
	
	public float depth;
	
	//find all of the items in here
	
	Item[] itemList;
	
	// Use this for initialization
	void Start () {
	
		itemList = gameObject.GetComponentsInChildren<Item>();
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
	
	public void Reset()
	{
		foreach (Item item in itemList)
		{
			item.Reset();
		}
	}
	
	
}
