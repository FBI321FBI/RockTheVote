namespace RockTheVote.Extensions
{
	public static class ULongExtensions
	{
		public static bool IsSteamId(this ulong steamId)
		{
			return steamId >= 70000000000000000 && steamId <= 79999999999999999;
		}
	}
}
