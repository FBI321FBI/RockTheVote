using CounterStrikeSharp.API.Core;
using RockTheVote.Extensions;
using RockTheVote.Services;

namespace RockTheVote.EventsHandlers
{
	public static class PlayerDisconnectEvent
	{
		public static HookResult Handler(EventPlayerDisconnect @event, GameEventInfo info)
		{
			var player = @event.Userid;
			if (player.IsPlayerValid())
			{
				MapService.UnsubscribeAll(player!);
			}
			return HookResult.Continue;
		}
	}
}
