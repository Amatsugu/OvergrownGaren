using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;

using UnityEngine;

public class GrowthController : MonoBehaviour
{
	public Transform sprite;
	public float minSize = 0.1f;
	public float maxSize = 1.0f;
	public AnimationCurve curve;
	public virtual void OnGrow(float normalizedAge, bool isDead)
	{
		normalizedAge = math.clamp(normalizedAge, 0, 1);
		var scale = sprite.localScale;
		scale.y = math.lerp(minSize, maxSize, curve.Evaluate(normalizedAge));
		sprite.localScale = scale;
	}
}
