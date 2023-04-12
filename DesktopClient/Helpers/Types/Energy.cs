namespace DesktopClient.Helpers.Types
{
    class Energy
    {
        public string RateName { get; }
        public double ActivePositive { get; }
        public double ActiveNegative { get; }
        public double ReactivePositive { get; }
        public double ReactiveNegative { get; }

        public Energy(string rateName) => RateName = rateName;
        public Energy(string rateName, (double, double, double, double) energy) : this(rateName)
        {
            ActivePositive = energy.Item1;
            ActiveNegative = energy.Item2;
            ReactivePositive = energy.Item3;
            ReactiveNegative = energy.Item4;
        }
    }
}
