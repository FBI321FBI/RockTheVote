using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Localization;
using RockTheVote.Extensions;
using RockTheVote.ReadModels;

namespace RockTheVote.EventsHandlers.RtvEventsHandlers
{
	public static class EventVoteMapPlayer
	{
		#region Properties
		private static IStringLocalizer _localizer = Plugin.BasePlugin.Localizer;
		#endregion

		#region Handler
		public static void Handler(CCSPlayerController player, MapReadModel map)
		{
			player.PrintToChatSafe(_localizer["Rtv.PlayerVote", player.PlayerName, map.VisibleName ?? "None"]);
		}
		#endregion
	}
}
