using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Localization;
using RockTheVote.Enums;
using RockTheVote.Extensions;
using RockTheVote.Menus;
using RockTheVote.Proxys;
using RockTheVote.Services;

namespace RockTheVote.Commands
{
	public static class CSS_rtv_Command
	{
		#region Properties
		private static BasePlugin _plugin = Plugin.BasePlugin!;
		private static IStringLocalizer _localization = Plugin.BasePlugin!.Localizer;
		#endregion

		#region Handler
		[CommandHelper(whoCanExecute: CommandUsage.CLIENT_ONLY)]
		public static void Handler(CCSPlayerController? player, CommandInfo info)
		{
			if (!player.IsPlayerValid()) return;

			switch (RockTheVoteService.Status)
			{
				case (StatusRtv.SelectingMap):
					{
						var menu = new VoteMapMenu(_localization["Rtv.MenuTitle"], _plugin);
						menu.CreateMenuOptions(MapServiceProxy.GetMaps()?.Where(x => x.Name != Server.MapName));
						menu.Open(player!);
						return;
					}
				case (StatusRtv.LastRound):
					{
						player.PrintToChatSafe(_localization["NextMapMessage", MapService.NextMap?.VisibleName ?? "None"]);
						return;
					}
			}

			var minPlayersForStartRtv = RockTheVoteService.RockTheVoteConfig.RockTheVote.MinPlayersForStartRtv;
			if (!minPlayersForStartRtv.Equals(Utilities.GetPlayers().Where(x => x.IsPlayerValid() == true).Count()))
			{
				player.PrintToChatSafe(_localization["RockTheVoteConfig.MinPlayersForStartRtv", minPlayersForStartRtv]);
				return;
			}

			int requiredVotes = RockTheVoteService.RequiredNumberVotesChangeMap;

			MapServiceProxy.VoteRtv(player!);

			int countVotes = MapService.RtvVotes.Count;
			new Server().PrintToChatAllSafe(_localization["Rtv.PlayerSendRtv", countVotes, requiredVotes]);
		}
		#endregion
	}
}
