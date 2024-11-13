using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Menu;
using RockTheVote.Enums;
using RockTheVote.Extensions;
using RockTheVote.Menus;
using RockTheVote.Services;

namespace RockTheVote.Commands
{
	public static class CSS_nominate_Command
	{
		#region Properties
		private static BasePlugin _plugin = Plugin.BasePlugin!;
		#endregion

		#region Handler
		[CommandHelper(whoCanExecute: CommandUsage.CLIENT_ONLY)]
		public static void Handler(CCSPlayerController? player, CommandInfo info)
		{
			if (!player.IsPlayerValid())
			{
				return;
			}

			var maps = MapService.GetMaps();
			var nominatedMenu = new MenuMapSelection(_plugin).OrderMenu(TypeMenuMapSelection.Nominated);
			nominatedMenu.CreateMenuOptions(maps);
			MenuManager.OpenCenterHtmlMenu(Plugin.BasePlugin!, player!, (CenterHtmlMenu)nominatedMenu);
		}
		#endregion
	}
}
