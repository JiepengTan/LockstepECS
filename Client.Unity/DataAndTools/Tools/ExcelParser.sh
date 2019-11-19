#!/bin/bash
dir="$(cd $(dirname ${BASH_SOURCE[0]}) && pwd)"
cd $dir/../
cd ./bin
pwd
echo "1.ExcelParser"
mono Tools.ExcelParser.exe ../Config/ExcelParser/Config.json

echo "2.Copy Source File"
rm -rf ../../Assets/LockstepECS/__DllSourceFiles/Game.Model/Src/__ExcelParser/*.cs
mkdir -p ../../Assets/LockstepECS/__DllSourceFiles/Game.Model/Src/__ExcelParser/
cp ../Config/ExcelParser/Output/CodeCS/*.cs ../../Assets/LockstepECS/__DllSourceFiles/Game.Model/Src/__ExcelParser/
rm -rf ../../Assets/LockstepECS/__DllSourceFiles/Game.Model/Src/__ExcelParser/Table_Assets.cs


