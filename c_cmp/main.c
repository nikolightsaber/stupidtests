#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define PREFIX_TOPIC "map/"

static void brmapping_mqtt_handler(char *topic, char *payload, int len, int retain)
{
    if (len > 0 && strstr(topic, PREFIX_TOPIC "path/req") != NULL)
    {
        char req_id[15];
        if(strcmp(topic, PREFIX_TOPIC "path/req") != 0)
        {
            strcpy(req_id, topic + strlen(PREFIX_TOPIC "path/req"));
            printf("gotit extra: %s\n", req_id);
        }
        else
            printf("gotit nothing \n");
        return;
    }
    printf("nope\n");

}

int main() {

    char* a = "";
    if (*a == '\0')
        printf("yess\n");
    char *topic = "map/path/req/id-123456";
    char *payload = "31, 0";
    brmapping_mqtt_handler(topic, payload, strlen(payload), 0);

    char *topic2 = "map/path/req";
    char *payload2 = "31, 0";
    brmapping_mqtt_handler(topic2, payload2, strlen(payload), 0);
    return 0;
}
