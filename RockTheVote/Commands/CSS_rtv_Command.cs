using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Localization;
using RockTheVote.Extensions;
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
			var minPlayersForStartRtv = RockTheVoteService.RockTheVoteConfig.RockTheVote.MinPlayersForStartRtv;
			if (!minPlayersForStartRtv.Equals(Utilities.GetPlayers().Where(x=>x.IsPlayerValid() == true).Count()))
			{
				player.PrintToChatSafe(_localization["RockTheVoteConfig.MinPlayersForStartRtv", minPlayersForStartRtv]);
				return;
			}

			if(MapService.NextMap != null)
			{
				player.PrintToChatSafe(_localization["NextMapMessage", MapService.NextMap]);
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
