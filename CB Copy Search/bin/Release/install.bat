@echo off
if not exist c:\cbcopysearch md c:\cbcopysearch
if exist "c:\cbcopysearch\CB Copy Search.exe" del "c:\cbcopysearch\CB Copy Search.exe"
if exist "c:\cbcopysearch\CB Copy Search.exe.config" del "c:\cbcopysearch\CB Copy Search.exe.config"

rem copy "h:\apps\cbcopysearch\CB Copy Search.exe" c:\cbcopysearch
rem copy "h:\apps\cbcopysearch\CB Copy Search.exe.config" c:\cbcopysearch

copy "CB Copy Search.exe" c:\cbcopysearch
copy "CB Copy Search.exe.config" c:\cbcopysearch

echo Install / Update completed...
pause