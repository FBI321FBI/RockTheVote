﻿using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Localization;
using RockTheVote.Extensions;
using RockTheVote.Proxys;
using RockTheVote.Services;

namespace RockTheVote.Commands
{
	public static class CSS_timeleft_Command
	{
		#region Properies
		private static IStringLocalizer _localizer = Plugin.BasePlugin!.Localizer;
		#endregion

		#region Handler
		public static void Handler(CCSPlayerController? player, CommandInfo info)
		{
			var timeLeft = MapServiceProxy.GetLeftTime();

			if (timeLeft == null)
			{
				player.PrintToChatSafe(_localizer["MapTimeInfinity"]);
				return;
			}

			if (MapService.NextMap != null)
			{
				player.PrintToChatSafe(_localizer["NextMapMessage", MapService.NextMap.VisibleName]);
				return;
			}

			if (timeLeft.TotalSeconds == 0)
			{
				player.PrintToChatSafe(_localizer["ZeroSecondsNextMap"]);
				return;
			}

			player.PrintToChatSafe(_localizer["TimeLeft", timeLeft.Minutes!, timeLeft.Seconds!]);
		}
		#endregion
	}
}
