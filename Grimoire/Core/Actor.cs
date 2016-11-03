using RLNET;
using RogueSharp;
using Grimoire.UI;
using Grimoire.Interfaces;

namespace Grimoire.Core
{
    public class Actor : IAmAnActor, IAmDrawable
    {
        public string Name { get; set; }
        public int FieldOfView { get; set; }
        private int _attack;
        public int Attack
        {
            get { return _attack; }
            set { _attack = value; }
        }

        private int _attackChance;
        public int AttackChance
        {
            get { return _attackChance; }
            set { _attackChance = value; }
        }

        private int _defenceChance;
        public int DefenceChance
        {
            get { return _defenceChance; }
            set { _defenceChance = value; }
        }

        private int _defence;
        public int Defence
        {
            get { return _defence; }
            set { _defence = value; }
        }

        private int _money;
        public int Money
        {
            get { return _money; }
            set { _money = value; }
        }

        private int _health;
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        private int _healthMax;
        public int HealthMax
        {
            get { return _healthMax; }
            set { _healthMax = value; }
        }

        private int _speed;
        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }



        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }
            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else
            {
                console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
            }
        }
    }
}
