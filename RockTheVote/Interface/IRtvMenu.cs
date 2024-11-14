using RockTheVote.ReadModels;

namespace RockTheVote.Interface
{
	public interface IRtvMenu
	{
		void CreateMenuOptions(IEnumerable<MapReadModel>? maps);
	}
}
