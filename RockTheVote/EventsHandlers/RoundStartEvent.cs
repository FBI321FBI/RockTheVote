using CounterStrikeSharp.API.Core;
using RockTheVote.Services;

namespace RockTheVote.EventsHandlers
{
	public static class RoundStartEvent
	{
		public static HookResult Handler(EventRoundStart @event, GameEventInfo info)
		{
			if(MapService.NextMap != null)
			{
				RockTheVoteService.SwitchMapForced(MapService.NextMap);
			}

			return HookResult.Continue;
		}
	}
}
