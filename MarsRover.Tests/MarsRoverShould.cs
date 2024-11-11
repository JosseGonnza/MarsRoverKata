using System.Runtime.CompilerServices;
using FluentAssertions;

namespace MarsRover.Tests;
/*
 * 1- Initial position 0:0:N
 * 🎯     "M" -> 0:1:N    forward
 * 🎯     "M" -> -1:0:W    forward
 * 🎯     "M" -> 0:-1:S    forward
 * 🎯     "M" -> 1:0:E    forward
 *
 * 🎯     "L" -> 0:0:W    turn left
 * 🎯    "R" -> 0:0:E    turn right
 *
 *      "RM" -> 1:0:E     turn right and forward
 *      "LM" -> -1:0:W    turn left and forward
 *      "LLM" -> 0:-1:S   turn twice left and forward
 *      "RRM" -> 0:-1:S   turn twice right and forward
 *
 * 2- Obstacle
 *      new Grid(10,10,[new Obstacle(1,2)])
 *      "RMLMM" -> O:1:1:N
 */
public class MarsRoverShould
{
    [Theory]
    [InlineData(Compass.N,"0:1:N")]
    [InlineData(Compass.W,"-1:0:W")]
    [InlineData(Compass.S,"0:-1:S")]
    [InlineData(Compass.E,"1:0:E")]
    public void move_forward(Compass compass, string expected)
    {
        var marsRover = new MarsRover(0, 0, compass);
        
        var result = marsRover.Execute("M");
        
        result.Should().Be(expected);
    }

    [Theory(DisplayName = "change orientation when initial is north")]
    [InlineData("L", "0:0:W")]
    [InlineData("R", "0:0:E")]
    public void change_orientation_when_initial_is_north(string command, string expected)
    {
        var marsRover = new MarsRover(0,0, Compass.N);

        var result = marsRover.Execute(command);

        result.Should().Be(expected);
    }
    
    [Theory(DisplayName = "change orientation when initial is west")]
    [InlineData("L", "0:0:S")]
    [InlineData("R", "0:0:N")]
    public void change_orientation_when_initial_is_west(string command, string expected)
    {
        var marsRover = new MarsRover(0,0, Compass.W);

        var result = marsRover.Execute(command);

        result.Should().Be(expected);
    }
    
    [Theory(DisplayName = "change orientation when initial is east")]
    [InlineData("L", "0:0:N")]
    [InlineData("R", "0:0:S")]
    public void change_orientation_when_initial_is_east(string command, string expected)
    {
        var marsRover = new MarsRover(0,0, Compass.E);

        var result = marsRover.Execute(command);

        result.Should().Be(expected);
    }
    
    [Theory(DisplayName = "change orientation when initial is south")]
    [InlineData("L", "0:0:E")]
    [InlineData("R", "0:0:W")]
    public void change_orientation_when_initial_is_south(string command, string expected)
    {
        var marsRover = new MarsRover(0,0, Compass.S);

        var result = marsRover.Execute(command);

        result.Should().Be(expected);
    }

    [Theory(DisplayName = "process many commands when initial orientation is north")]
    [InlineData("RM", "1:0:E")]
    [InlineData("LM", "-1:0:W")]
    public void process_many_commands_when_initial_orientation_is_north(string command, string expected)
    {
        var marsRover = new MarsRover(0,0, Compass.N);

        var result = marsRover.Execute(command);

        result.Should().Be(expected);
    }
    
    [Theory(DisplayName = "process many commands when initial orientation is west")]
    [InlineData("RM", "0:-1:S")]
    [InlineData("LM", "0:1:N")]
    public void process_many_commands_when_initial_orientation_is_west(string command, string expected)
    {
        var marsRover = new MarsRover(0,0, Compass.E);

        var result = marsRover.Execute(command);

        result.Should().Be(expected);
    }

    [Fact(DisplayName = "process many commands when initial orientation is north and find an obstacle")]
    public void process_many_commands_when_initial_orientation_is_north_and_find_an_obstacle()
    {
        var marsRover = new MarsRover(0, 0, Compass.N, new Grid(10, 10, new List<Obstacle>(){new Obstacle(1, 2)}));

        var result = marsRover.Execute("RMLMM");

        result.Should().Be("O:1:1:N");
    }
}

public class MarsRover
{
    private int X { get; set; }
    private int Y { get; set; }
    private Compass Compass { get; set; }
    public Grid Grid { get; }

    public MarsRover(int x, int y, Compass compass) // TODO: Solo un constructor, siempre se crea un grid (con o sin Obstacles)
    {
        this.X = x;
        this.Y = y;
        this.Compass = compass;
        this.Grid = new Grid(10, 10);
    }
    
    public MarsRover(int x, int y, Compass compass, Grid grid)
    {
        this.X = x;
        this.Y = y;
        this.Compass = compass;
        this.Grid = grid;
    }

    public string Execute(string command)
    {
        var obstacle = "";
        foreach (var c in command.ToCharArray())
        {
            if (c == 'M') 
            {
                if (this.Grid.ContainsObstacles() && this.Compass == Compass.N && this.Grid.GoNextInY(this.X, this.Y + 1))
                {
                    obstacle = "O:";
                    continue;
                }
                
                switch (this.Compass)
                {
                    case Compass.N:
                        this.Y++;
                        break;
                    case Compass.S:
                        this.Y--;
                        break;
                    case Compass.W:
                        this.X--;
                        break;
                    case Compass.E:
                        this.X++;
                        break;
                }
                continue;
            }

            if (c == 'L' && this.Compass == Compass.N)
            {
                this.Compass = Compass.W;
                continue;
            }
            if (c == 'R' && this.Compass == Compass.N)
            {
                this.Compass = Compass.E;
                continue;
            }
            
            if (c == 'L' && this.Compass == Compass.W)
            {
                this.Compass = Compass.S;
                continue;
            }
            if (c == 'R' && this.Compass == Compass.W)
            {
                this.Compass = Compass.N;
                continue;
            }
            
            if (c == 'L' && this.Compass == Compass.E)
            {
                this.Compass = Compass.N;
                continue;
            }
            if (c == 'R' && this.Compass == Compass.E)
            {
                this.Compass = Compass.S;
                continue;
            }
            
            if (c == 'L' && this.Compass == Compass.S)
            {
                this.Compass = Compass.E;
                continue;
            }
            if (c == 'R' && this.Compass == Compass.S)
            {
                this.Compass = Compass.W;
                continue;
            }
        }

        return $"{obstacle}{this.X}:{this.Y}:{this.Compass}";
    }
}

public class Grid
{
    public int Row { get; }
    public int Column { get; }
    public List<Obstacle> Obstacles { get; }

    public Grid(int row, int column)
    {
        this.Row = row;
        this.Column = column;
        this.Obstacles = new List<Obstacle>();
    }
    
    public Grid(int row, int column, List<Obstacle> obstacles)
    {
        this.Row = row;
        this.Column = column;
        this.Obstacles = obstacles;
    }

    public bool ContainsObstacles() => this.Obstacles.Count > 0;

    public bool GoNextInY(int x, int nextY)
    {
        var obstaclesInX = Obstacles.Where(obstacle => obstacle.X == x).Select(o => o.Y);
        return obstaclesInX.Contains(nextY);
    }
}

public class Obstacle
{
    public int X { get; }
    public int Y { get; }

    public Obstacle(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

public enum Compass
{
    N,
    W,
    S,
    E
}