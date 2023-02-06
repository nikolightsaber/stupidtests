#include <iostream>

int main() {
    float a = -1000.0f;
    int i = 0;
    while (true) {
        i++;
        a += a;
        std::cout << "a: " << a << " i: " << i << "\n";
    }
    return 0;
}
