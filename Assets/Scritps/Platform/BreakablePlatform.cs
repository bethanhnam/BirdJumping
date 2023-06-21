using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : Platform
{
	public override void PlatformAction()
	{
		Destroy(gameObject,0.4f);
	}
}
