using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialog : Dialog
{
    public Transform gridRoot;
    public ShopItemUI itemUIPrefab;
	public GameObject MenuObjects;

	public override void Show(bool isShow)
	{
		base.Show(isShow);
		MenuObjects.SetActive(!isShow);
		UpdateUI();
	}
	public override void Close()
	{
		base.Close();
		MenuObjects.SetActive(true);
	}
	private void UpdateUI()
	{
		var items = ShopManager.Ins.items;
		if (items == null || items.Length <= 0 || !gridRoot || !itemUIPrefab)
		{
			return;
		}
		ClearChilds();
		for(int i =0; i < items.Length; i++)
		{
			int index = i;
			var item = items[i];
			if(item != null)
			{
				var itemUIClone = Instantiate(itemUIPrefab, Vector3.zero, Quaternion.identity);
				itemUIClone.transform.SetParent(gridRoot);
				itemUIClone.transform.localPosition = Vector3.zero;
				itemUIClone.transform.localScale = Vector3.one;
				itemUIClone.UpdateUI(item, index);
				if (itemUIClone.button)
				{
					itemUIClone.button.onClick.RemoveAllListeners();
					itemUIClone.button.onClick.AddListener(() => ItemEvent(item,index));
				}
			}
		}
	}
	void ItemEvent(ShopItem item,int shopItemID)
	{
		if (item == null) return;
		bool isUnlocked = Pref.GetBool(PrefConst.PLAYER_PEFIX + shopItemID);
		if(isUnlocked)
		{
			if (shopItemID == Pref.CurPlayerId) return;
			Pref.CurPlayerId = shopItemID;
			//GameManager.Ins.ActivePlayer();
			UpdateUI();
		}
		else
		{
			if(Pref.Coins >= item.price)
			{
				Pref.Coins -= item.price;
				GUIManager.Ins.UpdateCoins();
				Pref.SetBool(PrefConst.PLAYER_PEFIX + shopItemID, true);
				Pref.CurPlayerId = shopItemID;
				//GameManager.Ins.ActivePlayer();
				UpdateUI();
			}
			else
			{
				Debug.Log("you dont have enough coins !!!");
			}
		}
	}
	public void ClearChilds()
	{
		if (!gridRoot || gridRoot.childCount <= 0) return;
		for(int i = 0; i < gridRoot.childCount; i++)
		{
			var child = gridRoot.GetChild(i);
			if (child)
			{
				Destroy(child.gameObject);
			}
		}
	}
}
