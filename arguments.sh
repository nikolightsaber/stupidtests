#!/bin/sh

get_apn()
{
    # usage: apn|password|username ICCID|MSNMCC DBFILE
    KEY="$2"
    echo "$3" | while read DBKEY DBAPN DBPASSWORD DBUSERNAME; do
        if [ "$KEY" == "$DBKEY" ]; then
			eval RESULT=\$DB$(echo "$1" | tr 'a-z' 'A-Z')
            echo $RESULT
            return 0
        fi
    done
    return 1
}

get_info2()
{
    echo $1 | sed -e 's/^[^ ]* //' -e 's/ *$//'
}
get_info()
{
    echo $1 | while read ID INFO; do echo "$INFO"; done
}

print_args()
{
	APN="$1"
	PWD="$2"
	USR="$3"

    if [ -n "$APN" ]; then
        echo "$APN"
    fi
    if [ -n "$PWD" ]; then
        echo "$PWD"
    fi
    if [ -n "$USR" ]; then
        echo "$USR"
    fi
}

print_args "apn" "pwd" "user"
echo "----"
print_args "apn" "pwd"
echo "---"
print_args "apn"
echo "---"
echo "---"
echo "---"
print_args $(get_info "id apn usr password")
echo "---"
print_args $(get_info "id apn usr")
echo "---"
print_args $(get_info "id apn")
echo "---"
echo "---"
echo "---"
print_args $(get_info2 "id apn usr password")
echo "---"
print_args $(get_info2 "id apn usr")
echo "---"
print_args $(get_info2 "id apn        ")
echo "---"

OUT="arg1 arg2"
echo ${OUT}


