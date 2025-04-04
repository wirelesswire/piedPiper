namespace FuzzySharp.MembershipFunctions
{
    public interface IMembershipFunction
    {
        public abstract float CalculateMemberships(float value);
        
        public abstract void SetUpBorder(float a, float b, float? c, float? d);
    }
}
