namespace AspServer.Models.game
{
    public class Villager
    {
        [ID]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Role { get; set; }
        public int? Health { get; set; }
        public VillagerAction? CurrentAction { get; set; }
        public void damage() { if (this.Health != 0) { this.Health -= 1; } }
    }

    public class Roles
    {
        public const string VILLAGER= "villager";
        public const string HUNTER = "hunter";
        public const string SPY = "spy";

        public static List<string> getRolesList()//todo
        {
            var list = new List<string>();
            var obj = new Roles();
            
            var props = obj.GetType().GetProperties().ToList();
            foreach ( var prop in props )
            {
                list.Add("");
            }
            return list;
        }
    }

    
}
