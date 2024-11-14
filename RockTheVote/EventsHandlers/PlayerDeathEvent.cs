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
				if(MenuManager.GetActiveMenu(player!) is BaseMenuInstance menu)
				{
					if (menu.Menu is IRtvMenu)
					{
						menu.Close();
					}
				}
			}

			return HookResult.Continue;
		}
	}
}
