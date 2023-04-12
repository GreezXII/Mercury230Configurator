namespace DesktopClient.Helpers.Types
{
    class EnergyPerPhase
    {
        public string RateName { get; }
        public double Phase1 { get; }
        public double Phase2 { get; }
        public double Phase3 { get; }

        public EnergyPerPhase(string rateName) => RateName = rateName;
        public EnergyPerPhase(string rateName, (double, double, double) energy) : this(rateName)
        {
            Phase1 = energy.Item1;
            Phase2 = energy.Item2;
            Phase3 = energy.Item3;
        }
    }
}
