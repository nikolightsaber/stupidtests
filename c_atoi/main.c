#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main() {
    char *payload = "30";

    char temp[50];
    strncpy(temp, payload, sizeof(temp));
    // payload format: zone number: if negative southern hemisphere
    int utm_zone = atoi(temp);

    printf("%i", utm_zone);

    return 0;
}
