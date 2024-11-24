using RockTheVote.Enums;
using RockTheVote.ReadModels;
using RockTheVote.Services;

namespace RockTheVote.Listeners
{
	public static class OnTickListener
	{
		public static void Handler()
		{
			var leftTime = MapService.GetLeftTime() ?? new LeftTimeReadModel() { TotalSeconds = 99999};

			if (leftTime.TotalSeconds == 0
				&& RockTheVoteService.Status == StatusRtv.None)
			{
				RockTheVoteService.StartVoteNewMap();
			}
		}
	}
}
