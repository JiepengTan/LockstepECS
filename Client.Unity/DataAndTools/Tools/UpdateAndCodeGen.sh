#!/bin/bash
dir="$(cd $(dirname ${BASH_SOURCE[0]}) && pwd)"
cd $dir/../
cd ./bin
pwd

echo "...Update Project File"

rm -rf ../Src/Tools.UnsafeECS.ECdefine.Game/Src/
mkdir -p ../Src/Tools.UnsafeECS.ECdefine.Game/Src/
cp -rf ../../../Client.Unity/Assets/LockstepECS/__DllSourceFiles/Tools.UnsafeECS.ECdefine.Game/Src/ ../Src/Tools.UnsafeECS.ECdefine.Game/Src/
find ../Src/Tools.UnsafeECS.ECdefine.Game/Src/ -name "*.meta" |xargs rm -f

dotnet msbuild /property:Configuration=Debug /p:WarningLevel=0 /verbosity:minimal ../Src/Tools.UnsafeECS.ECDefine.Game/Tools.UnsafeECS.ECDefine.Game.csproj

echo "...Update Project File"
../Tools/UnsafeECSCodeGen