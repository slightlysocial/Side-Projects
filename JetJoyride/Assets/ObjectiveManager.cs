using UnityEngine;
using System.Collections;

public class ObjectiveManager : MonoBehaviour {

	
	public Item[] collectables;

	private Item currentObjectiveCollectable;
	private int numberNeeded = 5;	
	private int numberCollected = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void NewObjective()
	{
		
	}
	
	void GotItem(string itemName)
	{
		if (itemName == currentObjectiveCollectable.collectableName)
		{
			numberCollected++;
			if (numberCollected > 5)
			{
				
			}
		}
	}	
}
