using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Localization;
using RockTheVote.Extensions;
using RockTheVote.Services;
using Timer = CounterStrikeSharp.API.Modules.Timers.Timer;

namespace RockTheVote.EventsHandlers.RtvEventsHandlers
{
	public static class EventPlayerVoteRtv
	{
		#region Properties
		private static IStringLocalizer _localizer = Plugin.BasePlugin!.Localizer;
		#endregion

		#region Handler
		public static void Handler(CCSPlayerController player)
		{
			if (MapService.RtvVotes.Count.Equals(RockTheVoteService.RequiredNumberVotesChangeMap)
				&& MapService.NextMap == null)
			{
				int secondsCount = 0;
				int maxSecondsCount = 3;
				new Timer(1, () =>
				{
					new Server().PrintToChatAllSafe($"{_localizer["Rtv.StartVoting", maxSecondsCount - secondsCount]}");
					secondsCount++;
					new Timer(1, () =>
					{
						new Server().PrintToChatAllSafe($"{_localizer["Rtv.StartVoting", maxSecondsCount - secondsCount]}");
						secondsCount++;
						new Timer(1, () =>
						{
							new Server().PrintToChatAllSafe($"{_localizer["Rtv.StartVoting", maxSecondsCount - secondsCount]}");
							RockTheVoteService.StartVoteNewMap();
						});
					});
				});
			}
		}
	}
	#endregion
}
