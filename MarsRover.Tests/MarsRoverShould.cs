using FluentAssertions;

namespace MarsRover.Tests;
/*
 * 1- Initial position 0:0:N
 *      "M" -> 0:1:N    forward
 *
 *      "L" -> 0:0:W    turn left
 *      "LL" -> 0:0:S   turn twice left
 *      "R" -> 0:0:E    turn right
 *      "RR" -> 0:0:S   turn twice right
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
    [Fact]
    public void move_forward()
    {
        var marsRover = new MarsRover(0, 0, 'N');
        var result = marsRover.Execute("M");
        result.Should().Be("0:1:N");
    }
    
    [Fact]
    public void move_forward2()
    {
        var marsRover = new MarsRover(0, 1, 'N');
        var result = marsRover.Execute("M");
        result.Should().Be("0:2:N");
    }
}

public class MarsRover
{
    public int X { get; }
    public int Y { get; }
    public char Direction { get; }

    public MarsRover(int x, int y, char direction)
    {
        X = x;
        Y = y;
        Direction = direction;
    }

    public string Execute(string command)
    {
        for (int i = 0; i < ; i++)
        {
            
        }
        return "0:1:N";
    }
}