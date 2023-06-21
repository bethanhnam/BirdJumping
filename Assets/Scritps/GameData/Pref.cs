using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pref
{
	public static int CurPlayerId
	{
		set => PlayerPrefs.SetInt(PrefConst.CUR_PLAYER_ID, value);
		get => PlayerPrefs.GetInt(PrefConst.CUR_PLAYER_ID);
	}
	public static int Coins
	{
		set => PlayerPrefs.SetInt(PrefConst.COIN_KEY, value);
		get => PlayerPrefs.GetInt(PrefConst.COIN_KEY,0);
	}
	public static int BestScore
	{
		set
		{
			int oldScore = PlayerPrefs.GetInt(Prefkey.BestCore.ToString(), 0);
			if(value >= oldScore|| oldScore == 0)
			{
				Debug.Log("thay");
				PlayerPrefs.SetInt(Prefkey.BestCore.ToString(), value);
			}
		}
		get => PlayerPrefs.GetInt(Prefkey.BestCore.ToString(), 0);
	}
	public static void SetBool(string name, bool value)
	{
		if (value)
		{
			PlayerPrefs.SetInt(name, 1);
		}
		else
		{
			PlayerPrefs.SetInt(name, 0);
		}
	}
	public static bool GetBool(string name)
	{
		return PlayerPrefs.GetInt(name) == 1 ? true : false;
	}
}
