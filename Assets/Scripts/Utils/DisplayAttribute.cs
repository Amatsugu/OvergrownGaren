using System;

[AttributeUsage(
		AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Method | AttributeTargets.Class,
		AllowMultiple = false)]
public sealed class DisplayAttribute : Attribute
{
	public string Name { get; set; }
	public string Description { get; set; }
	public string Group { get; set; }
}