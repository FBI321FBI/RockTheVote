using CounterStrikeSharp.API.Core;
using RockTheVote.Services;

namespace RockTheVote.EventsHandlers
{
	public static class RoundEndEvent
	{
		public static HookResult Handler(EventRoundEnd @event, GameEventInfo info)
		{
			if(MapService.NextMap != null)
			{
				RockTheVoteService.SwitchMapForced(MapService.NextMap);
			}

			return HookResult.Continue;
		}
	}
}
