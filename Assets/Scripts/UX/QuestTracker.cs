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

	private void Awake()
	{
		newQuestWindow.questTracker = this;
		GameManager.Events.OnDayStart += OnDayStart;
	}

	void OnDayStart(int _)
	{

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

	private bool CanCompleteQuest(QuestDefination quest)
	{
		return GameManager.ResourcesService.IsEnough(quest.deliveryRequirments);
	}

}
