using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;
using Microsoft.Extensions.Localization;

namespace RockTheVote.Extensions
{
	public static class ServerExtension
	{
		#region Properties
		private static IStringLocalizer _localizer = Plugin.BasePlugin!.Localizer;
		#endregion

		/// <summary>
		/// Получить оставшееся время карты.
		/// </summary>
		/// <param name="server"></param>
		/// <returns>Возвращает оставшееся время карты в секундах. Если mp_timelimit равен 0, то возвращает null.
		/// Если время стало меньше 1 секунды до конца карты, то возращает 0.</returns>
		public static double? GetRemainingMapTime(this Server server)
		{
			var cheatsCvar = ConVar.Find("mp_timelimit");
			var gameStartTime = new Server().GetGameRules()?.GameStartTime;
			if (cheatsCvar?.GetPrimitiveValue<float>() == 0 || cheatsCvar?.GetPrimitiveValue<float>() == null)
			{
				return null;
			}

			var lastTime = (cheatsCvar!.GetPrimitiveValue<float>() * 60) - (Server.CurrentTime - gameStartTime);
			if(lastTime < 1)
			{
				return 0;
			}
			else
			{
				return lastTime;
			}
		}

		public static CCSGameRules? GetGameRules(this Server server)
		{
			return Utilities.FindAllEntitiesByDesignerName<CCSGameRulesProxy>("cs_gamerules").FirstOrDefault()?
				.GameRules;
		}

		/// <summary>
		/// Получить общее кол-во раундов.
		/// </summary>
		/// <param name="server"></param>
		/// <returns>Возвращает общее кол-во раундов.</returns>
		public static int? GetTotalRoundsPlayed(this Server server)
		{
			return Utilities.FindAllEntitiesByDesignerName<CCSGameRulesProxy>("cs_gamerules").FirstOrDefault()?
				.GameRules?.TotalRoundsPlayed + 1;
		}

		/// <summary>
		/// Отправляет сообщение всем игрокам от имени отправителя. Если отправитель null, то отправляет от имени сервера.
		/// Переменная wrap - это обёртка, если wrap = [@], то сообщение будет начинаться [ServerName].
		/// </summary>
		/// <param name="server">Сервер.</param>
		/// <param name="sender">Отправитель.</param>
		public static void PrintToChatAllSafe(this Server server, CCSPlayerController? sender, string serverName, string wrap = "[@]", params string[] messages)
		{
			var serverNameWrap = wrap.Replace("@", serverName);

			if (sender.IsPlayerValid())
			{
				foreach (var message in messages)
				{
					Server.PrintToChatAll($"{serverNameWrap} {sender!.PlayerName} {message}");
				}
			}
			else
			{
				foreach (var message in messages)
				{
					Server.PrintToChatAll($"{serverNameWrap} Server {message}");
				}
			}
		}

		/// <summary>
		/// Отправляет сообщение всем игрокам от имени отправителя. Если отправитель null, то отправляет от имени сервера.
		/// Переменная wrap - это обёртка, если wrap = [@], то сообщение будет начинаться [ServerName].
		/// </summary>
		/// <param name="server">Сервер.</param>
		/// <param name="sender">Отправитель.</param>
		public static void PrintToChatAllSafe(this Server server, params string[] messages)
		{
			foreach (var message in messages)
			{
				Server.PrintToChatAll($"{_localizer["Prefix"]} {message}");
			}
		}
	}
}
