using Resources;

using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

[CreateAssetMenu(menuName = "Quest Defination")]
public class QuestDefination : ScriptableObject
{
	public string displayName;
	public string description;


	public ResourceIdentifier[] deliveryRequirments;
	public ResourceIdentifier[] rewards;
	public ResourceType[] rewardUnlocks;

	public StringBuilder GetRewardsText()
	{
		var sb = new StringBuilder();
		foreach (var reward in rewards)
			sb.AppendLine(reward.ToString());
		return sb;
	}

	public bool IsEligable()
	{
		foreach (var res in deliveryRequirments)
		{
			if (!GameManager.Unlocks.IsUnlocked(res.type))
				return false;
		}
		return true;
	}
}