using CounterStrikeSharp.API.Core;
using RockTheVote.Extensions;
using RockTheVote.Proxys;

namespace RockTheVote.EventsHandlers
{
	public static class PlayerDisconnectEvent
	{
		public static HookResult Handler(EventPlayerDisconnect @event, GameEventInfo info)
		{
			var player = @event.Userid;
			if (player.IsPlayerValid())
			{
				MapServiceProxy.DeNominatedMap(player!);
			}
			return HookResult.Continue;
		}
	}
}
