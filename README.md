# GlobPatternTester
Tests Glob Patterns against a directory and returns the list of resulting file paths

# Arguments

```text
  -i, --include      Required. Include Glob patterns.  Use ; for multiple.

  -e, --exclude      Required. Exclude Glob patterns.  Use ; for multiple.

  -d, --targetDir    Required. Target Directory
```

# Example Usage

```dos
D:\dev\>GlobPatternTester.exe -i **/*.Storage/**/*data*.dll -e **/Reference* -d "c:\Program Files (x86)"

WindowsPowerShell/Modules/Azure.Storage/4.2.1/Microsoft.WindowsAzure.Storage.DataMovement.dll
WindowsPowerShell/Modules/AzureRM.Storage/4.2.3/Microsoft.Data.Edm.dll
WindowsPowerShell/Modules/AzureRM.Storage/4.2.3/Microsoft.Data.OData.dll
WindowsPowerShell/Modules/AzureRM.Storage/4.2.3/Microsoft.Data.Services.Client.dll
```
