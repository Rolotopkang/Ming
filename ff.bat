@echo off
REM 设置要检查的目标目录
set "target_folder=%~1"
if "%target_folder%"=="" set "target_folder=%CD%"

REM 创建临时文件
set "tempfile=%TEMP%\temp_file.txt"

echo 正在检查和转换行尾符为 CRLF...

REM 遍历目标文件夹及其子文件夹下的所有文本文件
for /r "%target_folder%" %%f in (*.cs *.txt *.shader *.meta *.json *.xml) do (
    echo 正在处理: %%f
    REM 将 LF 转换为 CRLF
    powershell -Command "(Get-Content -Raw '%%f') -replace \"`r?`n\", \"`r`n\" | Set-Content -NoNewline '%%f'"
)

echo 完成！所有文本文件的行尾符已转换为 CRLF。
pause