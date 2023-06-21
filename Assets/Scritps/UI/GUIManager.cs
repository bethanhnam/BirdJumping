using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{
	public GameObject mainMenu;
	public GameObject gamePlay;
    public Text coinCountingText;
    public Text ScoreCountingText;
	public PauseDialog pauseDialog;
	public GameOverDialog gameOverDialog;
	public override void Awake()
	{
		MakeSingleton(false);
	}
	public void ShowGamePlay(bool isShow)
	{
		if (gamePlay)
		{
			gamePlay.SetActive(isShow);
		}
		if (mainMenu)
		{
			mainMenu.SetActive(!isShow);
		}
	}
	public void UpdateCoins()
	{
		if (coinCountingText)
		{
			coinCountingText.text = "COINS : " + Pref.Coins.ToString();
		}
	}
	public void UpdateScore()
	{
		if (ScoreCountingText)
		{
			ScoreCountingText.text = "SCORE : " + GameManager.Ins.Score.ToString();
		}
	}
}
