using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float jumpForce;
	public float moveSpeed;
	private Platform m_platformLanded;
	private float m_movingLimitX;
	Rigidbody2D rb;

	public Platform PlatformLanded { get => m_platformLanded; set => m_platformLanded = value; }
	public float MovingLimitX { get => m_movingLimitX; }

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	private void FixedUpdate()
	{
		MovingHandle();
	}
	public void jump()
	{
		if (!GameManager.Ins || GameManager.Ins.state != GameState.Playing)
		{
			return;
		}
		if (!rb || rb.velocity.y > 0 || !m_platformLanded) return;
		if(m_platformLanded is BreakablePlatform)
		{
			m_platformLanded.PlatformAction();
		}
		rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		if (AudioController.Ins)
		{
			AudioController.Ins.PlaySound(AudioController.Ins.jump);
		}
	}
	public void MovingHandle()
	{
		if (!GamePadController.Ins || !rb || !GameManager.Ins || GameManager.Ins.state != GameState.Playing) return;
		if (GamePadController.Ins.CanMoveLeft)
		{
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
			transform.localScale = new Vector3(-1,transform.localScale.y,transform.localScale.z);
		}
		else if (GamePadController.Ins.CanMoveRight)
		{
				rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
			transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
		}
		else
		{
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
		m_movingLimitX = Helper.Get2DCamSize().x/2;
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -m_movingLimitX+0.5f, m_movingLimitX-0.5f),transform.position.y,transform.position.z);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(GameTag.Collectable.ToString()))
		{
			var collectable = collision.GetComponent<Collectable>();
			if (collectable)
			{
				collectable.Trigger();
			}
		}
	}
}
