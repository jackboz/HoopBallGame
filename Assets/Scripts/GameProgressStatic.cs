namespace HoopBall
{
    public enum GameRegime
    {
        SingleNormal,
        SingleHard,
        Hotseat,
        Twohanded
    }
    public static class GameProgressStatic
    {
        public static GameRegime GameRegime = GameRegime.SingleNormal;
        public static bool Is1Pwin = true;
        public static int Strike = 0;
        public static int BestStrike = 0;
        public static bool ContinueGame = false;
    }
}
