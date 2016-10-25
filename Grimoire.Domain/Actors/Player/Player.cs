using Grimoire.UI;

namespace Grimoire.Domain.Actors.Player
{
    public class Player : Actor
    {
        public Player()
        {
            FOV = 15;
            Name = "Player";
            Color = Colors.Player;
            Symbol = '@';
            X = 10;
            Y = 10;
        }
    }
}
