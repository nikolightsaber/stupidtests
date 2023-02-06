#include <iostream>
#include <string>
#include <cstdlib>

using namespace std;

enum GPS_QUALITY
{
    GPS_QUALITY_NOPOSITION    = 0,
    GPS_QUALITY_AUTONOMOUS    = 1,
    GPS_QUALITY_DIFFERENTIAL  = 2,
    GPS_QUALITY_RTK_FIXED     = 4,
    GPS_QUALITY_RTK_FLOAT     = 5,
    GPS_QUALITY_DEADRECKONING = 6,
    GPS_QUALITY_MAX           = 7,
    SIMULATION_FIX            = 8
};

enum
{
    TRUST_LEVEL_INVALID   = 0, // Invalid GPS
    TRUST_LEVEL_RETURN    = 1, // Min level to allow GPS return
    TRUST_LEVEL_NORMAL    = 2, // Min level to allow "normal" operations
    TRUST_LEVEL_RTK_FLOAT = 3, // No pattern allowed, but RTK-float still available
    TRUST_LEVEL_PATTERN   = 4, // Min level to allow pattern
    TRUST_LEVEL_NOWIRE    = 5, // Min level to work without wire
    TRUST_LEVEL_TOP       = 6, // Max GPS level (best trust)
    TRUST_LEVEL_NBR       = 7
};

// Table with the values of trust levels
static const float trustCfgLevels[TRUST_LEVEL_NBR] =
    { 0.0,      0.6,        1.0,        1.2,        1.4,        1.6,        2.0 };

// Timeout before cancelling "nowire" level when GPS is in RTK-FLOAT mode
static const float trustCfgTimeoutNoWire = 3.0;
// Timeout before cancelling "pattern" level when GPS is in RTK-FLOAT mode
static const float trustCfgTimeoutPattern = 10.0;

float _lastRtkFixedPosition = 0;
// simulated
float _currentTime = 0;

static const float test_intervals[14] =
    { 0.1, 0.5, 1, 2, 5, 10, 15, 20, 30, 40, 50, 80, 100, 200 };

float ComputeTrustFromQuality(float time_span, int quality)
{
    if (quality == GPS_QUALITY_RTK_FIXED)
        _lastRtkFixedPosition = _currentTime;
    // Saving the last time we get a RTK FIXED position
    float dt = _currentTime - _lastRtkFixedPosition;

    if (quality == GPS_QUALITY_RTK_FIXED)
        return trustCfgLevels[TRUST_LEVEL_TOP];
    else if ((quality == GPS_QUALITY_RTK_FLOAT) && (dt <= trustCfgTimeoutNoWire))
        return trustCfgLevels[TRUST_LEVEL_NOWIRE];

    else if ((quality == GPS_QUALITY_RTK_FLOAT) && (dt <= trustCfgTimeoutPattern))
        return trustCfgLevels[TRUST_LEVEL_PATTERN];

    else if ((quality == GPS_QUALITY_RTK_FLOAT) && (dt > trustCfgTimeoutPattern))
        return trustCfgLevels[TRUST_LEVEL_RTK_FLOAT];

    else if ((quality == GPS_QUALITY_AUTONOMOUS) || (quality == GPS_QUALITY_DIFFERENTIAL))
        return trustCfgLevels[TRUST_LEVEL_NORMAL];
    else
        return trustCfgLevels[TRUST_LEVEL_INVALID];
}

void update

int main() {
    for (int i = 0; i < 14; i++) {
        float time_passed = :quality;
        while (time_passed < test_intervals[i]) {
        }
    }

    return 0;
}
