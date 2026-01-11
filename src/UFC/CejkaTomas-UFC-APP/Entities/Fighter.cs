namespace CejkaTomas_UFC_APP.Entities
{
    public  class Fighter
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Nickname { get; set; }
        public string? WeightClass { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        public virtual ICollection<Fights> FightFighterReds { get; set; } = new List<Fights>();
        public virtual ICollection<Fights> FightFighterBlues { get; set; } = new List<Fights>();
    }
}
