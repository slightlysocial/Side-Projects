using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent (typeof (Camera))]
public class MainMenuManager : MonoBehaviour {
	
	public GameObject mainMenu;
	public GameObject settingsMenu;
	public GameObject shopMenu;
	
	public GameObject soundOnObject;
	public GameObject musicOnObject;
	
	private static string SOUND_OFF = "SOUND_OFF";
	private static string MUSIC_OFF = "MUSIC_OFF";
	
	private static bool isSoundOff = false;
	
	private static bool isMusicOff = false;
	
	void Start()
	{
		/*if(!SSAdManager.isLoaded())
		{
			StartCoroutine(SSAdManager.LoadedPlist());
		}
		
		
		if (!Social.localUser.authenticated)
		{
			Social.localUser.Authenticate (ProcessAuthentication);
		}*/
		
		if (PlayerPrefs.GetInt(SOUND_OFF) > 0)
		{
			isSoundOff = true;
			soundOnObject.transform.localPosition = new Vector3(soundOnObject.transform.localPosition.x, soundOnObject.transform.localPosition.y, 102);
		}
		else
		{
			isSoundOff = false;	
		}
		
		if (PlayerPrefs.GetInt(MUSIC_OFF) > 0)
		{
			musicOnObject.transform.localPosition = new Vector3(musicOnObject.transform.localPosition.x, musicOnObject.transform.localPosition.y, 102);
			isMusicOff = true;
		}
		else
		{
			isMusicOff = false;	
		}
		
	}
	
	
	// This function gets called when Authenticate completes
    // Note that if the operation is successful, Social.localUser will contain data from the server. 
    void ProcessAuthentication (bool success) {
        if (success) {
            Debug.Log ("Authenticated, checking achievements");

            Social.LoadAchievements (ProcessLoadedAchievements);
        }
        else
            Debug.Log ("Failed to authenticate");
    }
	
	
	// This function gets called when the LoadAchievement call completes
    void ProcessLoadedAchievements (IAchievement[] achievements) {
        if (achievements.Length == 0)
            Debug.Log ("Error: no achievements found");
        else
            Debug.Log ("Got " + achievements.Length + " achievements");
    }	
	
	
	void SoundButton()
	{
		if (isSoundOff)
		{
			PlayerPrefs.SetInt(SOUND_OFF, 1);
		}
		else
		{
			isSoundOff = false;	
		}
		
	}
	
	
	void MusicButton()
	{
			
	}
	
	void PlayButton()
	{
		Application.LoadLevel("GameScene");
	}
	
	
	Vector3 OFFSCREEN = new Vector3(-8000,0,0);
	
	void RestoreButton()
	{
		StoreKitBinding.restoreCompletedTransactions();
		Debug.Log("Restored Completed Transactions");	
	}
	
	void AchievementsButton()
	{
		Social.ShowAchievementsUI();
	}
	
	void LeaderboardButton()
	{
		GameCenterPlatform.ShowLeaderboardUI("jj_leaderboard", TimeScope.AllTime);
	}
	
	void StoreButton()
	{
		mainMenu.transform.position = OFFSCREEN;
		shopMenu.transform.position = Vector3.zero;
		settingsMenu.transform.position = OFFSCREEN;
	}
	
	void SettingsButton()
	{
		mainMenu.transform.position = OFFSCREEN;
		settingsMenu.transform.position = Vector3.zero;
		shopMenu.transform.position = OFFSCREEN;
	}
	
	void ReturnButton()
	{
		settingsMenu.transform.position = OFFSCREEN;
		mainMenu.transform.position = Vector3.zero;
		shopMenu.transform.position = OFFSCREEN;
	}
	
	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{			
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit ;
            if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.gameObject.GetComponent<ButtonObject>() != null)
					hit.transform.gameObject.SendMessage("Selected");
            }
		}
	}
	
}
