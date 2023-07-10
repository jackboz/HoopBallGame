namespace HoopBall
{
    public enum GameRegime
    {
        SingleNormal,
        SingleHard,
        Hotseat,
        Twohands
    }
    public static class GameProgressStatic
    {
        public static GameRegime GameRegime = GameRegime.SingleNormal;
        public static bool Is1Pwin = true;
        public static int Strike = 0;
        public static int StrikeBest = 0;
        public static bool ContinueGame = false;
        public static float TwoHandTime = 0;
        public static float TwoHandTimeBest = 10000000f;
    }
}
