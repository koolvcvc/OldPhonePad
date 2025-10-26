using Xunit;

public class OldPhonePadTests
{
    [Fact]
    public void ReturnsEmpty_ForNullOrWhitespace()
    {
        Assert.Equal(string.Empty, OldPhone.OldPhonePad(null!));
        Assert.Equal(string.Empty, OldPhone.OldPhonePad(string.Empty));
        Assert.Equal(string.Empty, OldPhone.OldPhonePad("   "));
    }

    [Fact]
    public void Decodes_Desired_Example()
    {
        // Example from Program prompt
        string input = "8 88777444666*664#";
        string expected = "TURING";
        Assert.Equal(expected, OldPhone.OldPhonePad(input));
    }

    [Fact]
    public void Backspace_Star_RemovesPreviousCharacter()
    {
        // 44 -> H appended, '*' commits then deletes last output, then 4 -> G
        Assert.Equal("G", OldPhone.OldPhonePad("44*4"));
    }

    [Fact]
    public void Hash_EndsInputEarly_IgnoresTrailing()
    {
        // 999 -> Y, characters after # are ignored
        Assert.Equal("Y", OldPhone.OldPhonePad("999#222"));
    }

    [Fact]
    public void WrapsAround_WhenPressesExceedKeyLength()
    {
        // '7' -> "PQRS" length 4. Five presses => (5-1)%4 = 0 -> 'P'
        Assert.Equal("P", OldPhone.OldPhonePad("77777"));
    }

    [Fact]
    public void IgnoresInvalidCharacters()
    {
        // Letters and punctuation not in KeyMap are ignored
        Assert.Equal("H", OldPhone.OldPhonePad("4a!4")); // 4 then invalids then 4 -> two separate '4' presses => H 
        // To exercise ignoring and commit behavior more explicitly:
        Assert.Equal("GG", OldPhone.OldPhonePad("4 a 4"));
    }
}