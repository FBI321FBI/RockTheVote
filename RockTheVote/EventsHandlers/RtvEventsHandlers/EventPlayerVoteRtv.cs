using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Timers;
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
			if (MapService.RtvVotes.Count.Equals(RockTheVoteService.RequiredNumberVotesChangeMap))
			{
				Timer timer = new Timer(1, () =>
				{
					int secondsCount = 0;
					if (secondsCount != 3)
					{
						new Server().PrintToChatAllSafe($"{_localizer["Rtv.StartVoting"]}");
						secondsCount++;
					}
					else
					{
						RockTheVoteService.StartVoteNewMap();
						return;
					}
				}, TimerFlags.REPEAT);
			}
		}
		#endregion
	}
}
