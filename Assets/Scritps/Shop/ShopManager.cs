using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    public ShopItem[] items;
	private void Start()
	{
		if(items == null || items.Length <= 0)
		{
			return;
		}
		for(int i = 0; i < items.Length; i++)
		{
			var item = items[i];
			if(item !=null)
			{
				if( i == 0)
				{
					Pref.SetBool(PrefConst.PLAYER_PEFIX + i, true); // player_0
				}
				else
				{
					// chỉ chạy duy nhất lần đâu tiên khi chơi 
					if(!PlayerPrefs.HasKey(PrefConst.PLAYER_PEFIX + i))
					{
						Pref.SetBool(PrefConst.PLAYER_PEFIX + i, false);
					}
				}
			}
		}
	}
}
[System.Serializable]
public class ShopItem
{
    public int price;
    public Sprite hub;
    public Player playerPrefab;
}
