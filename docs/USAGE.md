# 📘 OldPhonePad Library — Usage Guide

## Overview
The OldPhonePad Library provides a clean, reusable C# implementation for decoding old mobile keypad sequences into readable text.  
It’s ideal for console apps, backend services, or integration testing where keypad-style input needs translation to text.

---

## ⚙️ Installation
use a library OldPhonePad.dll
Then reference it in your app.
---

## 📚 Example Usage
```csharp
using System;
using OldPhonePadLib;

class Program
{
    static void Main()
    {
        string input = "4433555 555666#";
        string output = OldPhone.OldPhonePad(input);
        Console.WriteLine($"Decoded: {output}"); // Output: HELLO
    }
}
```
