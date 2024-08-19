using System.Collections.Generic;
using System.Linq;

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
	
	private readonly List<QuestDefination> _activeQuests = new();
	private readonly List<QuestDefination> _completedQuests = new();
	private readonly List<QuestDefination> _notifiedQuests = new();

	private void Awake()
	{
		newQuestWindow.questTracker = this;
		questsWindow.questTracker = this;
		GameManager.Events.OnDayStart += OnDayStart;
		GameManager.Events.OnResourcesChange += OnResourceChange;
	}

	void OnResourceChange(ResourceIdentifier _)
	{
		if (questsWindow.IsOpen && _activeQuests.Count > 0)
			questsWindow.ShowQuests(_activeQuests);

		// Bad solution, but who cares, it's a jam
		foreach (var activeQuest in _activeQuests)
		{
			if (!_notifiedQuests.Contains(activeQuest) && CanCompleteQuest(activeQuest))
			{
				GameManager.Events.InvokeOnQuestBecomeReadyToComplete(activeQuest);
				_notifiedQuests.Add(activeQuest);
			}
		}
	}

	void OnDayStart(int _)
	{
		if (_activeQuests.Count >= maxAcceptedQuests)
			return;

		questsWindow.Hide();
		newQuestWindow.ShowQuests(GetEligableQuests());
	}

	public IEnumerable<QuestDefination> GetEligableQuests()
	{
		var eligableQuests = questDefinations.Where(q => q.IsEligable())
			.Except(_completedQuests).Except(_activeQuests).Take(3);
		return eligableQuests;
	}


	public void ShowQuests()
	{
		questsWindow.ShowQuests(_activeQuests);
		newQuestWindow.Hide();
	}

	public void ShowNewQuests()
	{
		newQuestWindow.ShowQuests(GetEligableQuests());
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
		return GameManager.ResourcesService.HasResources(quest.deliveryRequirments);
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

		_completedQuests.Add(quest);
		_activeQuests.Remove(quest);
		GameManager.ResourcesService.SpendResources(quest.deliveryRequirments);
		GameManager.ResourcesService.AddResources(quest.rewards);
		GameManager.Unlocks.UnlockResources(quest.rewardUnlocks);

		Debug.Log($"Completed Quest: {quest.displayName}");
		return true;
	}

}
