using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Localization;
using RockTheVote.Extensions;
using RockTheVote.Proxys;

namespace RockTheVote.Commands
{
	public static class CSS_maps_Command
	{
		#region Properties
		private static IStringLocalizer _localizer = Plugin.BasePlugin!.Localizer;
		#endregion

		#region Handler
		public static void Handler(CCSPlayerController? player, CommandInfo info)
		{
			var maps = MapServiceProxy.GetMaps();

			if(maps == null)
			{
				player.PrintToChatSafe(_localizer["Logger.ListMapIsNull"]);
			}

			player.PrintToChatSafe(_localizer["MessageReceivedInConsole"]);
			player.PrintToConsoleSafe(
				"-----------Maps----------- \n ");
			foreach (var map in maps!)
			{
				player.PrintToConsoleSafe(
					$"\nName: {map.Name} \n" +
					$"VisibleName: {map.VisibleName} \n ");
			}
			player.PrintToConsoleSafe(
				"-------------------------- \n ");
		}
		#endregion
	}
}
