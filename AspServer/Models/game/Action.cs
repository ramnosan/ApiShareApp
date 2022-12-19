namespace AspServer.Models.game
{
    public class VillagerAction
    {
        public string Name { get; set; }
        public int ExecutorId{ get; set; }
        public int? PlaceIndex { get; set; }
    }

    public static class VillagerActionNames
    {
        public const string hide = "hide";
        public const string sleep = "sleep";
        public const string snare = "snare";
        public const string shoot = "shoot";
        public const string spy = "spy";
    }
}
