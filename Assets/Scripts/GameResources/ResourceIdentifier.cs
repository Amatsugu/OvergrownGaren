using Resources;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ResourceIdentifier
{
	public ResourceType type;
	public int qty;


	public static implicit operator ResourceIdentifier((ResourceType type, int qty) data)
	{
		return new ResourceIdentifier
		{
			type = data.type,
			qty = data.qty
		};
	}
}
