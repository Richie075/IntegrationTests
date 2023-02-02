set TargetDir=%~1
set SolutionDir=%~2
set DIR= %TargetDir%..\
      
xcopy /i /s /y  "%SolutionDir%PersistenceCore\Laetus.NT.Core.PersistenceApi\Context\DefaultData"  "%TargetDir%Context\DefaultData"
      