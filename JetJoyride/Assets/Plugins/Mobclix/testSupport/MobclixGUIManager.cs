using UnityEngine;
using System.Collections.Generic;


public class MobclixGUIManager : MonoBehaviour
{
#if UNITY_IPHONE

	void OnStart()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	
	void OnGUI()
	{
		float yPos = 5.0f;
		float xPos = 5.0f;
		float width = ( Screen.width >= 960 || Screen.height >= 960 ) ? 320 : 160;
		float height = ( Screen.width >= 960 || Screen.height >= 960 ) ? 80 : 40;
		float heightPlus = height + 10.0f;
		
		
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Initialize Mobclix" ) )
		{
			MobclixBinding.start( "insert-your-application-key" );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Show Banner" ) )
		{
			MobclixBinding.showBanner();
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Hide Banner" ) )
		{
			MobclixBinding.hideBanner( false );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Request Full Screen Ad" ) )
		{
			MobclixBinding.requestFullScreenAd();
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Is Full Screen Ad Loaded?" ) )
		{
			Debug.Log( "is full screen ad ready? " + MobclixBinding.isFullScreenAdReady() );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Display Full Screen Ad" ) )
		{
			MobclixBinding.displayFullScreenAd();
		}


		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Load and Show Full Screen" ) )
		{
			MobclixBinding.requestAndDisplayFullScreenAd();
		}
		
		
		
		xPos = Screen.width - width - 5.0f;
		yPos = 5.0f;
		
		if( iPhone.generation != iPhoneGeneration.iPad1Gen && iPhone.generation != iPhoneGeneration.iPad2Gen && iPhone.generation != iPhoneGeneration.iPad3Gen )
		{
			if( GUI.Button( new Rect( xPos, yPos, width, height ), "320x50 Banner (bottom right)" ) )
			{
				MobclixBinding.createBanner( MobclixBannerType.iPhone_320x50, MobclixAdPosition.BottomRight );
			}
		
			if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "320x250 (top)" ) )
			{
				MobclixBinding.createBanner( MobclixBannerType.iPhone_300x250, MobclixAdPosition.TopCenter );
			}
		}
		else
		{
			if( GUI.Button( new Rect( xPos, yPos, width, height ), "120x600 Banner (center)" ) )
			{
				MobclixBinding.createBanner( MobclixBannerType.iPad_120x600, MobclixAdPosition.Centered );
			}
			
			
			if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "468x60 Banner (bottom left)" ) )
			{
				MobclixBinding.createBanner( MobclixBannerType.iPad_468x60, MobclixAdPosition.BottomLeft );
			}
			
			
			if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "728x90 Banner (bottom)" ) )
			{
				MobclixBinding.createBanner( MobclixBannerType.iPad_728x90, MobclixAdPosition.BottomCenter );
			}
			
			
			if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "300x250 Banner (top left)" ) )
			{
				MobclixBinding.createBanner( MobclixBannerType.iPad_300x250, MobclixAdPosition.TopLeft );
			}
		}
	}
#endif
}
