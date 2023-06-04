public static class Turtle
{
    private static readonly string LUA_PREFIX = "turtle.";

    public static string Up() => LUA_PREFIX + "up()";

    public static string Down() => LUA_PREFIX + "down()";

    public static string Forward() => LUA_PREFIX + "forward()";

    public static string Back() => LUA_PREFIX + "back()";

    public static string TurnLeft() => LUA_PREFIX + "turnLeft()";

    public static string TurnRight() => LUA_PREFIX + "turnRight()";

    public static string Dig() => LUA_PREFIX + "dig()";

    public static string DigUp() => LUA_PREFIX + "digUp()";

    public static string DigDown() => LUA_PREFIX + "digDown()";
}