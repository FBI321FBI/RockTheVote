using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RockTheVote.ReadModels;
using RockTheVote.ReadModels.Configs;

namespace RockTheVote.Services
{
	public static class RockTheVoteService
	{
		#region Properties 
		private static JsonReaderConfigs _jsonReaderConfig = new JsonReaderConfigs(Plugin.BasePlugin!);
		private static ILogger _logger = Plugin.BasePlugin!.Logger;
		#endregion

		#region Public
		public static RockTheVoteConfigReadModel? RockTheVoteConfig =>
			JsonConvert.DeserializeObject<RockTheVoteConfigReadModel>(
				File.ReadAllText(_jsonReaderConfig.GetFullPathJsonFile("RockTheVoteConfig.json", "RockTheVoteConfig")));

		public static MapsConfigReadModel? MapsConfig =>
			JsonConvert.DeserializeObject<MapsConfigReadModel>(
				File.ReadAllText(_jsonReaderConfig.GetFullPathJsonFile("Maps.json", "RockTheVoteConfig")));

		public static void SwitchMapForced(MapReadModel map)
		{
			Server.ExecuteCommand($"ds_workshop_changelevel {map.Name}");
		}

		public static void SwitchMapNextRound(MapReadModel map)
		{
			MapService.SetNextMap(map);
		}
		#endregion
	}
}
