namespace MarsRover.Tests;

public class WestState: IRoverState
{
    public void TurnLeft(MarsRover marsRover)
    {
        marsRover.State = new SouthState();
    }

    public void TurnRight(MarsRover marsRover)
    {
        marsRover.State = new NorthState();
    }

    public void MoveForward(MarsRover marsRover)
    {
        marsRover.X--;
    }

    public Compass GetDirection() => Compass.W;
}