namespace FuzzySharp.MembershipFunctions.Functions
{
    public class TriangleMembershipFunction : IMembershipFunction
    {
        private float? a;
        private float? b;
        private float? c;

        public float CalculateMemberships(float x)
        {
            if (x < a || x > b) 
            {
                return 0;
            }

            if (x >= a && x <= c) 
            {
                return (x - a.Value) / (c.Value - a.Value);
            }

            return (b!.Value - x) / (b.Value - c!.Value);
        }

        public void SetUpBorder(float a, float b, float? c, float? d)
        {
            if (a > c || c > b) 
            {
                throw new InvalidDataException($"Bad borders setup values should satisfy {a} < {c} < {b}");
            }
            if (c is null)
            {
                throw new InvalidDataException($"Thrid value cannot be null");
            }

            this.a = a;
            this.b = b;
            this.c = c;
        }
    }
}
