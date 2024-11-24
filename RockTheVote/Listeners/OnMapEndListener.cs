using RockTheVote.Enums;
using RockTheVote.Services;

namespace RockTheVote.Listeners
{
	public static class OnMapEndListener
	{
		public static void Handler()
		{
			if (MapService.NextMap != null && RockTheVoteService.Status == StatusRtv.LastRound)
			{
				RockTheVoteService.SwitchMapForced(MapService.NextMap);
			}
			RockTheVoteService.ResetToFactorySettingsRtv();
		}
	}
}
