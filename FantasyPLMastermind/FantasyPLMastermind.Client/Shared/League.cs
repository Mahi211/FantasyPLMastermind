using static FantasyPLMastermind.Client.Shared.ManagerInfo;

namespace FantasyPLMastermind.Client.Shared
{
    public class League
    {
        public Classic[] classic { get; set; }
        public object[] h2h { get; set; }
        public Cup cup { get; set; }
        public Cup_Matches[] cup_matches { get; set; }
    }
}
