using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewQuestWindow : UIPanel
{
	[HideInInspector]
	public QuestTracker questTracker;
	public QuestDisplay questPrefab;
	public RectTransform content;

	public void ShowQuests(IEnumerable<QuestDefination> quests)
	{
		DestroyChildren(content);
		foreach (var quest in quests)
		{
			var display = Instantiate(questPrefab, content);
			display.SetQuest(quest);
			var btn = display.GetComponent<Button>();
			btn.onClick.AddListener(() =>
			{
				questTracker.AcceptQuest(quest);
				Hide();
			});
		}
		Show();
	}
}
