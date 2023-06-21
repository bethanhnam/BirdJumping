using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
	public Player m_player;
	public GameState state;
	public int StartingPlaform;
	public float xSpawnOffset;
	public float minSpawnPos;
	public float maxSpawnPos;
	public Platform[] platformsPrefabs;
	public CollectableItem[] CollectableItems;

	private Platform m_lastPlatformSpawned;
	private List<int> m_platformLandedIds;
	private float m_halfCamsizeX;
	private int m_score;

	public Platform LastPlatformSpawned { get => m_lastPlatformSpawned; set => m_lastPlatformSpawned = value; }
	public List<int> PlatformLandedIds { get => m_platformLandedIds; set => m_platformLandedIds = value; }
	public int Score { get => m_score; }

	public override void Awake()
	{
		MakeSingleton(false);
		m_platformLandedIds = new List<int>();
		m_halfCamsizeX = Helper.Get2DCamSize().x / 2;
	}
	public override void Start()
	{
		base.Start();
		GUIManager.Ins.UpdateCoins();
		state = GameState.Starting;
		

	}
	public void PlayGame()
	{
		if (GUIManager.Ins)
		{
			GUIManager.Ins.ShowGamePlay(true);
			
		}
		Invoke("PlayGameIvoke", 1f);
	}
	private void PlayGameIvoke()
	{
		state = GameState.Playing;
		var platformClone = Instantiate(platformsPrefabs[0],new Vector3(0,-4,0), quaternion.identity);
		LastPlatformSpawned = platformClone;
		Invoke("PlatformInit", 0.8f);
		if (AudioController.Ins)
		{
			AudioController.Ins.PlayBackgroundMusic();
		}
		GameManager.Ins.ActivePlayer();
		if (m_player)
		{
			m_player.jump();
		}
	}
	public void ActivePlayer()
	{
		if (m_player || state != GameState.Playing)
		{
			Destroy(m_player.gameObject);
		}
		var newPlayerPrefab = ShopManager.Ins.items[Pref.CurPlayerId].playerPrefab;
		if (newPlayerPrefab || state != GameState.Playing)
		{
			m_player = Instantiate(newPlayerPrefab, Vector3.zero, Quaternion.identity);
		}
	}
	private void PlatformInit()
	{
		m_lastPlatformSpawned = m_player.PlatformLanded;
		for(int i=0;i<StartingPlaform;i++)
		{
			SpawnPlatform();
		}
	}
	public bool IsPlatformLanded(int id)
	{
		if (m_platformLandedIds == null || m_platformLandedIds.Count <= 0) return false;
		return m_platformLandedIds.Contains(id);
	}
	public void SpawnPlatform()
	{
		if (!m_player || platformsPrefabs == null || platformsPrefabs.Length <= 0) return;
		float spawnPosX = Random.Range(-(m_halfCamsizeX - xSpawnOffset), (m_halfCamsizeX - xSpawnOffset));
		float DistanceBetweenPlatform = Random.Range(minSpawnPos, maxSpawnPos);
		float spawnPosY = m_lastPlatformSpawned.transform.position.y + DistanceBetweenPlatform;
		Vector3 SpawnPos = new Vector3(spawnPosX, spawnPosY, 0f);
		int randomIndex = Random.Range(0,platformsPrefabs.Length);
		var platformPrefab = platformsPrefabs[randomIndex];
		if(platformPrefab != null)
		{
			var platformClone = Instantiate(platformPrefab, SpawnPos, quaternion.identity);
			platformClone.ID = m_lastPlatformSpawned.ID + 1;
			m_lastPlatformSpawned = platformClone;
		}
	}
	public void AddScore(int scoreToAdd)
	{
		m_score += scoreToAdd;
		Pref.BestScore = m_score;
		GUIManager.Ins.UpdateCoins();
		GUIManager.Ins.UpdateScore();
	}
	public void SpawnCollectable(Transform spawnPoint)
	{
		if (CollectableItems == null || CollectableItems.Length <= 0 || state != GameState.Playing) return;
		int randIdx = Random.Range(0, CollectableItems.Length);
		var collectItem = CollectableItems[randIdx];
		if (collectItem == null) return;
		float randCheck = Random.Range(0, 1f);
		if(randCheck <= collectItem.spawnRate && collectItem.collectablePrefab!=null)
		{
			var cClone = Instantiate(collectItem.collectablePrefab, spawnPoint.position, quaternion.identity);
			cClone.transform.SetParent(spawnPoint);
		}
	}
}
