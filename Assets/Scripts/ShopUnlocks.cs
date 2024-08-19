using Resources;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class ShopUnlocks 
{
    private HashSet<ResourceType> _unlocks = new HashSet<ResourceType>();

	public void UnlockResource(ResourceType resourceType)
	{
		if(_unlocks.Add(resourceType))
			GameManager.Events.InvokeOnResourceTypeUnlocked(resourceType);
	}

	public void UnlockResources(ResourceType[] resources)
	{
		foreach (var res in resources)
			UnlockResource(res);
	}

	public bool IsUnlocked(ResourceType resource)
	{
		return _unlocks.Contains(resource);
	}

	public ResourceType[] GetUnlocks()
	{
		return _unlocks.ToArray();
	}
}
