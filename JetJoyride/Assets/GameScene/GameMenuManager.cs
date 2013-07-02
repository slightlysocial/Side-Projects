using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))]
public class GameMenuManager : MonoBehaviour {
	
	public Transform pausedScreen;
	public Transform ingameGUI;
	public Transform gameoverScreen;
	
	public Hero hero;

	public GameObject scoreTextObject;
	private TextMesh scoreText;
	public GameObject shieldsTextObject;
	private TextMesh shieldsText;
	public GameObject multiplierObject;
	private TextMesh multiplierText;
		
	

	
	
	// Use this for initialization
	void Start () {
		
		if (scoreTextObject!=null)
		{
			scoreText = scoreTextObject.GetComponent<TextMesh>();
		}
		
		if (shieldsTextObject != null)
		{
			shieldsText = shieldsTextObject.GetComponent<TextMesh>();	
		}
		
		if (multiplierObject != null)
		{
			multiplierText = multiplierObject.GetComponent<TextMesh>();	
		}
				
	}
	
	void MainMenu()
	{
		Application.LoadLevel("MainMenu");	
	}
	
	
	void Restart()
	{
		Application.LoadLevel("GameScene");	
	}
	
	void Pause()
	{
		GameManager.PauseGame();	
	}
	
	void Resume()
	{
		GameManager.ResumeGame();	
	}
	

	
	Vector3 OFFSCREEN_POSITION = new Vector3(-800,0,0);
	
	void Update()
	{
		
		//scoreText.text = ""+(int)GameManager.score;
		
		scoreText.text = string.Format("{0:0000000000}", (int)GameManager.score);
		
		shieldsText.text = "SHIELDS: "+hero.shields;
		multiplierText.text = ""+GameManager.multiplier + "X";
		
		
		if (Input.GetMouseButtonUp(0))
		{			
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit ;
            if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.gameObject.GetComponent<ButtonObject>() != null)
					hit.transform.gameObject.SendMessage("Selected");
            }
		}
		
		
		if (GameManager.IsGameOver())
		{
			pausedScreen.transform.localPosition = OFFSCREEN_POSITION;
			ingameGUI.transform.localPosition = OFFSCREEN_POSITION;
			gameoverScreen.transform.localPosition = Vector3.zero;
			
		}
		else if (GameManager.IsPaused())
		{
			pausedScreen.transform.localPosition = Vector3.zero;
			ingameGUI.transform.localPosition = OFFSCREEN_POSITION;
			gameoverScreen.transform.localPosition = OFFSCREEN_POSITION;
		}
		else
		{
			ingameGUI.transform.localPosition = Vector3.zero;
			pausedScreen.transform.localPosition = OFFSCREEN_POSITION;
			gameoverScreen.transform.localPosition = OFFSCREEN_POSITION;
		}
	}
}
