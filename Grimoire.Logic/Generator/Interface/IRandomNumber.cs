namespace Grimoire.Logic.Generator.Interface
{
    public interface IRandomNumber
    {
        int Next(int maxValue);

        int Next(int minValue, int maxValue);

        RandomState Save();

        void Restore(RandomState state);
    }

    public class RandomState
    {
        public int[] Seed { get; set; }

        public long NumberGenerated { get; set; }
    }
}
