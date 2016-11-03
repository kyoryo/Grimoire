namespace Grimoire.Interfaces
{
    internal interface IAmAnActor
    {
        string Name { get; set; }
        int FieldOfView { get; set; } //range
        int Attack { get; set; }
        int AttackChance { get; set; } //acc
        int Defence { get; set; }
        int DefenceChance { get; set; } //dodge
        int Health { get; set; }
        int HealthMax { get; set; }
        int Speed { get; set; }
        int Money { get; set; }
    }
}
