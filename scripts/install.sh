#!/bin/bash
echo "[install] Making sure git lfs is installed"
# Get git lfs support
curl -s https://packagecloud.io/install/repositories/github/git-lfs/script.deb.sh | sudo bash
sudo apt-get install git-lfs

# You need git lfs to get the assets that are not code
echo
echo "[install] Making sure all assets have been retrieved from the distant repository"
git lfs install
git lfs pull

