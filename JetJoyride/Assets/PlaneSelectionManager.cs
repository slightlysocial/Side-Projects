using UnityEngine;
using System.Collections;

public class PlaneSelectionManager : MonoBehaviour {
	
	public PlaneChooserItem[] planeList;
	public GameObject planeAvailableNotification;
	
	private Vector3 focusPosition = new Vector3(5,25,100);
	
	private Vector3 offScreenRight = new Vector3(200,25,100);
	
	private Vector3 offScreenLeft = new Vector3(-200,25,100);
	
	private Quaternion defaultRotation = Quaternion.Euler(35,115,310);
	
	private int planeIndex = Hero.selectedShip;
	
	
	void PlaneUnavailableOff()
	{
		planeAvailableNotification.transform.localPosition = new Vector3(-1000,0,0);
	}
	
	void PlaneUnavailableOn()
	{
		planeAvailableNotification.transform.localPosition = new Vector3(0,6.5f,50);
	}
	private bool isContentBought = false;
	// Use this for initialization
	void Start () {
	
		//put the first plane in the index in the center
		
		for(int i = 0; i < planeList.Length;i++)
		{
			planeList[i].transform.localPosition = offScreenLeft;
			planeList[i].targetPosition = offScreenLeft;
			planeList[i].transform.rotation = defaultRotation;
			planeList[i].gameObject.layer = LayerMask.NameToLayer("GUI");
		}
		
		if (planeList.Length >0)
		{
			planeList[planeIndex].transform.localPosition = focusPosition;
			planeList[planeIndex].targetPosition = focusPosition;
			planeList[planeIndex].transform.rotation = defaultRotation;
		}
		
		if (planeIndex == 0)
		{
			PlaneUnavailableOff();	
		}
		isContentBought = SSAdManager.HasPurchasedContentPack();
	}
	
	void LeftArrowButton()
	{
		
		planeList[planeIndex].targetPosition = offScreenLeft;
		
		planeIndex--;
		if (planeIndex < 0)
		{
			planeIndex=planeList.Length-1;
		}
		planeList[planeIndex].transform.localPosition = offScreenRight;
		planeList[planeIndex].targetPosition = focusPosition;
		
		
	CheckPlaneAvailable();
		
	}
	
	
	void CheckPlaneAvailable()
	{
			if (!isContentBought)
		{
			Hero.selectedShip = 0;
			if (planeIndex == 0)
			{
					PlaneUnavailableOff();
			}
			else
			{
				PlaneUnavailableOn();
			}
		}
		else
		{
			Hero.selectedShip = planeIndex;	
			PlaneUnavailableOff();
		}
	}
	
	void RightArrowButton()
	{
		
		planeList[planeIndex].targetPosition = offScreenRight;
		
		planeIndex++;
		planeIndex%=planeList.Length;	
		
		planeList[planeIndex].transform.localPosition = offScreenLeft;
		planeList[planeIndex].targetPosition = focusPosition;
		
		Hero.selectedShip = planeIndex;
		
		
		CheckPlaneAvailable();
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	
}
