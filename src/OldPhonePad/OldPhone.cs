using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Simple translator for classic multi-tap phone keypad sequences.
/// Supports:
/// - digits 0-9 mapped to characters (see <see cref="KeyMap"/>)
/// - space ' ' to commit the current key buffer
/// - '*' to commit then delete the previous output character (backspace)
/// - '#' to commit and end input immediately
/// Invalid characters are ignored.
/// </summary>
public class OldPhone
{
    /// <summary>
    /// Mapping of numeric keys to the characters they cycle through on repeated presses.
    /// The mapping uses the same ordering as a traditional phone keypad.
    /// </summary>
    private static readonly Dictionary<char, string> KeyMap = new()
    {
        // '1' historically contained punctuation on some phones
        ['1'] = "&'(",
        ['2'] = "ABC",
        ['3'] = "DEF",
        ['4'] = "GHI",
        ['5'] = "JKL",
        ['6'] = "MNO",
        ['7'] = "PQRS",
        ['8'] = "TUV",
        ['9'] = "WXYZ",
        // '0' maps to space character
        ['0'] = " "
    };

    /// <summary>
    /// Decode a sequence of keypad presses into the resulting string.
    /// Example: "4433555 555666#" -> "HELLO"
    /// </summary>
    /// <param name="input">A sequence of characters representing key presses.</param>
    /// <returns>The decoded output string. Returns empty string for null/whitespace input.</returns>
    public static string OldPhonePad(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // Output builder holds the final translated characters.
        StringBuilder output = new();

        // Current accumulates consecutive presses of the same numeric key.
        // e.g. pressing '4' three times produces "444" in current before commit.
        StringBuilder current = new();

        foreach (char ch in input)
        {
            switch (ch)
            {
                case '#':
                    // Commit any buffered presses, then end processing immediately.
                    CommitBuffer(current, output);
                    return output.ToString();

                case ' ':
                    // Space commits the current buffer but does not add any additional output.
                    CommitBuffer(current, output);
                    break;

                case '*':
                    // '*' acts as a backspace: commit the current buffer, then remove
                    // the last character from output if present.
                    CommitBuffer(current, output);
                    if (output.Length > 0)
                        output.Remove(output.Length - 1, 1);
                    break;

                default:
                    // For numeric keys that exist in the KeyMap, append to current buffer.
                    // If the user starts pressing a different key, first commit the previous buffer.
                    if (KeyMap.ContainsKey(ch))
                    {
                        if (current.Length > 0 && current[0] != ch)
                        {
                            CommitBuffer(current, output);
                        }
                        current.Append(ch);
                    }
                    // Any characters not present in KeyMap (letters, punctuation not mapped, etc.)
                    // are ignored silently.
                    break;
            }
        }

        // Commit any remaining buffered presses after the input is exhausted.
        CommitBuffer(current, output);
        return output.ToString();
    }

    /// <summary>
    /// Convert the buffered identical key presses into a single output character and append it.
    /// Example: current = "777" (three presses of key '7') -> selects the 3rd character in KeyMap['7'].
    /// Uses mod arithmetic to wrap when presses exceed the number of characters for a key.
    /// </summary>
    /// <param name="current">Buffer of identical key characters (e.g. "222").</param>
    /// <param name="output">The output builder to append the resolved character to.</param>
    private static void CommitBuffer(StringBuilder current, StringBuilder output)
    {
        if (current.Length == 0) return;

        // Get the key character (all chars in current are the same)
        char key = current[0];
        string letters = KeyMap[key];

        // Calculate the index using (pressCount - 1) mod letters.Length.
        // Example: pressing a 1 time selects index 0; pressing (length+1) wraps to index 0 again.
        int index = (current.Length - 1) % letters.Length;
        output.Append(letters[index]);

        // Clear the buffer for the next sequence of presses.
        current.Clear();
    }

    // The following interactive Main was intentionally commented out when this project
    // became a class library. Keep it for local manual testing if you re-enable OutputType to Exe.
    /*
    public static void Main(string[] args)
    {
        Console.WriteLine("=== OldPhonePad Decoder ===");
        Console.WriteLine("Enter keypad sequence (end with #). Example: 4433555 555666#");
        Console.WriteLine("Press Enter without input to exit.\n");

        while (true)
        {
            Console.Write("Input: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                break;

            string output = OldPhone.OldPhonePad(input);
            Console.WriteLine($"Output: {output}\n");
        }

        Console.WriteLine("\nDone.");
    }
    */
}