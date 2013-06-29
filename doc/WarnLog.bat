@ECHO OFF
find "Warn:" help\LastBuild.log > help\WarnLastBuildAll.log
find /v "._" help\WarnLastBuildAll.log > help\WarnLastBuild.txt
::find ":Guson.Registry" help\WarnLastBuild.txt > help\WarnLastBuildRegistry.txt
del help\WarnLastBuildAll.log
help\WarnLastBuild.txt