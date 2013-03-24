[Setup]
AppName=Gladiawars (Alpha Build)
AppVersion=0.1
DefaultDirName={pf}\Gladiawars
DefaultGroupName=Gladiawars
Compression=lzma2
SolidCompression=yes
OutputDir=Inno Output
UninstallDisplayName=Uninstall Gladiawars
SetupIconFile=Logo.bmp

[Files]
Source: "Gladiawars.exe";     DestDir: "{app}"
Source: "Gladiawars_Data\*";  DestDir: "{app}\Gladiawars_Data" ; Flags: recursesubdirs

[Icons]
Name: "{group}\Gladiawars"; Filename: "{app}\Gladiawars.exe"