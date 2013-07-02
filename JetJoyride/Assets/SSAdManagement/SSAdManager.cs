using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SSAdManager : MonoBehaviour{
	
	public static string versionNumber = "1.0";

	private static string CHARTBOOST_ID ="5112964a16ba470e63000010";
	private static string CHARTBOOST_APP_SIGNATURE ="e573f1cc54d98f41861ff0d913aa019ba01e3942";
	private static string MOBCLIX_ID ="09C628BB-9F0A-44BA-8A53-79A2118583A5";
	private static string REVMOB_ID = "511297773fdaf0b11e000003";
	
	private static string INAPP_REMOVEALLADS = "jj_removeads";
	private static string INAPP_ALLOFABOVE = "jj_allofabove";
	private static string INAPP_CONTENTPACK = "jj_contentpack";
	
	private static string PLIST_URL ="http://iosgames.slightlysocial.com/raptor_run.plist";
	
	private static bool adRemoved = true;
	
	private static bool needsUpdating = false;
	private static bool isInReview = true;
	public static AdValue adBannerOn,
						adOnFreeGame,
						adOnActive,
						adOnPause,
						adOnLoad1,
						adOnLoad2,
						adOnGameOver;

	private static RevMob revmobSession;

	private static bool didLoad = false;
	
	private static bool isBannerShowing = false;
	
	
	private static float lastAdShownTime = float.MinValue;
	private static float adShownInterval = 30.0f;//the minimum number of seconds between ads
	
	public enum AdValue
	{
		OFF,
		CHARTBOOST,
		CHARTBOOST_MORE,
		REVMOB_POPUP,
		REVMOB_FULLSCREEN,
		MOBCLIX,
	};
	
	public static void AddStoreKitListener()
	{
		// Listens to all the StoreKit events.  All event listeners MUST be removed before this object is disposed!
		StoreKitManager.purchaseSuccessful += purchaseSuccessful;
		StoreKitManager.purchaseCancelled += purchaseCancelled;
		StoreKitManager.purchaseFailed += purchaseFailed;
		StoreKitManager.receiptValidationFailed += receiptValidationFailed;
		StoreKitManager.receiptValidationRawResponseReceived += receiptValidationRawResponseReceived;
		StoreKitManager.receiptValidationSuccessful += receiptValidationSuccessful;
		StoreKitManager.productListReceived += productListReceived;
		StoreKitManager.productListRequestFailed += productListRequestFailed;
		StoreKitManager.restoreTransactionsFailed += restoreTransactionsFailed;
		StoreKitManager.restoreTransactionsFinished += restoreTransactionsFinished;
	}
	 
//STOREKIT LISTENER STARTS HERE
	
	public static void productListReceived( List<StoreKitProduct> productList )
	{
		Debug.Log( "total productsReceived: " + productList.Count );
		
		// Do something more useful with the products than printing them to the console
		foreach( StoreKitProduct product in productList )
			Debug.Log( product.ToString() + "\n" );
	}
	
	
	public static void productListRequestFailed( string error )
	{
		Debug.Log( "productListRequestFailed: " + error );
	}
	
	
	public static void receiptValidationSuccessful()
	{
		Debug.Log( "receipt validation successful" );
	}
	
	
	public static void receiptValidationFailed( string error )
	{
		Debug.Log( "receipt validation failed with error: " + error );
	}
	
	
	public static void receiptValidationRawResponseReceived( string response )
	{
		Debug.Log( "receipt validation raw response: " + response );
	}
	
	
	public static void purchaseFailed( string error )
	{
		Debug.Log( "purchase failed with error: " + error );
	}
	

	public static void purchaseCancelled( string error )
	{
		Debug.Log( "purchase cancelled with error: " + error );
	}
	
	
	public static void purchaseSuccessful( string productIdentifier, string receipt, int quantity )
	{
		Debug.Log( "purchased product: " + productIdentifier + ", quantity: " + quantity );
		
		if (productIdentifier.Equals(INAPP_REMOVEALLADS))
		{
			//set the removal of the banner to be true in player prefs
			PurchaseRemoveAds();
			Debug.Log("Purchased remove all ads");
		}
		else if (productIdentifier.Equals(INAPP_CONTENTPACK))
		{
			
			
			
			Debug.Log("Purchased in app content pack");
		}
		
	}
	
	
	public static void restoreTransactionsFailed( string error )
	{
		Debug.Log( "restoreTransactionsFailed: " + error );
	}
	
	
	public static void restoreTransactionsFinished()
	{
		Debug.Log( "restoreTransactionsFinished" );
	}
	
//STOREKITLISTENERS END
	
	
	public static void PurchaseRemoveAds()
	{
		PlayerPrefs.SetInt(INAPP_REMOVEALLADS, 1);
		adRemoved = true;
		HideBannerAd();
	}

	public static void PurchaseContentPack()
	{
		PlayerPrefs.SetInt(INAPP_CONTENTPACK, 1);
	}
	
	public static void PurchaseAllOfAbove()
	{
		PlayerPrefs.SetInt(INAPP_ALLOFABOVE, 1);
		PurchaseRemoveAds();
		PurchaseContentPack();
	}
	
	public static IEnumerator LoadedPlist()
	{
		Debug.Log("Loading...");
		
        WWW www = new WWW(PLIST_URL);
        yield return www;
        //renderer.material.mainTexture = www.texture;
		
		
		if (www.error!= null)
		{
			Debug.Log(www.error);
			return false;
		}
		
		Debug.Log("Loaded PLIST");
			
		Hashtable hashTable = new Hashtable();
		PListManager.ParsePListText(www.text, ref hashTable);
		
		foreach(object key in hashTable.Keys)
		{
			if (key.Equals(versionNumber))
			{
				Debug.Log("found PLIST version..." + key);
				
				Hashtable adTable = (Hashtable)hashTable[key];
				
				needsUpdating = (((int) adTable["NEEDS_UPDATING"]) == 1);
				isInReview = (((int) adTable["IS_IN_REVIEW"]) != 1);	
				adOnPause = (AdValue)(adTable["AD_ON_PAUSE"]);
				adOnActive= (AdValue)(adTable["AD_ON_ACTIVE"]);
				adOnLoad1 = (AdValue)(adTable["AD_ON_LOAD_1"]);
				adOnLoad2 = (AdValue)(adTable["AD_ON_LOAD_2"]);
				adOnGameOver = (AdValue)(adTable["AD_ON_GAMEOVER"]);
				adOnFreeGame = (AdValue)(adTable["AD_ON_FREEGAME"]);
				adBannerOn = (AdValue)(adTable["AD_BANNER_ON"]);
				
				Debug.Log("loaded PLIST complete");
				
				break;//found version...exit loop
			}
		}
		
		
		//initialize the ad SDKs here
		#if UNITY_EDITOR
		Debug.Log("Can't run Ads in Editor...DEPLOY ME");
		return false;
		#endif
		
		revmobSession = new RevMobIos(REVMOB_ID, "GameManager");
	//	revmobSession = RevMob.Start(revmobAppIds);//Android/IOS
		
		//check to see if the banner removal has been purchased
		if (PlayerPrefs.GetInt(INAPP_REMOVEALLADS) > 0)
		{
			adRemoved = true;
			Debug.Log("All ads are removed");
		}
		else
		{
			adRemoved = false;
		}
		
		
		
		
		#if UNITY_IPHONE
		
		AddStoreKitListener();
		initChartBoost();
		MobclixBinding.start(MOBCLIX_ID);
		initMobclix();
		CreateMobclixBanner();
		HideBannerAd();
		
		//start tapjoy connect
		
		#endif
		
		didLoad = true;
		
		ShowLoad();
	
		return true;
	}
	
	
	public static bool isLoaded()
	{
		return didLoad;	
	}
	
	private static void ShowAd(AdValue adValue)
	{		
		if (isInReview || !didLoad || adRemoved)
			return;//don't show ads if we are in review
		
		/*if (lastAdShownTime < Time.time + adShownInterval)
		{
			lastAdShownTime = Time.time;
		}
		else
		{
			return;	//don't show an ad
		}*/
		
		if (adValue == AdValue.CHARTBOOST)
		{
			showChartboostInterstitial();
		}
		else if (adValue == AdValue.CHARTBOOST_MORE)
		{
			showChartBoostMore();	
		}
		else if (adValue == AdValue.REVMOB_POPUP)
		{
			revmobSession.ShowPopup();			
		}
		else if (adValue == AdValue.REVMOB_FULLSCREEN)
		{
			revmobSession.ShowFullscreen();	
		}
	}
	
	
	public static void ShowBannerAd()
	{
		if (adRemoved)
			return;
		
		if (!isBannerShowing)
		{
			CreateMobclixBanner();
			MobclixBinding.showBanner();
			isBannerShowing = true;
			Debug.Log("ShowingBannerAd()");
		}
		//also show the x button
	}
	
	public static void HideBannerAd()
	{
		MobclixBinding.hideBanner(false);
		isBannerShowing = false;
		Debug.Log("HideBannerAd()");
		//remove the x button
	}
	
	public static void ShowFreeGame()
	{
		ShowAd(adOnFreeGame);
	}
	
	public static void ShowGameOver()
	{
		ShowAd(adOnGameOver);
	}
	
	public static void ShowActive()
	{
		ShowAd(adOnActive);	
	}
	
	public static void ShowPause()
	{
		ShowAd(adOnPause);	
	}
	
	public static void ShowLoad()
	{
		ShowAd(adOnLoad1);
		ShowAd(adOnLoad2);
	}	
	
	private static bool IsIpad()
	{
		return (
		iPhone.generation == iPhoneGeneration.iPad1Gen ||
		iPhone.generation == iPhoneGeneration.iPad2Gen || 
		iPhone.generation == iPhoneGeneration.iPad3Gen || 
		iPhone.generation == iPhoneGeneration.iPad4Gen || 
		iPhone.generation == iPhoneGeneration.iPadMini1Gen || 
		iPhone.generation == iPhoneGeneration.iPadUnknown);
	}
	
	private static void CreateMobclixBanner()
	{	
		if (adRemoved)
			return;
		
		if (IsIpad())
		{
			MobclixBinding.createBanner( MobclixBannerType.iPad_728x90, MobclixAdPosition.TopCenter );
		}
		else
		{
			MobclixBinding.createBanner( MobclixBannerType.iPhone_320x50, MobclixAdPosition.TopCenter );
		}
		Debug.Log("Creating Mobclix Banner");
		
	}
	
	//show the banner X button for doing in App purchases
	public static void BannerAdGUI(Texture2D xButtonTexture)
	{
		//if (Application.platform != RuntimePlatform.IPhonePlayer)
	//		return;
		
		//if (!isBannerShowing)
		//	return;
		
		Rect buttonRect = new Rect(0,0,0,0);
		
		float buttonSize = 0.06f;
		if (IsIpad())
		{
			buttonRect = new Rect(Screen.width*0.90f, Screen.width*0.16f, Screen.width*buttonSize, Screen.width*buttonSize);
		}
		else
		{
			buttonRect = new Rect(Screen.width*0.90f, Screen.width*0.18f, Screen.width*buttonSize, Screen.width*buttonSize);
		}
		
		GUIStyle style = new GUIStyle();
		
		style.fixedWidth = buttonRect.width;
		style.fixedHeight =buttonRect.height;
		
		if (GUI.Button(buttonRect, xButtonTexture,style))
		{
			StoreKitBinding.purchaseProduct(INAPP_REMOVEALLADS, 1);
			
			GameManager.PauseGame();
			
			Debug.Log("pressed x button");
		}
	}
	
	public static bool HasPurchasedAllOfAbove()
	{
		return (PlayerPrefs.GetInt(INAPP_ALLOFABOVE) > 0);
	}
	
	public static bool HasPurchasedContentPack()
	{
	
		return 	(PlayerPrefs.GetInt(INAPP_CONTENTPACK) > 0);
	}
	
	public static bool HasPurchasedAdsRemoved()
	{
		return (PlayerPrefs.GetInt(INAPP_REMOVEALLADS) > 0);	
	}
	
	public static void RequestPurchaseAllOfAbove()
	{
		StoreKitBinding.purchaseProduct(INAPP_ALLOFABOVE, 1);
		Debug.Log("TRIED PURCHASING ALL");
	}
	
	public static void RequestPurchaseContentPack()
	{
		StoreKitBinding.purchaseProduct(INAPP_CONTENTPACK, 1);
		
		Debug.Log("TRIED PURCHASING CONTENT");
	}
	
	public static void RequestPurchaseAdsRemoved()
	{
		StoreKitBinding.purchaseProduct(INAPP_REMOVEALLADS, 1);
		
		Debug.Log("TRIED PURCHASING REMOVED ADS");
	}
	
	
	private static void initMobclix()
	{
		MobclixBinding.start( MOBCLIX_ID );
		
	}
			
	private static void initChartBoost()
	{
		ChartBoostBinding.init(CHARTBOOST_ID, CHARTBOOST_APP_SIGNATURE);	
	}
	
	private static void showChartboostInterstitial()
	{
		ChartBoostBinding.showInterstitial(null);//null for location
	}
	
	private static void showChartBoostMore()
	{
		ChartBoostBinding.showMoreApps();//null for location
	}
		
}
