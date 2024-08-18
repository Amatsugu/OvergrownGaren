using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : UIPanel
{
	public QuestTracker questTracker;
	public QuestDisplay questPrefab;
	public RectTransform content;
	public void ShowQuests(IEnumerable<QuestDefination> activeQuests)
	{
		DestroyChildren(content);
		foreach (var quest in activeQuests)
		{
			var display = Instantiate(questPrefab, content);
			display.SetQuest(quest);
			var active = display.GetComponent<ActiveQuestUI>();

			active.abandonBtn.onClick.AddListener(() =>
			{
				questTracker.AbandonQuest(quest);
				Destroy(gameObject);
			});


			if(active.completeBtn.interactable = QuestTracker.CanCompleteQuest(quest))
			{
				active.completeBtn.onClick.AddListener(() =>
				{
					if(questTracker.CompleteQuest(quest))
						Destroy(gameObject);
				});
			}
		}
		Show();
	}
}
