using AspServer.Models.game;

namespace AspServer.Game
{
    public static class MessageManager {
    
        public static List<MessageToPlayer> OnKill(List<Villager> killed, string actionName) 
        {
            var msgs = new List<MessageToPlayer>() { };

            foreach (var k in killed)
            {
                msgs.Add(new MessageToPlayer(new int?(), $"{k.Name} was killed by {actionName}"));
            }
            return msgs;
        }
        public static List<MessageToPlayer> OnSpied(List<Villager> seenVillager, string action)
        {
            var msgs = new List<MessageToPlayer>();
            foreach (var seenOne in seenVillager)
            {
                msgs.Add(new MessageToPlayer(new int?(), $"{seenOne.Name} was seen inside"));
            }
            return msgs;
        }
    }
}
