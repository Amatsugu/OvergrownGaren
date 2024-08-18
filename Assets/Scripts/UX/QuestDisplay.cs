using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class QuestDisplay : MonoBehaviour
{
	public QuestDefination quest;

	public TextMeshProUGUI titleText;
	public TextMeshProUGUI descriptionText;
	public TextMeshProUGUI rewardsDisplay;

	private void Awake()
	{
		if (quest == null)
			return;
		UpdateDisplay();
	}

	public void SetQuest(QuestDefination quest)
	{
		this.quest = quest;
		UpdateDisplay();
	}

	private void UpdateDisplay()
	{
		titleText.SetText(quest.displayName);
		descriptionText.SetText(quest.description);
		rewardsDisplay.SetText(quest.GetRewardsText());
	}
}