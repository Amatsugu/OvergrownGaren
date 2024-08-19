using Resources;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

public static class Extensions
{
	public static string ToDisplayString<T>(this T value, bool plural = false) where T : Enum
	{
		var type = typeof(T);
		var prop = type.GetField(value.ToString());
		if (prop == null)
			return value.ToString();
		var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
		if (displayAttr == null)
			return value.ToString();
		var name = plural ? (displayAttr.NamePlural ?? $"{displayAttr.Name}s") : displayAttr.Name;
		return name ?? value.ToString();
	}

	public static int GetSpriteId<T>(this T value) where T: Enum
	{
		var type = typeof(T);
		var prop = type.GetField(value.ToString());
		var attr = prop.GetCustomAttribute<SpriteAttribute>();
		if (attr == null)
			throw new ArgumentException($"The enum '{value}' does not have an Sprite Attribute", nameof(value));
		return attr.Id;
	}

	public static string GetSprite<T>(this T value) where T : Enum
	{
		var id = value.GetSpriteId();
		return $"<sprite={id}>";
	}

	public static bool IsItem(this ResourceType resource) => resource.Has<ItemAttribute, ResourceType>();
	public static bool IsSeed(this ResourceType resource) => resource.Has<SeedAttribute, ResourceType>();

	public static bool Has<T, E>(this E value) where T : Attribute where E : Enum
	{
		var type = typeof(E);
		var prop = type.GetField(value.ToString());
		var attr = prop.GetCustomAttribute<T>();
		return attr != null;
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