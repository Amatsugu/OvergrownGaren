using Balconies;

using System.Collections;
using System.Collections.Generic;
using System.Text;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class PurchaseDisplay : UIHover
{
	public Image Image;
	public TextMeshProUGUI text;
	protected override void Update()
	{
		base.Update();
	}

	public void Show(BalconyView balcony)
	{
		var sb = new StringBuilder();
		SetActive(true);
		foreach (var res in balcony._buildCost)
			sb.AppendLine(res.ToString());
		text.SetText(sb);
		rTransform.position = balcony.transform.position;
	}

	public void Hide()
	{
		SetActive(false);
	}
}
