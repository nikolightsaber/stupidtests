#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main() {
    /*
    char *payload = "30, 78";

    char temp[50];
    strncpy(temp, payload, sizeof(temp));
    char *zone_number = strtok(temp, ",");
    char zone_letter[2] = {(char) atoi(strtok(NULL, ",")) , 0};
    char *utm_zone = strcat(zone_number, zone_letter);

    printf("%s, %lu, %lu \n\r", utm_zone, strlen(utm_zone), sizeof(utm_zone));
    */

    char *payload = "31, 0";
    char temp[50];
    strncpy(temp, payload, sizeof(temp));
    int utm_zone = atoi(temp);
    printf("%s, %i\n", temp, utm_zone);

    char* topics[] = {"odin/drtk", "base/nav/drtk", "trig/"};
    for(int i = 0; i < 3; ++i)
        if(strncmp(topics[i], "base/nav/drtk", strlen(topics[i])) && strncmp(topics[i], "odin/drtk", strlen(topics[i])))
            printf("topic stop: %s\n", topics[i]);
        else
            printf("topic pass: %s\n", topics[i]);



    return 0;
}
