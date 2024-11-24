using CounterStrikeSharp.API.Core;
using RockTheVote.Services;

namespace RockTheVote.EventsHandlers
{
	public static class CsWinPanelMatchEvent
	{
		public static HookResult Handler(EventCsWinPanelMatch @event, GameEventInfo info)
		{
			var maps = MapService.GetMaps();
			if(maps != null)
			{
				RockTheVoteService.SwitchMapForced(maps.ElementAt(new Random().Next(0, maps.Count())));
			}
			//RockTheVoteService.ResetToFactorySettingsRtv();
			return HookResult.Continue;
		}
	}
}
