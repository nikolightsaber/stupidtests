#include <iostream>
#include <string>
#include <cstdlib>
#include <math.h>

#define MATH_MAX(x,y)  ((x)>(y) ? (x) : (y))
#define MATH_MIN(x,y)  ((x)>(y) ? (y) : (x))
#define SAFE_SPEED 0.2f

float GetGpsBorderTargetSpeed(float borderDistance)
{
    // the robot needs to work at full speed on the GPS border itself
    // reduce the speed linearly
    float margin_dist = 1.09f / 2.0f;
    float delta_dist = MATH_MAX(0.0f, margin_dist + borderDistance);
    float delta_speed = 1.0f - SAFE_SPEED;
    float new_target_speed = SAFE_SPEED + delta_dist * (delta_speed) / margin_dist;
    return new_target_speed = MATH_MIN(1.0f, new_target_speed);
}

int main() {
    for (float i = 10.0f; i > -1.0f; i-= 0.1f) {
        std::cout << "Dist: " << i << "\t\t\t\t Speed: " << GetGpsBorderTargetSpeed(i) << '\n';
    }
    return 0;
}
