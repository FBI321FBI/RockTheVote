using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Menu;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RockTheVote.Interface;
using RockTheVote.ReadModels;
using RockTheVote.Services;

namespace RockTheVote.Menus
{
	public class RtvMenu : CenterHtmlMenu, IMenuMapSelection
	{
		#region Properties
		#region Properties
		private IEnumerable<MapReadModel>? _maps = MapService.GetMaps();
		private ILogger _logger = Plugin.BasePlugin!.Logger;
		private IStringLocalizer _localizer = Plugin.BasePlugin.Localizer;
		#endregion
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

			foreach (var map in maps)
			{
				AddMenuOption(map.VisibleName, SelectedItem);
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

			MapService.NominatedMap(player, _maps.Where(x => x.VisibleName == option.Text).Distinct().First());
		}
		#endregion
	}
}
