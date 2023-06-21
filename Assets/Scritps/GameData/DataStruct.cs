using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	Starting,
	Playing,
	GameOver
}
public enum GameTag
{
	Platform,
	Player,
	LeftCorner,
	RightCorner,
	Collectable
}
public enum Prefkey
{
	BestCore
}
[System.Serializable]
public class CollectableItem
{
	public Collectable collectablePrefab;
	[Range(0f, 1f)]
	public float spawnRate;
}