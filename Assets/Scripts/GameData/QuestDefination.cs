using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

[CreateAssetMenu(menuName = "Quest Defination")]
public class QuestDefination : ScriptableObject
{
	public string displayName;
	public string description;

	public QuestType type;

	public object requirements;
	public object rewards;

	public StringBuilder GetRewardsText()
	{
		var sb = new StringBuilder();

		return sb;
	}
}

public enum QuestType
{
	Grow,
	Payment,
	Delivery
}