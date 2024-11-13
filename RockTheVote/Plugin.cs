using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using RockTheVote.Commands;
using RockTheVote.EventsHandlers;
using RockTheVote.Extensions;
using RockTheVote.Services;

namespace RockTheVote
{
	public class Plugin : BasePlugin
	{
		#region Properties
		#region Override
		public override string ModuleName => "RTV";

		public override string ModuleVersion => "0.1";
		#endregion

		public static BasePlugin? BasePlugin;
		#endregion

		#region .ctor
		public Plugin()
		{
			BasePlugin = this;
		}
		#endregion

		#region Override
		public override void Load(bool hotReload)
		{
			AddCommands();
			RegisterEventsHandlers();

			EventSubscribers();
		}
		#endregion

		#region Commands
		private void AddCommands()
		{
			AddCommand("css_timeleft", "Отображает время до конца карты.", CSS_timeleft_Command.Handler);
			AddCommand("css_rtv", "Голос за смену карты.", CSS_rtv_Command.Handler);
			AddCommand("css_nominate", "Номинировать карту.", CSS_nominate_Command.Handler);
		}

		private void RegisterEventsHandlers()
		{
			RegisterEventHandler<EventPlayerDisconnect>(PlayerDisconnectEvent.Handler);
			RegisterEventHandler<EventPlayerDeath>(PlayerDeathEvent.Handler);
		}
		#endregion

		#region Private
		private void EventSubscribers()
		{
			MapService.AssigningNewMapEvent += (map) => new Server().PrintToChatAllSafe(Localizer["NextMapMessage", map.VisibleName!]);
		}
		#endregion
	}
}
