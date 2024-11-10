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
    public static IEnumerable<object[]> GetRoverStateData()
    {
        yield return new object[] { new NorthState(), "0:1:N" };
        yield return new object[] { new WestState(), "-1:0:W" };
        yield return new object[] { new SouthState(), "0:-1:S" };
        yield return new object[] { new EastState(), "1:0:E" };
    }
    
    [Theory]
    [MemberData(nameof(GetRoverStateData))]
    public void move_forward(IRoverState compass, string expected)
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
        var marsRover = new MarsRover(0,0, new NorthState());

        var result = marsRover.Execute(command);

        result.Should().Be(expected);
    }
    
    [Theory(DisplayName = "change orientation when initial is west")]
    [InlineData("L", "0:0:S")]
    [InlineData("R", "0:0:N")]
    public void change_orientation_when_initial_is_west(string command, string expected)
    {
        var marsRover = new MarsRover(0,0, new WestState());

        var result = marsRover.Execute(command);

        result.Should().Be(expected);
    }
    
    [Theory(DisplayName = "change orientation when initial is east")]
    [InlineData("L", "0:0:N")]
    [InlineData("R", "0:0:S")]
    public void change_orientation_when_initial_is_east(string command, string expected)
    {
        var marsRover = new MarsRover(0,0, new EastState());

        var result = marsRover.Execute(command);

        result.Should().Be(expected);
    }
    
    [Theory(DisplayName = "change orientation when initial is south")]
    [InlineData("L", "0:0:E")]
    [InlineData("R", "0:0:W")]
    public void change_orientation_when_initial_is_south(string command, string expected)
    {
        var marsRover = new MarsRover(0,0, new SouthState());

        var result = marsRover.Execute(command);

        result.Should().Be(expected);
    }

    [Theory(DisplayName = "process many commands when initial orientation is north")]
    [InlineData("RM", "1:0:E")]
    [InlineData("LM", "-1:0:W")]
    public void process_many_commands_when_initial_orientation_is_north(string command, string expected)
    {
        var marsRover = new MarsRover(0,0, new NorthState());

        var result = marsRover.Execute(command);

        result.Should().Be(expected);
    }
    
    [Theory(DisplayName = "process many commands when initial orientation is west")]
    [InlineData("RM", "0:1:N")]
    [InlineData("LM", "0:-1:S")]
    public void process_many_commands_when_initial_orientation_is_west(string command, string expected)
    {
        var marsRover = new MarsRover(0,0, new WestState());

        var result = marsRover.Execute(command);

        result.Should().Be(expected);
    }
}