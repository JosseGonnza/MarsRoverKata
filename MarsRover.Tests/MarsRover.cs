namespace MarsRover.Tests;

public class MarsRover
{
    public int X { get; set; }
    public int Y { get; set; }
    public IRoverState State { get; set; }

    public MarsRover(int x, int y, IRoverState initialState)
    {
        this.X = x;
        this.Y = y;
        State = initialState;
    }

    public string Execute(string command)
    {
        foreach (var c in command.ToCharArray())
        {
            if (c == 'M') 
            {
                this.State.MoveForward(this);
                continue;
            }

            if (c == 'L')
            {
                this.State.TurnLeft(this);
                continue;
            }
            if (c == 'R')
            {
                this.State.TurnRight(this);
                continue;
            }
        }

        return $"{this.X}:{this.Y}:{this.State.GetDirection()}";
    }
}