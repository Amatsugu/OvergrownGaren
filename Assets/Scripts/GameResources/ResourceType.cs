namespace Resources
{
	public enum ResourceType
	{
		[Display(Name = "Coin"), Sprite(0)]
		Coins,
		[Display(Name = "Common Seed"), Seed, Sprite(1), Cost(2)]
		SeedsCommon,
		[Display(Name = "Super Seed"), Seed, Sprite(2), Cost(4)]
		SeedsSuper,
		[Display(Name = "Flower"), Item, Sprite(3), Cost(4)]
		Flower,
		[Display(Name = "Super Flower"), Item, Sprite(4), Cost(8)]
		SuperFlower,
	}
}