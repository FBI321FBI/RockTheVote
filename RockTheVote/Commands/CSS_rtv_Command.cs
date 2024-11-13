using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;

namespace RockTheVote.Commands
{
	public static class CSS_rtv_Command
	{
		#region Properties
		private static BasePlugin _plugin = Plugin.BasePlugin!;
		#endregion

		#region Handler
		[CommandHelper(whoCanExecute: CommandUsage.CLIENT_ONLY)]
		public static void Handler(CCSPlayerController? player, CommandInfo info)
		{

		}
		#endregion
	}
}
