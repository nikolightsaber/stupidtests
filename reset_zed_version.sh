#!/bin/sh

mosquitto_sub -v \
	-t 'ecu/map/zed/FWVER' \
	| while read TOPIC PAYLOAD; do
if [ "$PAYLOAD" = "HPG 1.32" ]; then
     sed -i -e "s/http:\/\/updates.vpn.belrobotics.com\/ublox\/UBX_F9_100_HPG132.df73486d99374142f3aabf79b7178f48.bin/http:\/\/updates.vpn.belrobotics.com\/ublox\/UBX_F9_100_HPG_113_ZED_F9P.7e6e899c5597acddf2f5f2f70fdf5fbe.bin/g" /etc/remote-firmware.conf
else
    sed -i -e "s/http:\/\/updates.vpn.belrobotics.com\/ublox\/UBX_F9_100_HPG_113_ZED_F9P.7e6e899c5597acddf2f5f2f70fdf5fbe.bin/http:\/\/updates.vpn.belrobotics.com\/ublox\/UBX_F9_100_HPG132.df73486d99374142f3aabf79b7178f48.bin/g" /etc/remote-firmware.conf
fi
done

