using AspServer.Models.game;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Cryptography.X509Certificates;

namespace AspServer.Game
{
    //this class should update the variables of villagers and generate information that each villager will get after every action was logged in
    public static class GameBoardSimulator
    {


        //first entry in list stands for first villager in villagerList
        public static List<List<Villager>> getPositions(List<VillagerAction> actionList, List<Villager> villagerList)
        {
            var aliveVillagers = villagerList.FindAll(v => v.Health >= 1);
            var board = new List<List<Villager>>();
            for (int i = 0; i < villagerList.Count; i++)
            {
                board.Add(new List<Villager>());

                //add villagers to house positions ,since they cant move, and if they dont hide
                var villager = villagerList[i];
                var a = actionList.Find(a => a.ExecutorId == villager.Id);
                if (villager.Role == Roles.VILLAGER)
                {

                    if (villager.Health >= 1 && a != null && a.Name != VillagerActionNames.hide)
                    {
                        board[i].Add(villagerList[i]);
                    }
                }
            }

            foreach (var action in actionList)  //find location of other roles
            {
                if (action.PlaceIndex != null)
                {
                    var foundVillager = villagerList.Find(v => v.Id == action.ExecutorId);
                    if (foundVillager != null) { board[action.PlaceIndex.GetValueOrDefault(-1)].Add(foundVillager); }
                }
            }

            return board;

        }

        //update villager variables and 
        public static List<MessageToPlayer> calculateOutcome(List<VillagerAction> actionList, List<Villager> villagerList)
        {
            var aliveVillagers = villagerList.FindAll(v => v.Health >= 1);
            var messages = new List<MessageToPlayer>();

            var board = getPositions(actionList, villagerList);
            for (int i = 0; i < board.Count; i++)
            {
                //get each roles
                var boardField = board[i];
                var villagers = boardField.FindAll(v => v.Role == Roles.VILLAGER);
                var hunters = boardField.FindAll(v => v.Role == Roles.HUNTER);
                var spies = boardField.FindAll(v => v.Role == Roles.SPY);

                if(spies.Count >= 1 && hunters.Count >= 1)
                {              
                    if(hunters.Any(v => v.CurrentAction.Name == VillagerActionNames.snare)){
                        foreach (var spy in spies)
                        {
                            if (spy.CurrentAction.Name == VillagerActionNames.spy)
                            {
                                spy.Health -= 1;
                            }
                            messages.Add(new MessageToPlayer(new int?(), ""));
                        }
                    }
                    else if (hunters.Any(v => v.CurrentAction.Name == VillagerActionNames.shoot))
                    {
                        
                    }
                }
            }

            return messages;
        }
    }
}
