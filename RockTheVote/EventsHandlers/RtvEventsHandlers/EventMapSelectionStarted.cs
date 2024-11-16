using RockTheVote.Enums;
using RockTheVote.Services;

namespace RockTheVote.EventsHandlers.RtvEventsHandlers
{
	public static class EventMapSelectionStarted
	{
		public static void Handler()
		{
			RockTheVoteService.Status = StatusRtv.SelectingMap;
			MapService.IsMapSelectionStarted = true;
		}
	}
}
