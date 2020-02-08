namespace SwordAndBored.Utilities.Random
{
    class Odds
    {
        protected static Odds Instance { get; } = new Odds();
        private System.Random Random { get; }

        private Odds()
        {
            Random = new System.Random();
        }

        /// <summary>
        /// Returns a value between 1 and <code>x</code>
        /// </summary>
        /// <param name="x">The upper value of the dice roll</param>
        /// <returns></returns>
        public static int DiceRoll(int x = 100)
        {
            return Instance.Random.Next(x) + 1;
        }

        public static float Percent()
        {
            return (float)PercentDouble();
        }

        public static double PercentDouble()
        {
            return Instance.Random.NextDouble();
        }

        public static bool CoinToss()
        {
            return DiceRoll(2) == 1;
        }
    }
}
