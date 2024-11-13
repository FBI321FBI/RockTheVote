using RockTheVote.ReadModels;

namespace RockTheVote.Interface
{
	public interface IMenuMapSelection
	{
		void CreateMenuOptions(IEnumerable<MapReadModel>? maps);
	}
}
