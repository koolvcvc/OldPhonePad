# Iron Software C# Coding Challenge — OldPhonePad

### Author: Vijay Choudhary

## Problem
Convert keypad input sequences (like old mobile phones) into text, handling backspaces (`*`) and end (`#`).

## Examples
| Input | Output |
|--------|---------|
| 33# | E |
| 227*# | B |
| 4433555 555666# | HELLO |
| 8 88777444666*664# | TURING |

## Design Highlights
- Designed as a indepedent Library so it can be used by any developer in thier Application.   
- Easily embeddable in other applications  
- Dictionary-driven key mapping
- Robust input handling
- Unit tested (xUnit)
##- CI-ready (ci.yml for integration and deplyment)

## Project Structure

 OldPhonePad/
├── src/
│   └── OldPhonePad/
│       ├── OldPhone.cs
│       ├── OldPhonePad.csproj
├── lib/
│   ├── Debug/
│   │   └── OldPhonePad.dll
│   └── Release/
│       └── OldPhonePad.dll
├── tests/
│   └── OldPhonePadTests/
│       ├── OldPhonePadTests.cs
│       ├── OldPhonePadTests.csproj
│       ├── OldPhonePadTests.exe
├── docs/
│   ├── README.md
│   ├── USAGE.md
│   └── ai-prompt.md
├── .github/
│   ├── workflow/
│   │   └── ci.yml
├── .gitignore
├── LICENSE
└── OldPhonePad.sln