using FluentAssertions;

namespace MarsRover.Tests;
/*
 * 1- Initial position 0:0:N
 *      "M" -> 0:1:N    forward
 *      "M" -> -1:0:W    forward
 *      "M" -> 0:-1:S    forward
 *      "M" -> 1:0:E    forward
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
    [Theory]
    [InlineData('N',"0:1:N")]
    [InlineData('W',"-1:0:W")]
    [InlineData('S',"0:-1:S")]
    [InlineData('E',"1:0:E")]
    public void move_forward(char direction, string expected)
    {
        var marsRover = new MarsRover(0, 0, direction);
        var result = marsRover.Execute("M");
        result.Should().Be(expected);
    }
}

public class MarsRover
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Direction { get; }

    public MarsRover(int x, int y, char direction)
    {
        X = x;
        Y = y;
        Direction = direction;
    }

    public string Execute(string command)
    {
        if (this.Direction == 'N')
        {
            this.Y++;
        }
        if (this.Direction == 'S')
        {
            this.Y--;
        }
        if (this.Direction == 'W')
        {
            this.X--;
        }
        if (this.Direction == 'E')
        {
            this.X++;
        }
        return $"{this.X}:{this.Y}:{this.Direction}";
    }
}