using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class SpriteAttribute : Attribute
{
	public int Id { get; set; }

	public SpriteAttribute(int id)
	{
		Id = id;
	}
}
