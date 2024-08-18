using Resources;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ResourceIdentifier : IEquatable<ResourceIdentifier>
{
	public ResourceType type;
	public int qty;


	public static implicit operator ResourceIdentifier((ResourceType type, int qty) data)
	{
		return new ResourceIdentifier
		{
			type = data.type,
			qty = data.qty
		};
	}

	public static ResourceIdentifier operator *(ResourceIdentifier resource, int multiplier) 
	{
		return (resource.type, resource.qty * multiplier);
	}

	public static ResourceIdentifier operator *(ResourceIdentifier resource, float multiplier)
	{
		return (resource.type, (int)(resource.qty * multiplier));
	}


	public static bool operator <(ResourceIdentifier lhs, ResourceIdentifier rhs) 
	{ 
		return lhs.type == rhs.type && lhs.qty < rhs.qty;
	}

	public static bool operator >(ResourceIdentifier lhs, ResourceIdentifier rhs)
	{
		return lhs.type == rhs.type && lhs.qty > rhs.qty;
	}

	public static bool operator <=(ResourceIdentifier lhs, ResourceIdentifier rhs)
	{
		return lhs.type == rhs.type && lhs.qty <= rhs.qty;
	}

	public static bool operator >=(ResourceIdentifier lhs, ResourceIdentifier rhs)
	{
		return lhs.type == rhs.type && lhs.qty >= rhs.qty;
	}

	public static bool operator <(ResourceIdentifier lhs, int qty)
	{
		return lhs.qty < qty;
	}

	public static bool operator >(ResourceIdentifier lhs, int qty)
	{
		return lhs.qty > qty;
	}

	public static bool operator <=(ResourceIdentifier lhs, int qty)
	{
		return lhs.qty <= qty;
	}

	public static bool operator >=(ResourceIdentifier lhs, int qty)
	{
		return lhs.qty >= qty;
	}

	public static bool operator ==(ResourceIdentifier lhs, ResourceIdentifier rhs)
	{
		return lhs.type == rhs.type && lhs.qty == rhs.qty;
	}

	public static bool operator !=(ResourceIdentifier lhs, ResourceIdentifier rhs)
	{
		return lhs.type == rhs.type && lhs.qty != rhs.qty;
	}

	public static bool operator ==(ResourceIdentifier lhs, int qty)
	{
		return lhs.qty == qty;
	}

	public static bool operator !=(ResourceIdentifier lhs, int qty)
	{
		return lhs.qty != qty;
	}

	public override readonly bool Equals(object obj)
	{
		return obj is ResourceIdentifier identifier && Equals(identifier);
	}

	public readonly bool Equals(ResourceIdentifier other)
	{
		return type == other.type &&
			   qty == other.qty;
	}

	public override readonly int GetHashCode()
	{
		return HashCode.Combine(type, qty);
	}
}
