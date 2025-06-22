namespace minijam.src.Manager
{
    public static class GameStateManager
    {
        // ---------PERSISTENT---------------
        public static int sanity = 100;
        public static int hunger = 50;
        public static int suspicion = 0;
        public static int victims = 0;
        public static int night = 1;

        // ---------RESET EVERY NIGHT---------------
        public static float nightTimer = 0f;
        public static int nightVictims = 0;

        public static void Reset()
        {
            sanity = 100;
            hunger = 50;
            suspicion = 0;
            victims = 0;
            night = 1;
        }
    }
}