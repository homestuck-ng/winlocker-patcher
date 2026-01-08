# WinlockerPatcher

WinlockerPatcher is a Windows Forms application written in C#.  
It modifies a stub executable by replacing predefined string placeholders with user-provided values using the dnlib library.

The application loads the stub from embedded resources, patches it in memory, and produces a customized executable file.

## Description

The program provides a graphical interface that allows the user to enter various parameters such as title text, threat text, contact information, password prompt text, password value, and color.  
These values are injected directly into the stub executable by scanning and modifying IL instructions.

The resulting executable is saved as `client.exe`.

## Features

- Windows Forms graphical interface
- Required field validation
- Stub executable embedded as a binary resource
- In-memory IL patching using dnlib
- No external stub files required at runtime
- Simple and deterministic string replacement logic

## How It Works

1. The stub executable is embedded into the application resources.
2. The user fills in all required fields in the UI.
3. The application loads the stub from resources into memory.
4. All `ldstr` instructions are scanned.
5. Placeholder strings are replaced with user-defined values.
6. The modified executable is written to disk as `client.exe`.

## Placeholders

The stub executable must contain the following placeholder strings:

- `TITLE_PLACEHOLDER_123456789012345678901234567890`
- `THREAT_TEXT_PLACEHOLDER_123456789012345678901234567890`
- `CONTACT_TEXT_PLACEHOLDER_123456789012345678901234567890`
- `CONTACT_PLACEHOLDER_123456789012345678901234567890`
- `PASSWORD_ENTER_TEXT_PLACEHOLDER_123456789012345678901234567890`
- `PASSWORD_PLACEHOLDER_123456789012345678901234567890`
- `COLOR_PLACEHOLDER_123456789012345678901234567890`

All replacements are done using direct string comparison.

## Requirements

- Windows operating system
- .NET Framework or .NET (depending on project configuration)
- Visual Studio
- dnlib (installed via NuGet)

## Project Structure

WinlockerPatcher/
├── Form1.cs
├── Form2.cs
├── Properties/
│ ├── Resources.resx
│ └── Resources.Designer.cs
├── WinlockerPatcher.csproj
└── README.md


The stub executable is embedded in `Resources.resx` as a binary resource.

## Building

1. Open the solution in Visual Studio.
2. Restore NuGet packages.
3. Build the solution.
4. Run the application.
5. Click the build button to generate `client.exe`.

## Notes

- The stub executable is not stored as a standalone file.
- All patching is performed in memory.
- Build artifacts such as `bin` and `obj` directories should not be committed.
- The project logic assumes the stub contains the exact placeholder strings.

## License

This project is provided as-is, without warranty of any kind.  
Use responsibly and at your own risk.