set TargetDir=%~1
set SolutionDir=%~2
set DIR=%TargetDir%..\

xcopy /i /s /y  "%DIR%Common\Laetus.NT.Core.Domain"  "%TargetDir%agents\Common"
xcopy /i /s /y  "%DIR%Agents\Laetus.NT.Core.ControlPanel"  "%TargetDir%agents\Laetus.NT.Core.ControlPanel"
xcopy /i /s /y  "%DIR%Agents\Laetus.NT.Core.MappingEditor.Agent"  "%TargetDir%agents\Laetus.NT.Core.MappingEditor.Agent"
xcopy /i /s /y  "%DIR%Agents\Laetus.NT.Core.MasterDataManager" "%TargetDir%agents\Laetus.NT.Core.MasterDataManager.Agent"
xcopy /i /s /y  "%DIR%Agents\Laetus.NT.Core.PCSManager.Agent" "%TargetDir%agents\Laetus.NT.Core.PCSManager.Agent"
xcopy /i /s /y  "%DIR%Agents\Laetus.NT.Core.POManager.Agent"  "%TargetDir%agents\Laetus.NT.Core.POManager.Agent"
xcopy /i /s /y  "%DIR%Agents\Laetus.NT.Core.CodePoolManagement"  "%TargetDir%agents\Laetus.NT.Core.CodePoolManagement.Agent"
xcopy /i /s /y  "%DIR%Agents\Laetus.NT.Core.USCExportImport"  "%TargetDir%agents\Laetus.NT.Core.USCExportImport"
xcopy /i /s /y  "%DIR%Agents\Laetus.NT.Core.EC.Agent"  "%TargetDir%agents\Laetus.NT.Core.EC.Agent"
xcopy /i /s /y  "%DIR%Agents\Laetus.NT.Core.Authentication"  "%TargetDir%agents\Laetus.NT.Core.Authentication.Agent"
xcopy /i /s /y  "%DIR%Agents\Laetus.NT.Core.AuditTrail"  "%TargetDir%agents\Laetus.NT.Core.Platform.AuditTrail"
if EXIST "%DIR%Agents\Laetus.NT.Core.Authentication\DummyAuthAgent.dll" (
xcopy /i /s /y  "%DIR%Agents\Laetus.NT.Core.Authentication\DummyAuthAgent.dll"  "%TargetDir%agents\DummyAuthAgent\" )
