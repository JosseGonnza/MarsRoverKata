using FluentAssertions;

namespace MarsRover.Tests;
/*
 * 1- Initial position 0:0:N
 * 🎯     "M" -> 0:1:N    forward
 * 🎯     "M" -> -1:0:W    forward
 * 🎯     "M" -> 0:-1:S    forward
 * 🎯     "M" -> 1:0:E    forward
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

    [Fact(DisplayName = "change direction")]
    public void change_direction()
    {
        var marsRover = new MarsRover(0,0, Compass.N);

        var result = marsRover.Execute("L");

        result.Should().Be("0:0:W");
    }
}

public class MarsRover
{
    private int X { get; set; }
    private int Y { get; set; }
    private Compass Compass { get; set; }

    public MarsRover(int x, int y, Compass compass)
    {
        this.X = x;
        this.Y = y;
        this.Compass = compass;
    }

    public string Execute(string command)
    {
        foreach (var c in command.ToCharArray())
        {
            if (c == 'M')
            {
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
            }

            if (c == 'L' && this.Compass == Compass.N)
            {
                this.Compass = Compass.W;
            }
        }

        return $"{this.X}:{this.Y}:{this.Compass}";
    }
}

public enum Compass
{
    N,
    W,
    S,
    E
}