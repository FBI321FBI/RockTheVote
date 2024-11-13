using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Localization;
using RockTheVote.Enums;
using RockTheVote.Interface;

namespace RockTheVote.Menus
{
	public class MenuMapSelection
	{
		#region Private
		private IStringLocalizer _localizer = Plugin.BasePlugin!.Localizer;
		private BasePlugin _plugin;
		#endregion

		#region .ctor
		public MenuMapSelection(BasePlugin plugin)
		{
			_plugin = plugin;
		}
		#endregion

		#region Public
		public IMenuMapSelection OrderMenu(TypeMenuMapSelection typeMenu)
		{
			switch (typeMenu)
			{
				case 0:
					{
						return new NominatedMenu(_localizer["Nominate.MenuTitle"], _plugin);
					}
				default:
					{
						throw new NullReferenceException();
					}
			}
		}
		#endregion
	}
}
