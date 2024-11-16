namespace RockTheVote.Attributes
{
	[AttributeUsage(AttributeTargets.All)]
	public class ResetRtvElementAttribute : Attribute
	{
        public string[] Value { get; }
        public ResetRtvElementAttribute(params string[] value)
        {
			Value = value;
		}
    }
}
