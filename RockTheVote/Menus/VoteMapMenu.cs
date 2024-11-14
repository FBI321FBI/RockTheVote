using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Menu;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RockTheVote.Interface;
using RockTheVote.Proxys;
using RockTheVote.ReadModels;

namespace RockTheVote.Menus
{
	public class VoteMapMenu : CenterHtmlMenu, IRtvMenu
	{
		#region Properties
		private IEnumerable<MapReadModel>? _maps = MapServiceProxy.GetMaps();
		private ILogger _logger = Plugin.BasePlugin!.Logger;
		private IStringLocalizer _localizer = Plugin.BasePlugin.Localizer;
		#endregion

		#region .ctor
		public VoteMapMenu(string title, BasePlugin plugin) : base(title, plugin)
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
		}
		#endregion
	}
}
