using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Menu;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RockTheVote.Extensions;
using RockTheVote.Interface;
using RockTheVote.ReadModels;
using RockTheVote.Services;

namespace RockTheVote.Menus
{
	public class NominatedMenu : CenterHtmlMenu, IMenuMapSelection
	{
		#region Properties
		private IEnumerable<MapReadModel>? _maps = MapService.GetMaps();
		private ILogger _logger = Plugin.BasePlugin!.Logger;
		private IStringLocalizer _localizer = Plugin.BasePlugin.Localizer;
		#endregion

		#region .ctor
		public NominatedMenu(string title, BasePlugin plugin) : base(title, plugin)
		{
			MapService.NominatedMapPlayerEvent += NominatedMapPlayer;
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

			foreach (var map in maps)
			{
				bool? isNominated = MapService.NominatedMaps.Where(x => x.Value.Name == map.Name).SingleOrDefault().Value?.Equals(map);
				AddMenuOption(map.VisibleName ?? "None", SelectedItem, !isNominated ?? false);
			}
		}
		#endregion

		#region Private
		private void SelectedItem(CCSPlayerController player, ChatMenuOption option)
		{
			if(_maps == null)
			{
				_logger.LogInformation(_localizer["Logger.ListMapIsNull"]);
				return;
			}

			MenuOptions.ForEach(option => { option.Disabled = false; });
			MenuOptions.Where(x => x.Text == option.Text).ToList().ForEach(x => { x.Disabled = true; });

			var map = _maps.Where(x => x.VisibleName == option.Text).Distinct().Single();

			if (!MapService.NominatedMap(player, map))
			{
				MapService.ReNominatedMap(player, map);
			}
		}
		#endregion

		#region Subscribers
		public void NominatedMapPlayer(CCSPlayerController player, MapReadModel map)
		{
			player.PrintToChatSafe(_localizer["Nominate.SelectedMap", map.VisibleName ?? string.Empty]);
		}
		#endregion
	}
}
