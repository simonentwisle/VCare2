namespace VCare2.ServiceLayer
{
    public interface IStateService
    {
        IEnumerable<State> List();
    }

    public class StateService : IStateService
    {
        public IEnumerable<State> List()
        {
            return new List<State>
        {
            new State { Abbreviation = "AK", Name = "Alaska" },
            new State { Abbreviation = "AL", Name = "Alabama" }
        };
        }
    }

    public class State
    {
        public string Abbreviation { get; set; }    
        public string Name { get; set; }            
    }
}
