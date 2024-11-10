namespace MarsRover.Tests;

public class NorthState: IRoverState
{
    public void TurnLeft(MarsRover marsRover)
    {
        marsRover.State = new WestState();
    }

    public void TurnRight(MarsRover marsRover)
    {
        marsRover.State = new EastState();
    }

    public void MoveForward(MarsRover marsRover)
    {
        marsRover.Y++;
    }

    public Compass GetDirection() => Compass.N;
}