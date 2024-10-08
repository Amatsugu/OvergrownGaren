using System;
using System.Collections;
using System.Collections.Generic;

using Balconies;

using Resources;

using UnityEngine;

public class GameEvents
{
	public event Action<PlantDefination> OnCropHarvested;

	public event Action<PlantDefination> OnCropPlanted;

	public event Action<QuestDefination> OnQuestAccepted;

	public event Action<QuestDefination> OnQuestAbandoned;

	public event Action<QuestDefination> OnQuestCompleted;

	public event Action<int> OnDayStart;

	public event Action<ResourceIdentifier, int> OnResourcesAdded;

	public event Action<ResourceIdentifier, int> OnResourcesSpent;
	public event Action<ResourceIdentifier> OnResourcesChange;

	public event Action<BalconyData> OnBalconyUnlocked;

	public event Action<BalconyData> OnBalconyReadyToUnlock;

	public event Action<ResourceType> OnResourceTypeUnlocked;

	public event Action<QuestDefination> OnQuestBecomeReadyToComplete; 

	public void InvokeOnCropHarvested(PlantDefination plant)
	{
		OnCropHarvested?.Invoke(plant);
	}

	public void InvokeOnCropPlanted(PlantDefination plant)
	{
		OnCropPlanted?.Invoke(plant);
	}

	public void InvokeOnQuestAccepted(QuestDefination quest)
	{
		OnQuestAccepted?.Invoke(quest);
	}

	public void InvokeOnQuestAbandoned(QuestDefination quest)
	{
		OnQuestAbandoned?.Invoke(quest);
	}

	public void InvokeOnQuestCompleted(QuestDefination quest)
	{
		OnQuestCompleted?.Invoke(quest);
	}

	public void InvokeResourcesAdded(ResourceIdentifier resource, int amountTotal)
	{
		OnResourcesAdded?.Invoke(resource, amountTotal);
	}

	public void InvokeResourcesSpent(ResourceIdentifier resource, int amountTotal)
	{
		OnResourcesSpent?.Invoke(resource, amountTotal);
	}
	public void InvokeResourcesChange(ResourceIdentifier resource)
	{
		OnResourcesChange?.Invoke(resource);
	}


	public void InvokeBalconyUnlocked(BalconyData balconyData)
	{
		OnBalconyUnlocked?.Invoke(balconyData);
	}

	public void InvokeBalconyReadyToUnlock(BalconyData balconyData)
	{
		OnBalconyReadyToUnlock?.Invoke(balconyData);
	}

	public void InvokeOnDayStart(int day)
	{
		OnDayStart?.Invoke(day);
	}

	public void InvokeOnResourceTypeUnlocked(ResourceType resource)
	{
		OnResourceTypeUnlocked?.Invoke(resource);
	}

	public void InvokeOnQuestBecomeReadyToComplete(QuestDefination quest)
	{
		OnQuestBecomeReadyToComplete?.Invoke(quest);
	}
}