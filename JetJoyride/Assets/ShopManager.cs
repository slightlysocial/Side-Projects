using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour {
	
	public GameObject contentPackButton;
	public GameObject adsRemovalButton;
	public GameObject allOfAboveButton;
	
	void Start()
	{
		
		if (SSAdManager.HasPurchasedAllOfAbove())
		{
			allOfAboveButton.SetActive(false);
			contentPackButton.SetActive(false);
			adsRemovalButton.SetActive(false);
		}
		else
		{
			if (SSAdManager.HasPurchasedAdsRemoved())
			{
				adsRemovalButton.SetActive(false);	
				allOfAboveButton.SetActive(false);
			}
			if(SSAdManager.HasPurchasedContentPack())
			{
				contentPackButton.SetActive(false);	
				allOfAboveButton.SetActive(false);
			}
		}
		
	}
	
	void ThreePackButton()
	{
		SSAdManager.RequestPurchaseContentPack();
	}
	
	
	void RemoveAdsButton()
	{
		SSAdManager.RequestPurchaseAdsRemoved();
	}
	
	
	void AllOfAboveButton()
	{
		SSAdManager.RequestPurchaseAllOfAbove();
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
