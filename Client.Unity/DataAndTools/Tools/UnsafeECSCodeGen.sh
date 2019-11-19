#!/bin/bash
dir="$(cd $(dirname ${BASH_SOURCE[0]}) && pwd)"
cd $dir/../
cd ./bin
pwd
echo "...Update Project File"
mono Tools.UnsafeECSGenerator.dll ../Config/UnsafeECSGenerator/ModelConfig.json
mono Tools.UnsafeECSGenerator.dll ../Config/UnsafeECSGenerator/ViewConfig.json

pwd

echo "...Update Project File"
../Tools/UpdateProjectFile

# copy to unity 
rm -rf ../../Assets/LockstepECS/__DllSourceFiles/Game.Model/Src/__UnsafeECS/Generated/
mkdir -p ../../Assets/LockstepECS/__DllSourceFiles/Game.Model/Src/__UnsafeECS/Generated/
cp -rf ../Src/Tools.UnsafeECS.ECSOutput/Src/Generated/Model/* ../../Assets/LockstepECS/__DllSourceFiles/Game.Model/Src/__UnsafeECS/Generated/


rm -rf ../../Assets/LockstepECS/__DllSourceFiles/Game.View/Src/__UnsafeECS/Generated/
mkdir -p ../../Assets/LockstepECS/__DllSourceFiles/Game.View/Src/__UnsafeECS/Generated/
cp -rf ../Src/Tools.UnsafeECS.ECSOutput/Src/Generated/View/* ../../Assets/LockstepECS/__DllSourceFiles/Game.View/Src/__UnsafeECS/Generated/