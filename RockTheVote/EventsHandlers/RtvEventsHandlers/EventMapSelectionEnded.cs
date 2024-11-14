using RockTheVote.Proxys;
using RockTheVote.ReadModels;
using RockTheVote.Services;

namespace RockTheVote.EventsHandlers.RtvEventsHandlers
{
	public static class EventMapSelectionEnded
	{
		public static void Handler(MapReadModel map)
		{
			MapService.IsMapSelectionStarted = false;

			var nextMap = MapService.VotesMap
				.GroupBy(x => x.Value)
				.OrderByDescending(x => x.Count())
				.FirstOrDefault()?.Key;
			var maps = MapService.GetMaps();
			if (maps != null)
			{
				MapServiceProxy.SetNextMap(nextMap ?? maps.ElementAt(new Random().Next(0, maps.Count())));
			}
		}
	}
}
