#include <stdio.h>
#include <stdlib.h>

enum NoWireStatusFlags
{
    GnssTimeout = 0x1,
    NoCfgSignature = 0x2,
    RtkStationSignatureTimeout = 0x4,
    RtkStationCorrectionTimeout = 0x8,
    RtkStationSignatureNotEqual = 0x10,
    BorderSignatureTimeout = 0x20,
    BorderPairedSignatureTimeout = 0x40,
    BorderSignatureNotEqual = 0x80,
    BorderPairedSignatureNotEqual = 0x100,
    BorderSignatureInvalid = 0x200,
    BorderPairedSignatureInvalid = 0x400,
    BorderDistanceNotEqualWork = 0x800,
    BorderDistanceNotEqualNogo = 0x1000,
    BorderDistanceWorkTimeout = 0x2000,
    BorderPairedDistanceWorkTimeout = 0x4000,
    BorderDistanceNogoTimeout = 0x8000,
    BorderPairedDistanceNogoTimeout = 0x10000,
    GpsQualityLow = 0x20000,
};

const char* getName(enum NoWireStatusFlags status) {
    switch (status) {
        case GnssTimeout: return "GnssTimeout";
        case NoCfgSignature: return "NoCfgSignature";
        case RtkStationSignatureTimeout: return "RtkStationSignatureTimeout";
        case RtkStationCorrectionTimeout: return "RtkStationCorrectionTimeout";
        case RtkStationSignatureNotEqual: return "RtkStationSignatureNotEqual";
        case BorderSignatureTimeout: return "BorderSignatureTimeout";
        case BorderPairedSignatureTimeout: return "BorderPairedSignatureTimeout";
        case BorderSignatureNotEqual: return "BorderSignatureNotEqual";
        case BorderPairedSignatureNotEqual: return "BorderPairedSignatureNotEqual";
        case BorderSignatureInvalid: return "BorderSignatureInvalid";
        case BorderPairedSignatureInvalid: return "BorderPairedSignatureInvalid";
        case BorderDistanceNotEqualWork: return "BorderDistanceNotEqualWork";
        case BorderDistanceNotEqualNogo: return "BorderDistanceNotEqualNogo";
        case BorderDistanceWorkTimeout: return "BorderDistanceWorkTimeout";
        case BorderPairedDistanceWorkTimeout: return "BorderPairedDistanceWorkTimeout";
        case BorderDistanceNogoTimeout: return "BorderDistanceNogoTimeout";
        case BorderPairedDistanceNogoTimeout: return "BorderPairedDistanceNogoTimeout";
        case GpsQualityLow: return "GpsQualityLow";
    }
    return "";
}

const char* getQuality(int quality) {
    switch (quality) {
        case 0: return "NO_FIX";
        case 1: return "GPS_FIX";
        case 2: return "DGPS_FIX";
        case 3: return "PPS_FIX";
        case 4: return "RTK_FIX";
        case 5: return "FLOAT_RTK_FIX";
    }
    return "Error";
}

int main(int argc, char *argv[]) {

    if (argc < 2) {
        return -1;
    }

    int userdata = atoi(argv[1]);
    int nowire_safety_status = (int) (userdata & 0x000FFFFF);

    printf("Status:\r\n");

    for (int i=GnssTimeout; i<GpsQualityLow; i++) {
        if (nowire_safety_status & i) {
            printf("%s\r\n", getName(i));
        }
    }

    printf("Closest Border: %s\r\n",((userdata >> 20) & 0x00000001) == 0 ? "Work" : "NoGo");
    printf("Quality: %s\r\n",getQuality((int) ((userdata >> 21) & 0x00000007)));
    float safedistance_distance_to_margin =  ((float)((userdata >> 24) & 0x000000FF)) / 50.0f;
    printf("Distance To Margin: %f\r\n", safedistance_distance_to_margin);
    return 0;
}
