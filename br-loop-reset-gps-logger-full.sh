#!/bin/sh

while true; do
    sleep 600
    mosquitto_pub -t trig/reboot-rtk/zed-sw -m 1
    echo "{\"time\":$(date +%s),\"reset\":1}"
done

LASTLAT=0
LASTLON=0
LASTALT=0
LASTSPEED=0
LASTSATUSE=0
LASTHDOP=0
LASTVDOP=0
LASTPDOP=0
LASTGEOID=0

mosquitto_sub -F "%U %t %p" -t 'ext/raw_quality' -t 'ext/lat' -t 'ext/lon' | while read TIME TOPIC PAYLOAD; do

    if [[ "$PAYLOAD" = "(null)" || -z "$PAYLOAD" ]]; then
        # remove this mosquitto_sub fancy output
        PAYLOAD=0
    fi

    case "$TOPIC" in
    ext/raw_quality)
        echo "{\"time\":$TIME,\"lat\":$LASTLAT,\"lon\":$LASTLON,\"alt\":$LASTALT,\"quality\":$PAYLOAD,\"quality\":$PAYLOAD,\"quality\":$PAYLOAD,\"quality\":$PAYLOAD,\"quality\":$PAYLOAD}"
        ;;
    ext/lat)
        LASTLAT=$PAYLOAD
        ;;
    ext/lon)
        LASTLON=$PAYLOAD
        ;;
    ext/alt)
        LASTALT=$PAYLOAD
        ;;
    ext/speed)
        LASTSPEED=$PAYLOAD
        ;;
    ext/gn/satuse)
        LASTSATUSE=$PAYLOAD
        ;;
    ext/hdop)
        LASTHDOP=$PAYLOAD
        ;;
    ext/vdop)
        LASTVDOP=$PAYLOAD
        ;;
    ext/pdop)
        LASTPDOP=$PAYLOAD
        ;;
    ext/geoid)
        LASTGEOID=$PAYLOAD
        ;;
    esac
done

kill -9 %1
