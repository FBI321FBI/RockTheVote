using CounterStrikeSharp.API.Core;
using RockTheVote.ReadModels;
using RockTheVote.Services;

namespace RockTheVote.Proxys
{
	public static class MapServiceProxy
	{
		#region Properties
		public static int RequiredRtvVotes => RockTheVoteService.RockTheVoteConfig.RockTheVote.MinPlayersForStartRtv;
		#endregion

		#region Events
		public delegate void AssigningNewMap(MapReadModel map);
		/// <summary>
		/// Событие, когда назначилась новая карта.
		/// </summary>
		public static event AssigningNewMap? AssigningNewMapEvent;

		public delegate void VoteMapPlayer(CCSPlayerController player, MapReadModel map);
		/// <summary>
		/// Событие, когда игрок отдал голос за следующую карту.
		/// </summary>
		public static event VoteMapPlayer? VoteMapPlayerEvent;

		public delegate void ReVoteMapPlayer(CCSPlayerController player, MapReadModel map);
		/// <summary>
		/// Событие, когда игрок перевыбрал голос за следующую карту.
		/// </summary>
		public static event ReVoteMapPlayer? ReVoteMapPlayerEvent;

		public delegate void NominatedMapPlayer(CCSPlayerController player, MapReadModel map);
		/// <summary>
		/// Событие, когда игрок номинировал карту.
		/// </summary>
		public static event NominatedMapPlayer? NominatedMapPlayerEvent;

		public delegate void DeNominatedMapPlayer(CCSPlayerController player, MapReadModel oldMap);
		/// <summary>
		/// Событие, когда игрок убрал голос за номинацию карты.
		/// </summary>
		public static event DeNominatedMapPlayer? DeNominatedMapPlayerEvent;

		public delegate void ReNominatedMapPlayer(CCSPlayerController player, MapReadModel map, MapReadModel oldMap);
		/// <summary>
		/// Событие, когда игрок номинирует другую карту с условием, что он уже номинировал. 
		/// </summary>
		public static event ReNominatedMapPlayer? ReNominatedMapPlayerEvent;

		public delegate void PlayerVoteRtv(CCSPlayerController player);
		/// <summary>
		/// Событие, когда игрок прописывает команду rtv.
		/// </summary>
		public static event PlayerVoteRtv? PlayerVoteRtvEvent;
		#endregion

		#region Publuic
		/// <summary>
		/// Метод получения всех карт.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<MapReadModel>? GetMaps()
		{
			return MapService.GetMaps();
		}

		/// <summary>
		/// Метод установки следующей карты.
		/// </summary>
		/// <param name="map">Карта.</param>
		public static void SetNextMap(MapReadModel map)
		{
			MapService.SetNextMap(map);
			AssigningNewMapEvent?.Invoke(map);
		}

		/// <summary>
		/// Получение оставшигося времени до смены карты.
		/// </summary>
		/// <returns>Оставшееся время до смены карты.</returns>
		public static LeftTimeReadModel? GetLeftTime()
		{
			return MapService.GetLeftTime();
		}

		/// <summary>
		/// Голосование за карту.
		/// </summary>
		/// <param name="player">Игрок.</param>
		/// <param name="map">Игрок.</param>
		/// <returns>true, если получилось проголосовать и false, если уже проголосовал.</returns>
		public static bool VoteMap(CCSPlayerController player, MapReadModel map)
		{
			bool result = MapService.VoteMap(player, map);
			if (result)
			{
				VoteMapPlayerEvent?.Invoke(player, map);
			}
			return result;
		}

		/// <summary>
		/// Переголосовать за карту.
		/// </summary>
		/// <param name="player">Игрок.</param>
		/// <param name="map">Карта.</param>
		/// <returns>true, если получилось проголосовать и false, если не получилось проголосовать.</returns>
		public static bool ReVoteMap(CCSPlayerController player, MapReadModel map)
		{
			bool result = MapService.ReVoteMap(player, map);
			if (result)
			{
				ReVoteMapPlayerEvent?.Invoke(player, map);
			}
			return result;
		}

		/// <summary>
		/// Номинировать карту.
		/// </summary>
		/// <param name="player">Игрок.</param>
		/// <param name="map">Карта.</param>
		/// <returns>true, если получилось проголосовать и false, если уже номинировал.</returns>
		public static bool NominatedMap(CCSPlayerController player, MapReadModel map)
		{
			bool result = MapService.NominatedMap(player, map);
			if (result)
			{
				NominatedMapPlayerEvent?.Invoke(player, map);
			}
			return result;
		}

		/// <summary>
		/// Убрать голос за номинацию карты.
		/// </summary>
		/// <param name="player">Игрок.</param>
		/// <returns>true, если успешно убрал голосо и false, если не было голоса.</returns>
		public static bool DeNominatedMap(CCSPlayerController player)
		{
			bool result = MapService.DeNominatedMap(player, out MapReadModel map);
			if (result)
			{
				DeNominatedMapPlayerEvent?.Invoke(player, map);
			}
			return result;
		}

		/// <summary>
		/// Переноминировать крату.
		/// </summary>
		/// <param name="player">Игрок.</param>
		/// <param name="map">Карта.</param>
		/// <returns>true, если успешно переголосовал и false, если не смог переголосовать.</returns>
		public static bool ReNominatedMap(CCSPlayerController player, MapReadModel map)
		{
			bool result = MapService.ReNominatedMap(player, map, out MapReadModel oldMap);
			if (result)
			{
				ReNominatedMapPlayerEvent?.Invoke(player, map, oldMap);
			}
			return result;
		}

		/// <summary>
		/// Проголосовать за начало rtv.
		/// </summary>
		/// <param name="player">Игрок.</param>
		/// <returns>true, если получилось проголосовать и false, если не получиось проголосовать.</returns>
		public static bool VoteRtv(CCSPlayerController player)
		{
			bool result = MapService.VoteRtv(player);
			if (result)
			{
				PlayerVoteRtvEvent?.Invoke(player);
			}

			return result;
		}
		#endregion
	}
}
