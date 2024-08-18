using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
	[Header("Quest Data")]
	public QuestDefination[] questDefinations;

	[Header("Questing Config")]
	public int maxAcceptedQuests = 2;

	[Header("Windows")]
	public QuestWindow questsWindow;
	public NewQuestWindow newQuestWindow;

	private List<QuestDefination> _activeQuests = new();
	private List<QuestDefination> _completedQuests = new();

	private void Awake()
	{
		newQuestWindow.questTracker = this;
		questsWindow.questTracker = this;
		GameManager.Events.OnDayStart += OnDayStart;
		GameManager.Events.OnResourcesAdded += OnResourceChange;
		GameManager.Events.OnResourcesSpent += OnResourceChange;
	}

	void OnResourceChange(ResourceIdentifier _, int __)
	{
		if (questsWindow.IsOpen)
			questsWindow.ShowQuests(_activeQuests);
	}

	void OnDayStart(int _)
	{
		if (_activeQuests.Count >= maxAcceptedQuests)
			return;

	}


	public void ShowQuests()
	{
		questsWindow.ShowQuests(_activeQuests);
		newQuestWindow.Hide();
	}

	public void ShowNewQuests()
	{
		newQuestWindow.ShowQuests(questDefinations);
		questsWindow.Hide();
	}


	public bool AcceptQuest(QuestDefination quest)
	{
		if(_activeQuests.Count < maxAcceptedQuests)
		{
			_activeQuests.Add(quest);
			GameManager.Events.InvokeOnQuestAccepted(quest);
			return true;
		}
		return false;
	}

	public static bool CanCompleteQuest(QuestDefination quest)
	{
		return GameManager.ResourcesService.IsEnough(quest.deliveryRequirments);
	}

	public void AbandonQuest(QuestDefination quest)
	{
		_activeQuests.Remove(quest);
		GameManager.Events.InvokeOnQuestAbandoned(quest);
	}

	public bool CompleteQuest(QuestDefination quest)
	{
		if (!CanCompleteQuest(quest))
			return false;

		GameManager.ResourcesService.SpendResources(quest.deliveryRequirments);
		GameManager.ResourcesService.AddResources(quest.rewards);
		GameManager.Unlocks.UnlockResources(quest.rewardUnlocks);

		return true;
	}

}
