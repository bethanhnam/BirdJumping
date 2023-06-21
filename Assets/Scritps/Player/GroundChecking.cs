using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecking : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!collision.gameObject.CompareTag(GameTag.Platform.ToString()))
		{
			return;
		}
		var platformLanded = collision.gameObject.GetComponent<Platform>();
		if (!GameManager.Ins || !GameManager.Ins.m_player || !platformLanded) return;
		GameManager.Ins.m_player.PlatformLanded = platformLanded;
		GameManager.Ins.m_player.jump();
		if (!GameManager.Ins.IsPlatformLanded(platformLanded.ID))
		{
			int randomScore = Random.Range(3, 8);
			GameManager.Ins.AddScore(randomScore);
			GameManager.Ins.PlatformLandedIds.Add(platformLanded.ID);
		}
	}
}
