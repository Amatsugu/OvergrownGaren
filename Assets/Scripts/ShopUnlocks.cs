using Resources;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUnlocks 
{
    private HashSet<ResourceType> unlocks = new HashSet<ResourceType>();

	public void UnlockResource(ResourceType resourceType)
	{
		if(unlocks.Add(resourceType))
			GameManager.Events.InvokeOnResourceTypeUnlocked(resourceType);
	}

	public void UnlockResources(ResourceType[] resources)
	{
		foreach (var res in resources)
			UnlockResource(res);
	}

	public bool IsUnlocked(ResourceType resource)
	{
		return unlocks.Contains(resource);
	}
}
