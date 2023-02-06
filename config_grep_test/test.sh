#!/bin/sh


# this is just a script to easily grab an entry from /etc/Serials.xml or /var/lib/brain/config.xml

if [ "$#" = 1 ]; then
	FILE=/etc/Serials.xml
	KEY="$1"
else
case "$1" in
serials)
	FILE=/etc/Serials.xml
	;;
config)
	FILE=./config.xml
	;;
*)
	if [ -f "$1" ]; then
		FILE="$1"
	else
		echo "usage: $0 (serials|config|PATH/TO_FILE) KEY" >&2
		echo "KEY is any of 'Brand', 'MachineID', ..." >&2
		exit 1
	fi
	;;
esac
KEY="$2"
fi

if [ "$FILE" = /etc/Serials.xml ]; then
	# /etc/Serials is a special 2-line key-value structure
	grep -i "$KEY" "$FILE" -A1 | tail -n1 | sed -e 's,<[^>]*>,,g' -e 's,^ *,,g' -e 's, *$,,' -e 's,\r,,g'
else
	grep -i "$KEY" "$FILE" 2>/dev/null | sed -e 's,<[^>]*>,,g' -e 's,^ *,,' -e '/^$/ d'
fi
