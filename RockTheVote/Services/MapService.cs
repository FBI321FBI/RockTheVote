using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Localization;
using RockTheVote.Extensions;
using RockTheVote.ReadModels;
using System.Collections.Concurrent;
using System.Diagnostics.Eventing.Reader;

namespace RockTheVote.Services
{
	public static class MapService
	{
		#region Properties
		private static IStringLocalizer _localizer = Plugin.BasePlugin!.Localizer;
		public static MapReadModel? NextMap { get; private set; }
		public static ConcurrentDictionary<CCSPlayerController, MapReadModel> VotesMap = new();
		public static ConcurrentDictionary<CCSPlayerController, MapReadModel> NominatedMaps = new();
		public static HashSet<CCSPlayerController> RtvVotes = new();
		#endregion

		#region Public
		public static IEnumerable<MapReadModel>? GetMaps()
		{
			return RockTheVoteService.MapsConfig?.Maps;
		}

		public static void SetNextMap(MapReadModel map)
		{
			NextMap = map;
		}

		public static LeftTimeReadModel? GetLeftTime()
		{
			var totalSeconds = new Server().GetRemainingMapTime();
			if (totalSeconds == null)
			{
				return null;
			}

			if (totalSeconds == 0)
			{
				return new LeftTimeReadModel { Minutes = 0, Seconds = 0, TotalSeconds = (int)totalSeconds };
			}

			var minutes = (totalSeconds / 60);
			var seconds = (totalSeconds % 60);

			return new LeftTimeReadModel { Minutes = (int)minutes, Seconds = (int)seconds, TotalSeconds = (int)totalSeconds };
		}

		public static bool VoteMap(CCSPlayerController player, MapReadModel map)
		{
			if (VotesMap.TryAdd(player, map))
			{
				return true;
			}
			return false;
		}

		public static bool ReVoteMap(CCSPlayerController player, MapReadModel map)
		{
			if (VotesMap.TryRemove(player, out MapReadModel? _map))
			{
				return VoteMap(player, _map);
			}
			else
			{
				return VoteMap(player, _map!);
			}
		}

		public static bool NominatedMap(CCSPlayerController player, MapReadModel map)
		{
			return NominatedMaps.TryAdd(player, map);
		}

		public static bool DeNominatedMap(CCSPlayerController player, out MapReadModel map)
		{
			return NominatedMaps.TryRemove(player, out map!);
		}

		public static bool ReNominatedMap(CCSPlayerController player, MapReadModel map, out MapReadModel oldMap)
		{
			if (NominatedMaps.TryRemove(player, out oldMap!))
			{
				return NominatedMap(player, map);
			}
			else
			{
				return NominatedMap(player, map);
			}
		}

		public static bool VoteRtv(CCSPlayerController player)
		{
			return RtvVotes.Add(player);
		}
		#endregion
	}
}
