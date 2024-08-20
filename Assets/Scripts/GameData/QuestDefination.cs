using Resources;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		if(rewardUnlocks.Length > 0)
			sb.Append("Unlocks: ").AppendLine(string.Join("", rewardUnlocks.Select(r => r.GetSprite())));
		return sb;
	}

	public bool IsEligable()
	{
		foreach (var res in deliveryRequirments)
		{
			if(res.type == ResourceType.Coins)
				continue;
			var seeds = GameManager.PlanterController.GetSeedsFor(res.type);
			foreach (var seed in seeds)
			{
				if(!GameManager.Unlocks.IsUnlocked(seed))
					return false;
			}
		}
		return true;
	}

	public StringBuilder GetDescription()
	{
		var sb = new StringBuilder();
		if(!string.IsNullOrWhiteSpace(displayName))
			sb.AppendLine(description);

		sb.AppendLine("Requirements:");
		foreach (var res in deliveryRequirments)
		{
			if (GameManager.ResourcesService.HasResources(res))
				sb.Append("<color=#00ff00>");
			else
				sb.Append("<color=#ff0000>");

			sb.Append(res.ToString()).Append("</color>").Append(' ');
		}

		return sb;
	}
}