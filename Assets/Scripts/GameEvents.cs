using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameEvents
{
	public event Action<PlantDefination> OnCropHarvested;

	public event Action<PlantDefination> OnCropPlanted;

	public event Action<QuestDefination> OnQuestAccepted;

	public event Action<QuestDefination> OnQuestAbandoned;

	public event Action<QuestDefination> OnQuestCompleted;

	public event Action<object> OnItemAquired;

	public event Action<int> OnMoneyAquired;

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

	public void InvokeOnItemAquired(object item)
	{
		OnItemAquired?.Invoke(item);
	}

	public void InvokeOnMoneyAquired(int amount)
	{
		OnMoneyAquired?.Invoke(amount);
	}
}