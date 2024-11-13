using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Localization;
using RockTheVote.Extensions;
using RockTheVote.ReadModels;
using System.Collections.Concurrent;

namespace RockTheVote.Services
{
	public static class MapService
	{
		#region Events
		public delegate void AssigningNewMap(MapReadModel map);
		public static event AssigningNewMap? AssigningNewMapEvent;

		public delegate void VoteMapPlayer(CCSPlayerController player, MapReadModel map);
		public static event VoteMapPlayer? VoteMapPlayerEvent;

		public delegate void ReVoteMapPlayer(CCSPlayerController player, MapReadModel map);
		public static event ReVoteMapPlayer? ReVoteMapPlayerEvent;

		public delegate void NominatedMapPlayer(CCSPlayerController player, MapReadModel map);
		public static event NominatedMapPlayer? NominatedMapPlayerEvent;

		public delegate void DeNominatedMapPlayer(CCSPlayerController player, MapReadModel oldMap);
		public static event DeNominatedMapPlayer? DeNominatedMapPlayerEvent;

		public delegate void ReNominatedMapPlayer(CCSPlayerController player, MapReadModel map);
		public static event ReNominatedMapPlayer? ReNominatedMapPlayerEvent;

		public delegate void PlayerVoteRtv(CCSPlayerController player, MapReadModel map);
		public static event PlayerVoteRtv? PlayerVoteRtvEvent;
		#endregion

		#region Properties
		private static IStringLocalizer _localizer = Plugin.BasePlugin!.Localizer;
		public static string? NextMap { get; private set; }
		public static ConcurrentDictionary<CCSPlayerController, MapReadModel> VotesMap = new();
		public static ConcurrentDictionary<CCSPlayerController, MapReadModel> NominatedMaps = new();
		public static ConcurrentDictionary<CCSPlayerController, MapReadModel> RtvVotes = new();

		#endregion

		#region Public
		public static IEnumerable<MapReadModel>? GetMaps()
		{
			return RockTheVoteService.MapsConfig?.Maps;
		}

		public static void SetNextMap(MapReadModel map)
		{
			NextMap = map.Name;
			AssigningNewMapEvent?.Invoke(map);
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

		public static void VoteMap(CCSPlayerController player, MapReadModel map)
		{
			if (VotesMap.TryAdd(player, map))
			{
				VoteMapPlayerEvent?.Invoke(player, map);
			}
		}

		public static void ReVoteMap(CCSPlayerController player, MapReadModel map)
		{
			if (VotesMap.TryRemove(player, out MapReadModel? _map))
			{
				VoteMap(player, _map);
				ReVoteMapPlayerEvent?.Invoke(player, _map);
			}
			else
			{
				VoteMap(player, _map!);
			}
		}

		public static bool NominatedMap(CCSPlayerController player, MapReadModel map)
		{
			if (NominatedMaps.TryAdd(player, map))
			{
				NominatedMapPlayerEvent?.Invoke(player, map);
				return true;
			}
			else
			{
				return false;
			}
		}

		public static void DeNominatedMap(CCSPlayerController player)
		{
			if (NominatedMaps.TryRemove(player, out MapReadModel? _map))
			{
				DeNominatedMapPlayerEvent?.Invoke(player, _map);
			}
		}

		public static void ReNominatedMap(CCSPlayerController player, MapReadModel map)
		{
			if (NominatedMaps.TryRemove(player, out MapReadModel? _map))
			{
				NominatedMap(player, map);
				ReNominatedMapPlayerEvent?.Invoke(player, _map);
			}
			else
			{
				NominatedMap(player, map);
			}
		}

		public static void VoteRtv(CCSPlayerController player, MapReadModel map)
		{
			if (RtvVotes.TryAdd(player, map))
			{
				PlayerVoteRtvEvent?.Invoke(player, map);
			}
		}
		#endregion
	}
}
