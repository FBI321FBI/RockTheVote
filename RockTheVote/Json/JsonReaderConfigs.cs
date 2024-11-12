using CounterStrikeSharp.API.Core;
using System.Runtime.InteropServices;

namespace RockTheVote
{
	public class JsonReaderConfigs
	{
		#region Properties
		private BasePlugin _plugin;
		#endregion

		#region .ctor
		public JsonReaderConfigs(BasePlugin plugin)
		{
			_plugin = plugin;
		}
		#endregion

		#region Public
		/// <summary>
		/// File path in: ModuleDirectory + ..\..\configs\{defaultConfigFullFileName}
		/// </summary>
		/// <param name="defaultConfigFullFileName"></param>
		/// <returns></returns>
		public string GetFullPathDefaultConfig(string defaultConfigFullFileName)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				return Path.GetFullPath(Path.Combine(_plugin.ModuleDirectory,
					@$"..\..\configs\{defaultConfigFullFileName}"));
			}
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				return Path.GetFullPath(Path.Combine(_plugin.ModuleDirectory,
					@$"../../configs/{defaultConfigFullFileName}"));
			}
			else
			{
				return Path.GetFullPath(Path.Combine(_plugin.ModuleDirectory,
					@$"..\..\configs\{defaultConfigFullFileName}"));
			}
		}

		/// <summary>
		/// File path in: ..\..\configs\plugins\{namePlugin}\{jsonFullFileName}
		/// </summary>
		/// <param name="jsonFullFileName"></param>
		/// <returns></returns>
		public string GetFullPathJsonFile(string jsonFullFileName, string namePlugin)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				return Path.GetFullPath(Path.Combine(_plugin.ModuleDirectory,
					@$"..\..\configs\plugins\{namePlugin}\{jsonFullFileName}"));
			}
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				return Path.GetFullPath(Path.Combine(_plugin.ModuleDirectory,
					@$"../../configs/plugins/{namePlugin}/{jsonFullFileName}"));
			}
			else
			{
				return Path.GetFullPath(Path.Combine(_plugin.ModuleDirectory,
					@$"..\..\configs\plugins\{namePlugin}\{jsonFullFileName}"));
			}
		}
		#endregion
	}
}
