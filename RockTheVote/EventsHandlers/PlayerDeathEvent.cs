using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Menu;
using RockTheVote.Extensions;
using RockTheVote.Interface;

namespace RockTheVote.EventsHandlers
{
	public static class PlayerDeathEvent
	{
		public static HookResult Handler(EventPlayerDeath @event, GameEventInfo info)
		{
			var player = @event.Userid;

			if (player.IsPlayerValid())
			{
				var menu = (CenterHtmlMenuInstance?)MenuManager.GetActiveMenu(player!);
				if(menu?.Menu is IMenuMapSelection)
				{
					menu.Close();
				}
			}

			return HookResult.Continue;
		}
	}
}
