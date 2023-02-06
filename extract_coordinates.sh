#!/bin/bash

FILE="$1"
TIME="$2"
TOPIC="$3"

if [ -z "$FILE" ]; then
    echo "Usage: extract_coordinates.sh file_name [ pre_grep topic(ext/pose)]" >&2
	exit 1
fi

if [ -z "$TOPIC" ]; then
    TOPIC="ext"
fi
if [[ $FILE =~ \.gz$ ]]; then
    if [ -z "$TIME" ]; then
        gunzip --to-stdout $FILE | cat -v | sed -E '/'$TOPIC'\/lat|'$TOPIC'\/lon/!d' | sed -E 's/.*'$TOPIC'.* (.*$)/\1/g' | sed ':a;N;$!ba;s/\n/,/g' | sed -E 's/(.{7,11}),(.{7,11}),/\1,\2 /g'
    else
        gunzip --to-stdout $FILE | cat -v | sed -E '/'$TOPIC'\/lat|'$TOPIC'\/lon/!d' | rg -e $TIME |  sed -E 's/.*'$TOPIC'.* (.*$)/\1/g' | sed ':a;N;$!ba;s/\n/,/g' | sed -E 's/(.{7,11}),(.{7,11}),/\1,\2 /g'
    fi
else
    if [ -z "$TIME" ]; then
        cat -v $FILE | sed -E '/'$TOPIC'\/lat|'$TOPIC'\/lon/!d' | sed -E 's/.*'$TOPIC'.* (.*$)/\1/g' | sed ':a;N;$!ba;s/\n/,/g' | sed -E 's/(.{7,11}),(.{7,11}),/\1,\2 /g'
    else
        cat -v $FILE | sed -E '/'$TOPIC'\/lat|'$TOPIC'\/lon/!d' | rg -e $TIME |  sed -E 's/.*'$TOPIC'.* (.*$)/\1/g' | sed ':a;N;$!ba;s/\n/,/g' | sed -E 's/(.{7,11}),(.{7,11}),/\1,\2 /g'
    fi
fi
