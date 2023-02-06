#!/bin/sh

mount -orw,remount /
while true; do

    echo "TEST: SETTING ZED 1.13"
    cp /var/tmp/conf113.conf /etc/remote-firmware.conf

    echo "TEST: RESETTING ZED"
    /etc/rc.ublox usb-reset-ext-hub

    sleep 1200

    echo "TEST: SETTING ZED 1.13"
    cp /var/tmp/conf132.conf /etc/remote-firmware.conf

    echo "TEST: RESETTING ZED"
    /etc/rc.ublox usb-reset-ext-hub

    sleep 1200

done
