using CounterStrikeSharp.API.Modules.Commands;

namespace RockTheVote.Extensions
{
	public static class CommandInfoExtension
	{
		public static string GetReasonOrMessage(this CommandInfo info, int reasonOrMessageStartArgIndex, int argsBeforeReasonOrMessage = 0)
		{
			string reasonOrMessage = string.Empty;

			for (int i = reasonOrMessageStartArgIndex; i < info.ArgCount - argsBeforeReasonOrMessage; i++)
			{
				reasonOrMessage += $"{info.ArgByIndex(i)} ";
			}
			return reasonOrMessage;
        }
	}
}
