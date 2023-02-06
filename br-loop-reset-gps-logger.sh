#!/bin/sh

while true; do
    sleep 600
    mosquitto_pub -t trig/reboot-rtk/zed-sw -m 1
    echo "{\"time\":$(date +%s),\"reset\":1}"
done &

LASTQUAL=0
LASTLAT=0
LASTLON=0
PRINTCOUNT=10
LASTTIME=0

mosquitto_sub -F "%U %t %p" -t 'ext/raw_quality' -t 'ext/lat' -t 'ext/lon' | while read TIME TOPIC PAYLOAD; do

    if [[ "$PAYLOAD" = "(null)" || -z "$PAYLOAD" ]]; then
        # remove this mosquitto_sub fancy output
        PAYLOAD=0
    fi

    case "$TOPIC" in
    ext/raw_quality)
        if [ $PAYLOAD != $LASTQUAL ]; then
            LASTQUAL=$PAYLOAD
            PRINTCOUNT=10
            LASTTIME=$((${TIME%.*} + 180));
        fi
        if (( $LASTTIME < ${TIME%.*} )); then
            PRINTCOUNT=10
            LASTTIME=$((${TIME%.*} + 180));
        fi
        if [ $PRINTCOUNT -gt 0 ]; then
            echo "{\"time\":$TIME,\"lat\":$LASTLAT,\"lon\":$LASTLON,\"quality\":$LASTQUAL}"
            PRINTCOUNT=$(($PRINTCOUNT - 1));
        fi
        ;;
    ext/lat)
        LASTLAT=$PAYLOAD
        ;;
    ext/lon)
        LASTLON=$PAYLOAD
        ;;
    esac
done

kill -9 %1
