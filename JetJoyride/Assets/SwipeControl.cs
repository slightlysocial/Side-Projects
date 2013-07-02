using UnityEngine;
using System.Collections;

public class SwipeControl : MonoBehaviour {

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButtonDown(0))
		{
			firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);	
		}	
		
		if (Input.GetMouseButtonUp(0))
		{
			secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			
			currentSwipe = secondPressPos-firstPressPos;
			
			if (currentSwipe.magnitude > 10.0f)
			{
				if (Mathf.Abs(currentSwipe.x) < Mathf.Abs(currentSwipe.y))
				{
					if (currentSwipe.y > 0)
					{
						gameObject.SendMessage("OnSwipeUp", SendMessageOptions.DontRequireReceiver);
					}
					else
					{
						gameObject.SendMessage("OnSwipeDown", SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					if (currentSwipe.x > 0)
					{
						gameObject.SendMessage("OnSwipeRight", SendMessageOptions.DontRequireReceiver);
					}
					else
					{	
						gameObject.SendMessage("OnSwipeLeft", SendMessageOptions.DontRequireReceiver);	
					}
				}
			}
			
		}
		
		
	}
	
	
	void UpdateSwipes()
	{
		
	}
	
	
}
