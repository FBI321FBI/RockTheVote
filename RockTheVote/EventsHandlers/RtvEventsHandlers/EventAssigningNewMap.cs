using CounterStrikeSharp.API;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RockTheVote.Extensions;
using RockTheVote.ReadModels;
using RockTheVote.Services;

namespace RockTheVote.EventsHandlers.RtvEventsHandlers
{
	public static class EventAssigningNewMap
	{
		#region Properties
		private static IStringLocalizer _localizer = Plugin.BasePlugin!.Localizer;
		private static ILogger _logger = Plugin.BasePlugin!.Logger;
		#endregion

		#region Handler
		public static void Handler(MapReadModel map)
		{
			var isInstantLaunchMap = RockTheVoteService.RockTheVoteConfig?.RockTheVote?.InstantLaunchMap;
			if (isInstantLaunchMap == null)
			{
				_logger.LogError(_localizer["ConfigError"]);
				return;
			}

			if (isInstantLaunchMap == true)
			{
				RockTheVoteService.SwitchMapForced(map);
			}

			new Server().PrintToChatAllSafe(_localizer["NextMapMessage", map.VisibleName]);
		}
		#endregion
	}
}
