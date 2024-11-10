namespace MarsRover.Tests;

public interface IRoverState
{
    void TurnLeft(MarsRover marsRover);
    void TurnRight(MarsRover marsRover);
    void MoveForward(MarsRover marsRover);
    Compass GetDirection();
}