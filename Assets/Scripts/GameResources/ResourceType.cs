namespace Resources
{
    public enum ResourceType
    {
		[Display(Name = "Coin"), Sprite(0)]
        Coins,
		[Display(Name = "Common Seed"), Seed, Sprite(1)]
        SeedsCommon,
		[Display(Name = "Super Seed"), Seed, Sprite(2)]
        SeedsSuper,
		[Display(Name = "Flower"), Item, Sprite(3)]
		Flower,
	}
}