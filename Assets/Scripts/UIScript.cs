using UnityEngine;
using System.Collections;

public class UIScript: MonoBehaviour {

	public GameObject StartMenu;
	public GameObject PauseMenu;
	public GameObject GameOverMenu;
	public GameObject Playgrounds;

	void Awake()
	{
		PlaygroundController.OnGameOver_event += EndGame;  //// TO DO: Enable disable 
    }

	public void StartGame()
	{
		StartMenu.SetActive(false);
		Playgrounds.SetActive(true);

		PlaygroundController.PAUSE = false;
    }

	public void RestartGame()
	{
		PlaygroundController.OnGameOver_event -= EndGame;
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void EndGame()
	{
		GameOverMenu.SetActive(true);
		Playgrounds.SetActive(false);
		PlaygroundController.PAUSE = true;
	}

	public void PauseGame()
	{
		PauseMenu.SetActive(true);
		PlaygroundController.PAUSE = true;
	}

	public void ContinueGame()
	{
		PauseMenu.SetActive(false);
		PlaygroundController.PAUSE = false;
	}
}
