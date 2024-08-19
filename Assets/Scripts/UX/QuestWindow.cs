using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : UIPanel
{
	[HideInInspector]
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
				Destroy(display.gameObject);
			});


			if(QuestTracker.CanCompleteQuest(quest))
			{
				active.completeBtn.onClick.AddListener(() =>
				{
					if(questTracker.CompleteQuest(quest))
						Destroy(display.gameObject);
				});
			}else
				active.completeBtn.interactable = false;
		}
		Show();
	}
}
