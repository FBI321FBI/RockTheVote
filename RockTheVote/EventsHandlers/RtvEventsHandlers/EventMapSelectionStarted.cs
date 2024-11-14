using RockTheVote.Services;

namespace RockTheVote.EventsHandlers.RtvEventsHandlers
{
	public static class EventMapSelectionStarted
	{
		public static void Handler()
		{
			MapService.IsMapSelectionStarted = true;
		}
	}
}
