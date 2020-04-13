#/bin/bash

if ! which butler > /dev/null ; then
    echo "Couldn't find butler. Install butler by following https://itch.io/docs/butler/"
    exit 1
fi

webgl_build=builds/WebGL/


if ! [ -e "$webgl_build" ] ; then
    echo "You need to build the project (You can use scripts/webgl.sh)"
    exit 1
fi

itchio_dest="tic-is-mad/ludumdare46:web"

butler push "$webgl_build" "$itchio_dest"
butler status "$itchio_dest"
