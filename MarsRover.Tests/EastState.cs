namespace MarsRover.Tests;

public class EastState: IRoverState
{
    public void TurnLeft(MarsRover marsRover)
    {
        marsRover.State = new NorthState();
    }

    public void TurnRight(MarsRover marsRover)
    {
        marsRover.State = new SouthState();
    }

    public void MoveForward(MarsRover marsRover)
    {
        marsRover.X++;
    }

    public Compass GetDirection() => Compass.E;
}