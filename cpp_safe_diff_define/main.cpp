#include <iostream>
#define SAFE_DIFF(a, b) ((b >= a) ? (b - a) : (UINT32_MAX - a + b + 1))

int main() {
    unsigned int start_val = UINT32_MAX - 5;
    for (unsigned int i = start_val; i < 5 || i > 10; i++) {
        std::cout << "val: " << i << " diff: " << SAFE_DIFF(start_val, i) << "\n";
    }
    return 0;
}
