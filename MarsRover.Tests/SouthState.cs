namespace MarsRover.Tests;

public class SouthState: IRoverState
{
    public void TurnLeft(MarsRover marsRover)
    {
        marsRover.State = new EastState();
    }

    public void TurnRight(MarsRover marsRover)
    {
        marsRover.State = new WestState();
    }

    public void MoveForward(MarsRover marsRover)
    {
        marsRover.Y--;
    }

    public Compass GetDirection() => Compass.S;
}