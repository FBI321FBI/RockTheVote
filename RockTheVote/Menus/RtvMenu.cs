using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Menu;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RockTheVote.Extensions;
using RockTheVote.Interface;
using RockTheVote.Proxys;
using RockTheVote.ReadModels;
using RockTheVote.Services;

namespace RockTheVote.Menus
{
	public class RtvMenu : CenterHtmlMenu, IRtvMenu
	{
		#region Properties
		private IEnumerable<MapReadModel>? _maps = MapServiceProxy.GetMaps();
		private ILogger _logger = Plugin.BasePlugin!.Logger;
		private IStringLocalizer _localizer = Plugin.BasePlugin.Localizer;
		private int _numberOfNominatedMaps = RockTheVoteService.RockTheVoteConfig.RockTheVote.NumberOfNominatedMaps;
		#endregion

		#region .ctor
		public RtvMenu(string title, BasePlugin plugin) : base(title, plugin)
		{
		}
		#endregion

		#region Public
		public void CreateMenuOptions(IEnumerable<MapReadModel>? maps)
		{
			if (maps == null)
			{
				_logger.LogInformation(_localizer["Logger.ListMapIsNull"]);
				return;
			}

			var maxMapsOnRtvMenu = RockTheVoteService.RockTheVoteConfig.RockTheVote.MaxMapsOnRtvMenu;
			var numberOfNominatedMaps = RockTheVoteService.RockTheVoteConfig.RockTheVote.NumberOfNominatedMaps;
			var mapCount = maps.Count();
			var defaultMapCount = Math.Max(0, maxMapsOnRtvMenu - Math.Min(MapService.NominatedMaps.Count, numberOfNominatedMaps));

			if (MapService.NominatedMaps.Count != 0)
			{
				for (var i = 0; i < numberOfNominatedMaps; i++)
				{
					var maxNumberOfNominatedMap = Math.Min(MapService.NominatedMaps.Count, numberOfNominatedMaps);
					var nominatedMap = MapService.NominatedMaps.ElementAt(new Random().Next(0, maxNumberOfNominatedMap)).Value;
					bool? isMapVoted = MapService.VotesMap.Where(x => x.Value.Name == nominatedMap.Name).SingleOrDefault().Value?.Equals(nominatedMap);
					AddMenuOption(nominatedMap.VisibleName ?? "None", SelectedItem, !isMapVoted ?? false);
				}
			}

			for (var i = 0; i < Math.Min(maxMapsOnRtvMenu, defaultMapCount); i++)
			{
				var map = maps.ElementAt(new Random().Next(0, Math.Min(maxMapsOnRtvMenu, defaultMapCount)));
				bool? isMapVoted = MapService.VotesMap.Where(x => x.Value.Name == map.Name).SingleOrDefault().Value?.Equals(map);
				AddMenuOption(map.VisibleName ?? "None", SelectedItem, !isMapVoted ?? false);
			}
		}
		#endregion

		#region Private
		private void SelectedItem(CCSPlayerController player, ChatMenuOption option)
		{
			if (_maps == null)
			{
				_logger.LogInformation(_localizer["Logger.ListMapIsNull"]);
				return;
			}

			MapServiceProxy.VoteMap(player, _maps.Where(x => x.VisibleName == option.Text).Distinct().First());

			MenuOptions.ForEach(option => { option.Disabled = false; });
			MenuOptions.Where(x => x.Text == option.Text).ToList().ForEach(x => { x.Disabled = true; });
		}
		#endregion
	}
}
