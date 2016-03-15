using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlaygroundController: MonoBehaviour {

	public Text Timer;
	public Text Score;
	public Text MenuScore;

	public GameObject Congratulation;

	public int Int_score = 0;

	public int levelTime = 30;
	public int levelTimeStep = 3;
	private float current_time = 0;
	private float time = 0;

	public delegate void MethodContainer();
	public static event MethodContainer OnGameOver_event;

	public static bool PAUSE = true;

	public TargetControl targetControl;
	public PlayerGesture playerGesture;

	void Start () {
		targetControl.Init();
		Congratulation.SetActive(false);
    }
	
	void Update () {
		if (!PAUSE)
		{
			current_time += Time.deltaTime;
			Timer.text = System.String.Format("{0:00}", levelTime - current_time);
			if ((levelTime - current_time) <= 0)
			{
				Timer.text = "00";
				GameOver();
			}
		}
	}

	public void AddScore()
	{
		Int_score++;
		Score.text = Int_score.ToString();
		MenuScore.text = Int_score.ToString();

	}

	public void GameOver()
	{

		if (OnGameOver_event != null)
		{
			OnGameOver_event();
		}

		
    }

	private void ShowCongratulation()
	{
		Congratulation.SetActive(true);
		StartCoroutine(WaitAndPrint(1.0f));
	}

	private void SetGestureTime()
	{
		levelTime = levelTime > levelTimeStep ? levelTime - levelTimeStep : levelTimeStep;
		current_time = 0;
 
    }

	IEnumerator WaitAndPrint(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Congratulation.SetActive(false);
	}

	void OnCustomGesture(PointCloudGesture gestures)
	{
		if (!PAUSE)
		{
			Debug.Log(gestures.RecognizedTemplate.name);

			if (gestures.RecognizedTemplate.name.Equals(targetControl.currentName))
			{
				targetControl.GetNextGesture();
				AddScore();
				ShowCongratulation();
				SetGestureTime();
				playerGesture.CleanGesture();
			}
		}
	}
}
