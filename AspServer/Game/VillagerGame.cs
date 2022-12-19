using AspServer.Models.game;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.Threading.Tasks.Dataflow;

namespace AspServer.Game
{
    public class VillagerGame
    {
        public int Id { get; set; }
        public List<Villager> villagers;
        public int State { get; private set; }
        private List<VillagerAction> actions;

        public VillagerGame(int id)
        {
            Id = id;
            State = States.WAITING;
            villagers = new List<Villager>();
            actions = new List<VillagerAction>();
        }
        public void Add(Villager villager)
        {
            if (State != States.WAITING)
            {
                return;
            }
            else if (villagers.Contains(villager) == false)
            {
                villagers.Add(villager);
            }
        }
        //starts the games and blocks others to join
        public bool Start()
        {
            State = States.NIGHT;
            return false;
        }
        //tries to enter the next state, (only if all actions are logged)
        public bool Next()
        {
            if (State == States.NIGHT)
            {
                State = States.NIGHT;
                return true;
            }
            else if (State == States.DAY)
            {
                State = States.NIGHT;
                return true;
            }
            else if (State == States.WAITING)
            {
                State = States.NIGHT;
                return true;
            }
            return false;
        }

        public void AddAction(VillagerAction villagerAction)
        {
            actions.Add(villagerAction);
            if (CheckIfReady())
            {
                ExecuteActions();
            }
        }

        private void ExecuteActions()
        {
            foreach (var action in actions)
            {

            }
        }

        //checks weather all alive players have posted their action choice
        private bool CheckIfReady()
        {
            var alivePlayers = villagers.FindAll(v => v.Health >= 1);
            if (alivePlayers.All(v => actions.Any(a => a.ExecutorId == v.Id)))
            {
                return true;
            }
            return false;
        }
    }
}