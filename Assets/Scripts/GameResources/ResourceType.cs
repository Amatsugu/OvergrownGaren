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
		[Display(Name = "Pulsar Seed"), Seed, Sprite(5), Cost(300)]
		SeedsPulsar,
		[Display(Name = "Pulsar Flower"), Item, Sprite(6), Cost(1000)]
		PulsarFlower,
		[Display(Name = "Datura Seed"), Seed, Sprite(7), Cost(20)]
		SeedsDatura,
		[Display(Name = "Datura Flower"), Item, Sprite(8), Cost(50)]
		DaturaFlower,
		[Display(Name = "Scarlet Ribbon Seed"), Seed, Sprite(9), Cost(40)]
		SeedsScarletRibbon,
		[Display(Name = "Scarlet Ribbon"), Item, Sprite(10), Cost(80)]
		ScarletRibbon,
		[Display(Name = "Sanguine Root Seed"), Seed, Sprite(11), Cost(90)]
		SeedsSanguineRoot,
		[Display(Name = "Sanguine Root"), Item, Sprite(12), Cost(130)]
		SanguineRoot,
		[Display(Name = "Flamelurker Gourd Seed"), Seed, Sprite(13), Cost(200)]
		SeedsFlamelurkerGourd,
		[Display(Name = "Flamelurker Gourd"), Item, Sprite(14), Cost(280)]
		FlamelurkerGourd,
		[Display(Name = "Entwined Rot Seed"), Seed, Sprite(15), Cost(2500)]
		SeedsEntwinedRotSeed,
		[Display(Name = "Entwined Rot Flower"), Item, Sprite(16), Cost(3200)]
		EntwinedRotFlower,
		[Display(Name = "Tulip Seed"), Seed, Sprite(17), Cost(7)]
		SeedsTulip,
		[Display(Name = "Tulip Flower"), Item, Sprite(18), Cost(12)]
		TulipFlower,
		[Display(Name = "San Pedro Cactus Seed"), Seed, Sprite(19), Cost(9)]
		SeedsSanPedroCactus,
		[Display(Name = "San Pedro Cactus Flower"), Item, Sprite(20), Cost(13)]
		SanPedroCactusFlower,
		[Display(Name = "Damocles Rose Seed"), Seed, Sprite(21), Cost(15)]
		SeedsDamoclesRose,
		[Display(Name = "Damocles Rose"), Item, Sprite(22), Cost(50)]
		DamoclesRose,
		[Display(Name = "Magnolia Seeds"), Seed, Sprite(23), Cost(30)]
		SeedsMagnolia,
		[Display(Name = "Magnolia Flower"), Item, Sprite(24), Cost(80)]
		MagnoliaFlower,
	}
}