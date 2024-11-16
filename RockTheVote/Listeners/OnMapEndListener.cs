using RockTheVote.Services;

namespace RockTheVote.Listeners
{
	public static class OnMapEndListener
	{
		public static void Handler()
		{
			RockTheVoteService.ResetToFactorySettingsRtv();
		}
	}
}
