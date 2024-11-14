using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Menu;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RockTheVote.Extensions;
using RockTheVote.Interface;
using RockTheVote.Menus;
using RockTheVote.Proxys;
using RockTheVote.ReadModels;
using RockTheVote.ReadModels.Configs;
using Timer = CounterStrikeSharp.API.Modules.Timers.Timer;

namespace RockTheVote.Services
{
	public static class RockTheVoteService
	{
		#region Properties 
		private static JsonReaderConfigs _jsonReaderConfig = new JsonReaderConfigs(Plugin.BasePlugin!);
		private static BasePlugin _plugin = Plugin.BasePlugin!;
		private static ILogger _logger = Plugin.BasePlugin!.Logger;
		private static IStringLocalizer _localization = Plugin.BasePlugin!.Localizer;

		public static int RequiredNumberVotesChangeMap =>
			(int)Math.Round(Utilities.GetPlayers().Where(x => x.IsPlayerValid() == true).Count() *
				(RockTheVoteConfig.RockTheVote.PercentageForcedVoting / 100D));
		#endregion

		#region Public
		public static RockTheVoteConfigReadModel? RockTheVoteConfig =>
			JsonConvert.DeserializeObject<RockTheVoteConfigReadModel>(
				File.ReadAllText(_jsonReaderConfig.GetFullPathJsonFile("RockTheVoteConfig.json", "RockTheVoteConfig")));

		public static MapsConfigReadModel? MapsConfig =>
			JsonConvert.DeserializeObject<MapsConfigReadModel>(
				File.ReadAllText(_jsonReaderConfig.GetFullPathJsonFile("Maps.json", "RockTheVoteConfig")));

		public static void StartVoteNewMap()
		{
			var maps = MapServiceProxy.GetMaps();
			var nominatedMenu = new RtvMenu(_localization["Rtv.MenuTitle"], _plugin);
			nominatedMenu.CreateMenuOptions(maps);
			nominatedMenu.OpenToAll();

			Timer timer = new Timer(10000, () =>
			{
				Utilities.GetPlayers().Where(x => x.IsPlayerValid() == true).ToList().ForEach(x =>
				{
					if (MenuManager.GetActiveMenu(x) is BaseMenuInstance menu)
					{
						if (menu is IRtvMenu)
						{
							menu.Close();
						}
					}
				});
			});
			timer.Kill();
		}

		public static void SwitchMapForced(MapReadModel map)
		{
			Server.ExecuteCommand($"ds_workshop_changelevel {map.Name}");
		}

		public static void SwitchMapNextRound(MapReadModel map)
		{
			MapServiceProxy.SetNextMap(map);
		}
		#endregion
	}
}
