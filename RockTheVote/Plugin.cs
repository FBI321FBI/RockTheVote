using CounterStrikeSharp.API.Core;
using RockTheVote.Commands;
using RockTheVote.EventsHandlers;
using RockTheVote.EventsHandlers.RtvEventsHandlers;
using RockTheVote.Listeners;
using RockTheVote.Proxys;
using static CounterStrikeSharp.API.Core.Listeners;

namespace RockTheVote
{
	public class Plugin : BasePlugin
	{
		#region Properties
		#region Override
		public override string ModuleName => "RockTheVote";

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
			RtvEventSubscribers();
			RegisterListenersHandlers();
		}
		#endregion

		#region Commands
		private void AddCommands()
		{
			AddCommand("css_timeleft", "Отображает время до конца карты.", CSS_timeleft_Command.Handler);
			AddCommand("css_rtv", "Голос за смену карты.", CSS_rtv_Command.Handler);
			AddCommand("css_nominate", "Номинировать карту.", CSS_nominate_Command.Handler);
			AddCommand("css_nextmap", "Ставит следующую карту.", CSS_nextmap_Command.Handler);
			AddCommand("css_maps", "Отображает список карт в консоле.", CSS_maps_Command.Handler);
		}
		#endregion

		#region Events
		private void RegisterEventsHandlers()
		{
			RegisterEventHandler<EventPlayerDisconnect>(PlayerDisconnectEvent.Handler);
			RegisterEventHandler<EventPlayerDeath>(PlayerDeathEvent.Handler);
			RegisterEventHandler<EventRoundEnd>(RoundEndEvent.Handler);
		}
		#endregion

		#region Listeners
		private void RegisterListenersHandlers()
		{
			RegisterListener<OnMapEnd>(OnMapEndListener.Handler);
			RegisterListener<OnTick>(OnTickListener.Handler);
		}
		#endregion

		#region Private
		private void RtvEventSubscribers()
		{
			MapServiceProxy.PlayerVoteRtvEvent += EventPlayerVoteRtv.Handler;
			MapServiceProxy.MapSelectionStartedEvent += EventMapSelectionStarted.Handler;
			MapServiceProxy.MapSelectionEndedEvent += EventMapSelectionEnded.Handler;
			MapServiceProxy.VoteMapPlayerEvent += EventVoteMapPlayer.Handler;
			MapServiceProxy.AssigningNewMapEvent += EventAssigningNewMap.Handler;
		}
		#endregion
	}
}
