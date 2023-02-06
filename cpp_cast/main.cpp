#include <cstdint>
#include <iostream>
#include <string>
#include <cstdlib>
#include <math.h>

#define MATH_ABS(x)  ((x)>=0 ? (x) : (-(x)))

int main() {

    int val = 1074383552;
    float a = *((float*)&val);
    float b = *reinterpret_cast<float*>(&val);
    printf("value a %f", a);
    printf("value b %f", b);
    // for (float i = 5.0f; i > -1.0f; i-= 0.1f) {
    //     uint8_t distanceToMargin = static_cast<uint8_t>(MATH_ABS(5.0f - i) * 50.0f);
    //     std::cout << "mar: " << i << "\t\t\t\t uint: " << (int) distanceToMargin << "\t\t\t asfl: "<< static_cast<float>(distanceToMargin) / 50.0f << '\n';
    //     std::cout << MATH_ABS(5.0f - i) * 50.0f << '\n';
    // }
    // std::cout << UINT32_MAX;

    int q = 2147483647;
    printf("value q %f", 0.001f * ((float)q));
    return 0;
}
