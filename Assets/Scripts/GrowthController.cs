using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthController : MonoBehaviour
{
	public virtual void OnGrow(float normalizedAge, bool isDead)
	{
		normalizedAge = Mathf.Max(normalizedAge, 1);
		transform.localScale.Scale(new Vector3(0, normalizedAge, 0));
	}
}
