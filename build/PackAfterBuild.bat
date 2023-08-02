@Echo off
Title PackAfterBuild

Set y=%date:~0,4%
Set d=%date:~8,2%
Set m=%date:~5,2%
Set h=%time:~0,2%
Set min=%time:~3,2%

Set BUILD_VERR=%1
Set PROJECT_PATH=%2
Set OUTPUTDIR=%3

for /l %%i in (1,1,9) do (
	set BUILD_VER=%BUILD_VERR%.%m%%d%%%i
	Echo version %BUILD_VERR%.%m%%d%%%i
	if not exist %OUTPUTDIR%\%BUILD_VERR%.%m%%d%%%i (goto versionfind)
)

:versionfind
Md %OUTPUTDIR%\%BUILD_VER%
Echo makedir %OUTPUTDIR%\%BUILD_VER%
xcopy /e/f/y "%PROJECT_PATH%\LDPP-API\bin\Debug\*.*" "%OUTPUTDIR%\%BUILD_VER%\"

if not exist %OUTPUTDIR%\lastest (Md %OUTPUTDIR%\lastest)
del %OUTPUTDIR%\lastest\*.* /f/s/q
xcopy /e/f/y "%PROJECT_PATH%\LDPP-API\bin\Debug\*.*" "%OUTPUTDIR%\lastest\"