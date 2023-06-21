using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDialog : Dialog
{
    public Text totalScoreText;
    public Text bestScoreText;

	public override void Show(bool isShow)
	{
		base.Show(isShow);
		if(totalScoreText && GameManager.Ins)
		{
			totalScoreText.text = GameManager.Ins.Score.ToString();	
		}
		if (bestScoreText)
		{
			bestScoreText.text = Pref.BestScore.ToString();
		}
	}
	public void Reset()
	{
		SceneManager.sceneLoaded += OnSceneLoadedEvent;
		SceneController.Ins.LoadCurrentScene();
	}
	private void OnSceneLoadedEvent(Scene scene,LoadSceneMode mode)
	{
		if (GameManager.Ins)
		{
			GameManager.Ins.PlayGame();
		}
		SceneManager.sceneLoaded -= OnSceneLoadedEvent;
	}
}
