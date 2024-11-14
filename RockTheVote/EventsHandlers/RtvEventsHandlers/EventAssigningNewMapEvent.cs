using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RockTheVote.ReadModels;
using RockTheVote.Services;

namespace RockTheVote.EventsHandlers.RtvEventsHandlers
{
	public static class EventAssigningNewMapEvent
	{
		#region Properties
		private static ILogger _logger = Plugin.BasePlugin!.Logger;
		private static IStringLocalizer _localizer = Plugin.BasePlugin!.Localizer;
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
		}
		#endregion
	}
}
