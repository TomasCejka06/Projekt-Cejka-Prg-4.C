namespace CejkaTomas_UFC_APP.Entities
{
    public class Fights
    {
        public int Id { get; set; }
        public int FighterRedId { get; set; }
        public int FighterBlueId { get; set; }
        public int? WinnerId { get; set; }
        public string EventName { get; set; } = null!;
        public DateTime FightDate { get; set; }

        public virtual Fighter FighterRed { get; set; } = null!;
        public virtual Fighter FighterBlue { get; set; } = null!;
        public virtual Fighter? Winner { get; set; }
    }
}
