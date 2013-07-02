using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static float highPosY = 100.0f;
	public static float lowPosY = 0.0f;
	
	public static float leftPosX = -100.0f;
	public static float rightPosX = 100.0f;
	public static float middlePosX = 0.0f;
	
	private static bool isPaused = false;
	
	
	private static bool isGameOver = false;

	
	private static float GAMEOVER_TIME = 2.0f;
	private static float gameOverTimer = GAMEOVER_TIME;//how long it counts down for 
	
	public static float score = 0.0f;
	public static int multiplier = 1;
	
	private static int MAX_MULTIPLIER = 4;
		
	// Use this for initialization
	void Start () {
		Time.fixedDeltaTime = 0.04f;
		score = 0.0f;
		multiplier = 1;
		ResumeGame();
		gameOverTimer = GAMEOVER_TIME;
	}
	
	void FixedUpdate()
	{
		if (isGameOver)
		{
			if (gameOverTimer >= 0.0f)
			{
				gameOverTimer-=Time.fixedDeltaTime;
				Debug.Log ("counting down");
			}
			else
			{
				Debug.Log ("set game over down");
				
				Object[] objects = FindObjectsOfType (typeof(GameObject));
				foreach (GameObject go in objects) {
					go.SendMessage ("OnPauseGame", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
	
	void IncreaseMultiplier()
	{
		if (multiplier == 1)
		{
			multiplier =  2;
		}
		else if (multiplier % 2 == 0)
		{
			multiplier+=2;	
		}
		else
		{
			multiplier++;	
		}
	}
	
	void ResetMultiplier()
	{
		multiplier = 1;	
	}
	
	void IncreaseScore(float amount)
	{
		score+=amount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static bool IsPaused()
	{
		return isPaused;	
	}
	
	public static bool IsGameOver()
	{
		return (isGameOver && gameOverTimer <= 0);
	}
	
	
	public void OnGameOver()
	{
		isGameOver = true;

	
		
	}
	
	
	public static void PauseGame()
	{	
		isPaused = true;
		
		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) {
			go.SendMessage ("OnPauseGame", SendMessageOptions.DontRequireReceiver);
		}	
	}
	
	public static void ResumeGame()
	{
		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) {
			go.SendMessage ("OnResumeGame", SendMessageOptions.DontRequireReceiver);
		}	
		
		isPaused = false;
		isGameOver = false;
	}
	
	
}
