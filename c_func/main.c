#include <stdio.h>

typedef struct Data {
    int a;
    int b;
}Data;

void some_func(Data* d)
{
    p.x = 0;
    d.a = 1;
}

int main() {
    int zone = 30;
    Point p;
    p.x = 50.7186411;
    p.y = 4.59693449;

    Data d{};

    some_func(p, d);

    std::cout << p.x << "\n";
    std::cout << d.a << "\n";
    return 0;
}
