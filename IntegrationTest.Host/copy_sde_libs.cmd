set TargetDir=%~1
set SolutionDir=%~2
set DIR= %TargetDir%..\
      
xcopy /I /y  "%SolutionDir%\sde\dds\libs\lib\x64Win64dotnet4.5\*.dll"  "%TargetDir%"
