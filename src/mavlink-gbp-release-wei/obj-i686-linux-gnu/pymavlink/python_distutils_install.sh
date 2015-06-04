#!/bin/sh -x
# Modified version of catkin template/python_distutils_install.sh.in

if [ -n "$DESTDIR" ] ; then
    case $DESTDIR in
        /*) # ok
            ;;
        *)
            /bin/echo "DESTDIR argument must be absolute... "
            /bin/echo "otherwise python's distutils will bork things."
            exit 1
    esac
    DESTDIR_ARG="--root=$DESTDIR"
fi

/usr/bin/env \
    "/usr/bin/python" \
    "/home/chengque/workspace/src/mavlink-gbp-release/pymavlink/setup.py" \
    build --build-base "/home/chengque/workspace/src/mavlink-gbp-release/obj-i686-linux-gnu/pymavlink/pybuild" \
    install \
    $DESTDIR_ARG \
    --install-layout=deb --prefix="/opt/ros/jade"
