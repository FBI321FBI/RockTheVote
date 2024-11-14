using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Localization;
using RockTheVote.Extensions;
using RockTheVote.Menus;
using RockTheVote.Proxys;

namespace RockTheVote.Commands
{
	public static class CSS_nominate_Command
	{
		#region Properties
		private static BasePlugin _plugin = Plugin.BasePlugin!;
		private static IStringLocalizer _localization = Plugin.BasePlugin!.Localizer;
		#endregion

		#region Handler
		[CommandHelper(whoCanExecute: CommandUsage.CLIENT_ONLY)]
		public static void Handler(CCSPlayerController? player, CommandInfo info)
		{
			if (!player.IsPlayerValid())
			{
				return;
			}

			var maps = MapServiceProxy.GetMaps();
			var nominatedMenu = new NominatedMenu(_localization["Nominate.MenuTitle"], _plugin);
			nominatedMenu.CreateMenuOptions(maps);
			nominatedMenu.Open(player!);
		}
		#endregion
	}
}
