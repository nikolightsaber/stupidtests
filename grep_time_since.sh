#!/bin/bash

TIME="$1"

if [ -z "$TIME" ]; then
    echo "Usage: grep_time_since.sh time" >&2
	exit 1
fi
#python3 /home/nikolai/code/stupidtests/time_since.py $TIME
rg -e "'"$(python3 /home/nikolai/code/stupidtests/time_since.py $TIME)"'"
