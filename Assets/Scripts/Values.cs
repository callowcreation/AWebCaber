public static class Values
{
    public const int PLAYER_LAYER = 8;
    public const int BALLOON_LAYER = 9;
    public const int GROUND_LAYER = 10;

    public enum Surface
    {
        Ground,
        Body,
        Head,
        Balloon
    }

    public enum BalloonTypes
    {
        Normal = 0,
        Special = 1
    }
}