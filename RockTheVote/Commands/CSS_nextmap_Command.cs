using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Localization;
using RockTheVote.Extensions;
using RockTheVote.Proxys;
using RockTheVote.Services;

namespace RockTheVote.Commands
{
	public static class CSS_nextmap_Command
	{
		#region Properties
		private static IStringLocalizer _localizer = Plugin.BasePlugin!.Localizer;
		#endregion

		[CommandHelper(minArgs: 1, usage:"map")]
		[RequiresPermissions("@RTV/nextmap")]
		public static void Handler(CCSPlayerController? player, CommandInfo info)
		{
			var map = info.ArgByIndex(1);
			var nexMap = MapService.GetMaps()?.Where(x => x.Name == map).FirstOrDefault();

			if (nexMap == null)
			{
				player.PrintToChatSafe(_localizer["Admin.SetNextMapError", map]);
				return;
			}

			new Server().PrintToChatAllSafe(_localizer["Admin.SetNextMap", player?.PlayerName ?? _localizer["ServerPrefix"], nexMap.VisibleName]);
			MapService.SetNextMap(nexMap);
		}
	}
}
