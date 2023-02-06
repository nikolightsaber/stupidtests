#!/bin/sh

get_apn()
{
    # usage: apn|password|username ICCID|MSNMCC DBFILE
    KEY="$2"
    while read DBKEY DBAPN DBPASSWORD DBUSERNAME; do
        if [ "$KEY" = "$DBKEY" ]; then
			eval RESULT="\$DB$(echo $1 | tr 'a-z' 'A-Z')"
            echo $RESULT
            return 0
        fi
    done 2>/dev/null < "$3"
    return 1
}

echo "$(get_apn 'apn' '20601' 'inp1')"
echo "$(get_apn 'apn' '20610' 'inp')"
echo "$(get_apn 'apn' '24007' 'inp')"
echo "$(get_apn 'apn' '20601' 'inp')"
echo "-----"
echo "$(get_apn 'apn' '44051' 'inp')"
echo "$(get_apn 'username' '44051' 'inp')"
echo "$(get_apn 'password' '44051' 'inp')"

set_apn()
{
	# usage: apn|password|username value
	APN_TOPIC="$1"
	case "$APN_TOPIC" in
	apn|password|username) ;;
	*) return 1 ;;
	esac

	NEW_VAL="$2"
	OLDVAL=`mqttretained net/ppp0/$APN_TOPIC`
	# publish to the rest
	if [ "$NEW_VAL" != "$OLDVAL" ]; then
		mosquitto_pub -i "$MQTTNAME-set-$APN_TOPIC" -r -t "net/ppp0/$APN_TOPIC" -m "$NEW_VAL"
		# avoid 'systemctl restart' as pppd may not have been started yet.
		echo "set_$APN_TOPIC' '$OLDVAL' '$NEW_VAL': kill pppd" >&2
	fi
}
echo "-------"
echo "$(set_apn 'password' '1234')"
echo "$(set_apn 'password' '1234')"
echo "$(set_apn 'password' '12345')"
echo "$(set_apn 'password' '')"

echo "-------"
echo "$(set_apn 'pwd' '1234')"
echo "$(set_apn 'pwd' '1234')"
echo "$(set_apn 'pwd' '12345')"
echo "$(set_apn 'pwd' '')"

echo "-------"
echo "$(set_apn 'apn' '1234')"
echo "$(set_apn 'apn' '1234')"
echo "$(set_apn 'apn' '12345')"
echo "$(set_apn 'apn' '')"

echo "-------"
echo "$(set_apn 'username' '1234')"
echo "$(set_apn 'username' '1234')"
echo "$(set_apn 'username' '12345')"
echo "$(set_apn 'username' '')"

APNDB=db

set_manual_apn()
{
	APN="$1"
	ICCID="$2"
	if [ -n "$ICCID" -a -n "$APN" ]; then
		SAVED_APN=`get_apn "apn" "$ICCID" "$APNDB"`
		if [ "$SAVED_APN" != "$APN" ]; then
			# save APN for this operator
			SAVED_PWD=`get_apn "password" "$ICCID" "$APNDB"`
			SAVED_USR=`get_apn "username" "$ICCID" "$APNDB"`
			touch "$APNDB" && \
			sed -i -e "/^$ICCID /d" "$APNDB" && \
			printf '%s %s %s %s\n' "$ICCID" "$APN" "$SAVED_PWD" "$SAVED_USR" \
			| sed -e 's/ *$//' >> "$APNDB"
		fi && \
		set_apn "apn" "$APN" true
	fi
}

set_manual_apn "prox" "123432347"
set_manual_apn "praa" "123432347"
set_manual_apn "prox" "1432347"
set_manual_apn "prox2" "1432347"
set_manual_apn "prox2" "1432347"
set_manual_apn "prox2" "14323"
