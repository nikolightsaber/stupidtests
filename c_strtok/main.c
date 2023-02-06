#include <stdio.h>
#include <stdlib.h>
#include <string.h>

static char* strtotoken(char *line)
{
	static char *saved_line;
	char* str;

	if (!line)
		line = saved_line;

	for (str = line; *str; ++str) {
		if (strchr(",", *str)) {
			*str++ = 0;
            saved_line = str;
            return *line ? line : "";
		}
	}
	saved_line = str;
	return *line ? line : NULL;
}

int main() {
    char str[] = "1,2,,,,6,,8,9";
    char *token;

    printf("%s\n", str);
    token = strtotoken(str);

    int i = 1;
    while(token != NULL) {
        //printf("%s\n", str);
        printf("%d = %s\n", i, token);

        token = strtotoken(NULL);
        i++;
    }
    return 0;
}
