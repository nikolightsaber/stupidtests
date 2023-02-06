#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main() {
    int i = 1;
    int b = i+=2;

    int a = -1;
    if(a)
        printf("i = %i, b = %i", i, b);

    return 0;
}
