using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace RockTheVote.Extensions
{
	public static class CCSPlyaerControllerExtension
	{
		#region Properties
		private static ILogger _logger = Plugin.BasePlugin!.Logger;
		private static IStringLocalizer _localizer = Plugin.BasePlugin.Localizer;
		#endregion

		/// <summary>
		/// Отправляет сообщение типа alert игроку от имени сервера, если игрок равен null, то отправляет сообщение в консоле.
		/// Переменная wrap - это обёртка, если wrap = [@], то сообщение будет начинаться [ServerName].
		/// </summary>
		/// <param name="player"></param>
		/// <param name="logger"></param>
		/// <param name="serverName"></param>
		/// <param name="wrap"></param>
		/// <param name="messages"></param>
		public static void PrintToCenterAlertSafe(this CCSPlayerController? player, ILogger logger, IStringLocalizer localizer, params string[] messages)
		{
			if (player.IsPlayerValid())
			{
				foreach (var message in messages)
				{
					player!.PrintToCenterAlert($"{localizer["Prefix"]} {message}");
				}
			}
			else
			{
				foreach (var message in messages)
				{
					logger.LogError($"{localizer["Prefix"]} {message}");
				}
			}
		}

		/// <summary>
		/// Отправляет сообщение игроку от имени сервера, если игрок равен null, то отправляет сообщение в консоле.
		/// Переменная wrap - это обёртка, если wrap = [@], то сообщение будет начинаться [ServerName].
		/// </summary>
		/// <param name="player"></param>
		/// <param name="logger"></param>
		/// <param name="serverName"></param>
		/// <param name="wrap"></param>
		/// <param name="messages"></param>
		public static void PrintToConsoleSafe(this CCSPlayerController? player, ILogger logger, IStringLocalizer localizer, params string[] messages)
		{
			if (player.IsPlayerValid())
			{
				foreach (var message in messages)
				{
					player!.PrintToConsole($"{localizer["Prefix"]} {message}");
				}
			}
			else
			{
				foreach (var message in messages)
				{
					logger.LogInformation($"{message}");
				}
			}
		}

		/// <summary>
		/// Отправляет сообщение игроку от имени сервера, если игрок равен null, то отправляет сообщение в консоле.
		/// Переменная wrap - это обёртка, если wrap = [@], то сообщение будет начинаться [ServerName].
		/// </summary>
		/// <param name="player">Игрок.</param>
		/// <param name="logger">Логгер.</param>
		/// <param name="wrap">Обёртка</param>
		/// <param name="messages">Сообщения.</param>
		public static void PrintToChatSafe(this CCSPlayerController? player, ILogger logger, IStringLocalizer localizer, params string[] messages)
		{
			if (player.IsPlayerValid())
			{
				foreach (var message in messages)
				{
					player!.PrintToChat($"{localizer["Prefix"]} {message}");
				}
			}
			else
			{
				foreach (var message in messages)
				{
					logger.LogInformation($"{message}");
				}
			}
		}

		/// <summary>
		/// Отправляет сообщение игроку от имени сервера, если игрок равен null, то отправляет сообщение в консоле.
		/// Переменная wrap - это обёртка, если wrap = [@], то сообщение будет начинаться [ServerName].
		/// </summary>
		/// <param name="player">Игрок.</param>
		/// <param name="logger">Логгер.</param>
		/// <param name="wrap">Обёртка</param>
		/// <param name="messages">Сообщения.</param>
		public static void PrintToChatSafe(this CCSPlayerController? player, params string[] messages)
		{
			if (player.IsPlayerValid())
			{
				foreach (var message in messages)
				{
					player!.PrintToChat($"{_localizer["Prefix"]} {message}");
				}
			}
			else
			{
				foreach (var message in messages)
				{
					_logger.LogInformation($"{message}");
				}
			}
		}

		/// <summary>
		/// Проверяет игрока, есть ли он на сервере.
		/// </summary>
		/// <returns>true, если игрок на сервере и false, если игрока нет на сервере.</returns>
		public static bool IsOnTheServer(this CCSPlayerController player)
		{
			CCSPlayerController? _player = Utilities.GetPlayers().Where(x => x == player).SingleOrDefault();

			if (_player == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// Проверяет игрока на валидность, в том числе и на null.
		/// </summary>
		/// <returns>true, если игрок валиден и false, если игрок не валиден.</returns>
		public static bool IsPlayerValid(this CCSPlayerController? player)
		{
			if (player is null || !player.IsValid || player.IsBot || player.IsHLTV) return false;
			else return true;
		}

		/// <summary>
		/// Проверка, живой ли игрок или нет.
		/// </summary>
		/// <param name="player">Игрок.</param>
		/// <returns>true, если живой и false, если не живой.</returns>
		public static bool IsPlayerLive(this CCSPlayerController? player)
		{
			if (!player.IsPlayerValid())
			{
				return false;
			}

			if (player!.PlayerPawn.Value.LifeState == (byte)LifeState_t.LIFE_ALIVE)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Получение имени игрока.
		/// </summary>
		/// <returns>Если игрок равен null, то вернёт "SERVER".</returns>
		public static string GetName(this CCSPlayerController? player)
		{
			if (player.IsPlayerValid())
			{
				return player!.PlayerName;
			}
			else
			{
				return "Server";
			}
		}
	}
}
