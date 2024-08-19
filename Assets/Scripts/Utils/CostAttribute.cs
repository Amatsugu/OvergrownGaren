using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class CostAttribute : Attribute
{
	public int Cost { get; set; }
	public CostAttribute(int cost)
	{
		Cost = cost;
	}
}
