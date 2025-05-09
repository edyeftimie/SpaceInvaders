public class Direction {
    public static int directionCoefficient (Character character) {
        if (character is Player) {
            return -1;
        } else {
            return 1;
        }
    }
}