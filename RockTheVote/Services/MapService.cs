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

		public delegate void DeNominatedMapPlayer(CCSPlayerController player, MapReadModel map);
		public static event DeNominatedMapPlayer? DeNominatedMapPlayerEvent;
		#endregion

		#region Properties
		private static IStringLocalizer _localizer = Plugin.BasePlugin!.Localizer;
		public static string? NextMap { get; private set; }
		public static ConcurrentDictionary<CCSPlayerController, MapReadModel> VotesMap = new();
		public static ConcurrentDictionary<CCSPlayerController, MapReadModel> NominatedMaps = new();

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
			if(VotesMap.TryAdd(player, map))
			{
				VoteMapPlayerEvent?.Invoke(player, map);
			}
		}

		public static void ReVoteMap(CCSPlayerController player, MapReadModel map)
		{
			if(VotesMap.TryRemove(player, out MapReadModel? _map))
			{
				VoteMap(player, _map);
				ReVoteMapPlayerEvent?.Invoke(player, _map);
			}
			else
			{
				VoteMap(player, _map!);
			}
		}

		public static void NominatedMap(CCSPlayerController player, MapReadModel map)
		{
			if(NominatedMaps.TryAdd(player, map))
			{
				NominatedMapPlayerEvent?.Invoke(player, map);
			}
		}

		public static void DeNominatedMap(CCSPlayerController player, MapReadModel map)
		{
			if(NominatedMaps.TryRemove(player, out MapReadModel? _map))
			{
				NominatedMap(player, _map);
				DeNominatedMapPlayerEvent?.Invoke(player, _map);
			}
			else
			{
				NominatedMap(player, _map!);
			}
		}
		#endregion
	}
}
