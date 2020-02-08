namespace SwordAndBored.Utilities.Random
{
    /// <summary>
    /// A class for returning random odds of events happening
    /// </summary>
    class Odds
    {
        /// <summary>
        /// This hidden instance of the class only needs to be accessible by this
        /// class to get the <code>System.Random</code> class that it holds
        /// </summary>
        private static Odds Instance { get; } = new Odds();
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

        /// <summary>
        /// Returns a random float value between 0 and 1
        /// </summary>
        /// <returns>Random value between [0, 1)</returns>
        public static float Percent()
        {
            return (float)PercentDouble();
        }

        /// <summary>
        /// Returns a random double value between 0 and 1
        /// </summary>
        /// <returns>Random value between [0, 1) as a double</returns>
        public static double PercentDouble()
        {
            return Instance.Random.NextDouble();
        }

        /// <summary>
        /// Returns an equal probability of either true or false for heads or tails
        /// </summary>
        /// <returns>True for heads, False for tails</returns>
        public static bool CoinToss()
        {
            return DiceRoll(2) == 1;
        }
    }
}
