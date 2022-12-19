namespace AspServer.Models.game
{
    public class MessageToPlayer
    {
        public int? PlayerId { get; set; }
        public string? Message { get; set; }

        public MessageToPlayer(int? playerId, string? message) 
        {
            this.PlayerId = playerId;
            this.Message = message;
        }
    }
}
