# Performance Report UI Fix

This extracted mod provides a replacement end of round report UI.

## Building
Use the provided `PerformanceReportUIFix.csproj` to compile the plugin against the game assemblies and BepInEx.

## Assets
The plugin expects an asset bundle named `performancereport_assets` to be located next to the compiled DLL. The bundle must contain the following prefabs:

- `Assets/Prefabs/UI/PerformanceReport.prefab`
- `Assets/Prefabs/UI/DeadContainer.prefab`
- `Assets/Prefabs/UI/MissingContainer.prefab`
- `Assets/Prefabs/UI/NoteContainer.prefab`

Place the compiled plugin DLL and the asset bundle inside the BepInEx `plugins` folder.
