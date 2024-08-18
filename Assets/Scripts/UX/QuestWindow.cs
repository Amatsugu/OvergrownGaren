using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : UIPanel
{
	public QuestDisplay questPrefab;
	public RectTransform content;
	public void ShowQuests(IEnumerable<QuestDefination> activeQuests)
	{
		DestroyChildren(content);
		foreach (var quest in activeQuests)
		{
			var display = Instantiate(questPrefab, content);
			display.SetQuest(quest);
			var btn = display.GetComponent<Button>();
			btn.onClick.AddListener(() =>
			{
				GameManager.Events.InvokeOnQuestAccepted(quest);
			});
		}
		Show();
	}
}
