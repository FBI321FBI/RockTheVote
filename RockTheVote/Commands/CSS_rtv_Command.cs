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

			MapServiceProxy.VoteRtv(player!);

			if (MapService.RtvVotes.Where(x => x == player).Single() == null)
			{
				int requiredVotes = RockTheVoteService.RequiredNumberVotesChangeMap;
				int countVotes = MapService.RtvVotes.Count;
				new Server().PrintToChatAllSafe(_localization["Rtv.PlayerVoteAgain", countVotes, requiredVotes]);
			}
		}
		#endregion
	}
}
