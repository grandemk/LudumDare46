#!/bin/bash

unity_version="2019.3.9f1"
unity_exe=${1-}

if [ "$1" == "" ] ; then
    # default location when seen from wsl
    unity_exe="/mnt/c/Program Files/Unity/Hub/Editor/$unity_version/Editor/Unity.exe"
fi

echo "[webgl] Started Unity Build $(date)"
"$unity_exe" -batchmode -nographics --projectPath=$(pwd) -executeMethod WebGLBuilder.build -quit
echo "[webgl] Ended Unity Build $(date)"

