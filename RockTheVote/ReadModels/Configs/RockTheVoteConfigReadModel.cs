namespace RockTheVote.ReadModels.Configs
{
	public class RockTheVoteConfigReadModel
	{
		public RtvReadModel? RockTheVote { get; set; }
	}

	public class RtvReadModel
	{
		public int PercentageForcedVoting { get; set; }
		public int MinPlayersForStartRtv { get; set; }
		public int NumberOfNominatedMaps { get; set; }
		public int MaxMapsOnRtvMenu { get; set; }
		public bool InstantLaunchMap { get; set; }
	}
}
