rem To run under Developer Command Prompt for VS2012
for /f "tokens=*" %%a in ('dir /b *.VertexShader.hlsl') do (
call "%%WindowsSdkDir%%bin\x86\fxc.exe" /T vs_2_0 /O3 /Zpr /Fo %%~na %%a >> hlslcompil.log
)
for /f "tokens=*" %%a in ('dir /b *.PixelShader.hlsl') do (
call "%%WindowsSdkDir%%bin\x86\fxc.exe" /T ps_2_0 /O3 /Zpr /Fo %%~na %%a >> hlslcompil.log
)
pause