# CMAKE generated file: DO NOT EDIT!
# Generated by "Unix Makefiles" Generator, CMake Version 3.0

#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:

# Remove some rules from gmake that .SUFFIXES does not remove.
SUFFIXES =

.SUFFIXES: .hpux_make_needs_suffix_list

# Produce verbose output by default.
VERBOSE = 1

# Suppress display of executed commands.
$(VERBOSE).SILENT:

# A target that is always out of date.
cmake_force:
.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

# The shell in which to execute make rules.
SHELL = /bin/sh

# The CMake executable.
CMAKE_COMMAND = /usr/bin/cmake

# The command to remove a file.
RM = /usr/bin/cmake -E remove -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = /home/chengque/workspace/src/mavlink-gbp-release

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = /home/chengque/workspace/src/mavlink-gbp-release/obj-i686-linux-gnu

# Utility rule file for pixhawk.xml-v1.0.

# Include the progress variables for this target.
include CMakeFiles/pixhawk.xml-v1.0.dir/progress.make

CMakeFiles/pixhawk.xml-v1.0: pixhawk.xml-v1.0-stamp

pixhawk.xml-v1.0-stamp: ../message_definitions/v1.0/pixhawk.xml
pixhawk.xml-v1.0-stamp: ../pymavlink/generator/mavgen.py
	$(CMAKE_COMMAND) -E cmake_progress_report /home/chengque/workspace/src/mavlink-gbp-release/obj-i686-linux-gnu/CMakeFiles $(CMAKE_PROGRESS_1)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --blue --bold "Generating pixhawk.xml-v1.0-stamp"
	/usr/bin/env PYTHONPATH="/home/chengque/workspace/src/mavlink-gbp-release:/mnt/project/documents/uav/workspace/ros/catkin_ws/devel/lib/python2.7/dist-packages:/opt/ros/jade/lib/python2.7/dist-packages" /usr/bin/python -m pymavlink.tools.mavgen --lang=C --wire-protocol=1.0 --output=include/v1.0 /home/chengque/workspace/src/mavlink-gbp-release/message_definitions/v1.0/pixhawk.xml
	touch pixhawk.xml-v1.0-stamp

pixhawk.xml-v1.0: CMakeFiles/pixhawk.xml-v1.0
pixhawk.xml-v1.0: pixhawk.xml-v1.0-stamp
pixhawk.xml-v1.0: CMakeFiles/pixhawk.xml-v1.0.dir/build.make
.PHONY : pixhawk.xml-v1.0

# Rule to build all files generated by this target.
CMakeFiles/pixhawk.xml-v1.0.dir/build: pixhawk.xml-v1.0
.PHONY : CMakeFiles/pixhawk.xml-v1.0.dir/build

CMakeFiles/pixhawk.xml-v1.0.dir/clean:
	$(CMAKE_COMMAND) -P CMakeFiles/pixhawk.xml-v1.0.dir/cmake_clean.cmake
.PHONY : CMakeFiles/pixhawk.xml-v1.0.dir/clean

CMakeFiles/pixhawk.xml-v1.0.dir/depend:
	cd /home/chengque/workspace/src/mavlink-gbp-release/obj-i686-linux-gnu && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" /home/chengque/workspace/src/mavlink-gbp-release /home/chengque/workspace/src/mavlink-gbp-release /home/chengque/workspace/src/mavlink-gbp-release/obj-i686-linux-gnu /home/chengque/workspace/src/mavlink-gbp-release/obj-i686-linux-gnu /home/chengque/workspace/src/mavlink-gbp-release/obj-i686-linux-gnu/CMakeFiles/pixhawk.xml-v1.0.dir/DependInfo.cmake --color=$(COLOR)
.PHONY : CMakeFiles/pixhawk.xml-v1.0.dir/depend

