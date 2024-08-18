using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

public static class Extensions
{
	public static string ToDisplayString<T>(this T value) where T : Enum
	{
		var type = typeof(T);
		var prop = type.GetField(value.ToString());
		if (prop == null)
			return value.ToString();
		var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
		if (displayAttr == null)
			return value.ToString();
		return displayAttr.Name ?? value.ToString();
	}

	public static ResourceIdentifier[] Multiply(this ResourceIdentifier[] resources, int multi)
	{
		var result = new ResourceIdentifier[resources.Length];
		for (int i = 0; i < resources.Length; i++)
		{
			result[i] = resources[i] * multi;
		}

		return result;
	}

	public static ResourceIdentifier[] Multiply(this ResourceIdentifier[] resources, float multi)
	{
		var result = new ResourceIdentifier[resources.Length];
		for (int i = 0; i < resources.Length; i++)
		{
			result[i] = resources[i] * multi;
		}

		return result;
	}
}