using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using RockTheVote.Attributes;
using RockTheVote.Enums;
using RockTheVote.Extensions;
using RockTheVote.ReadModels;
using System.Collections.Concurrent;

namespace RockTheVote.Services
{
	[SupportsResetRtvElement]
	public static class MapService
	{
		#region Properties
		[ResetRtvElement("MapReadModel")]
		public static MapReadModel? NextMap { get; private set; }

		[ResetRtvElement("ConcurrentDictionary", "CCSPlayerController", "MapReadModel")]
		public static ConcurrentDictionary<CCSPlayerController, MapReadModel> VotesMap = new();

		[ResetRtvElement("ConcurrentDictionary", "CCSPlayerController", "MapReadModel")]
		public static ConcurrentDictionary<CCSPlayerController, MapReadModel> NominatedMaps = new();

		[ResetRtvElement("HashSet", "CCSPlayerController")]
		public static HashSet<CCSPlayerController> RtvVotes = new();

		[ResetRtvElement("Boolean")]
		public static bool IsMapSelectionStarted = false;
		#endregion

		#region Public
		public static IEnumerable<MapReadModel>? GetMaps()
		{
			return RockTheVoteService.MapsConfig?.Maps;
		}

		public static void SetNextMap(MapReadModel map)
		{
			RockTheVoteService.Status = StatusRtv.LastRound;
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

		public static bool DeVoteMap(CCSPlayerController player)
		{
			return VotesMap.TryRemove(player, out MapReadModel? _map);
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

		public static bool DeVoteRtv(CCSPlayerController player)
		{
			return RtvVotes.Remove(player);
		}

		public static void UnsubscribeAll(CCSPlayerController player)
		{
			DeNominatedMap(player, out MapReadModel _);
			DeVoteRtv(player);
			DeVoteMap(player);
		}

		public static void MapSelectionStart()
		{
			IsMapSelectionStarted = true;
		}

		public static void MapSelectionEnd()
		{
			IsMapSelectionStarted = false;
		}

		public static void ResetToFactorySettingsMapService()
		{
			NextMap = null;
			VotesMap = new();
			NominatedMaps = new();
			RtvVotes = new();
			IsMapSelectionStarted = false;
	}
		#endregion
	}
}
