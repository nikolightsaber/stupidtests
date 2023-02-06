#include <iostream>
#include <string>
#include <cstdlib>

using namespace std;

class Point
{
    public:
        double x = 0.0;
        double y = 0.0;
};

static int getProjectionZone(Point pose)
{
    //Get UTM Zone
    int zoneNumber;
    if (((int)pose.y) < 180)
        zoneNumber = (int)(31 + (pose.y / 6.0));
    else
        zoneNumber = (int)((pose.y / 6.0) - 29);

    if (zoneNumber > 60)
        zoneNumber = 1;

    /* UTM special cases */
    int Lat_Degrees = (int)pose.x;
    int Long_Degrees = (int)pose.y;
    if ((Lat_Degrees > 55) && (Lat_Degrees < 64) && (Long_Degrees > -1)
        && (Long_Degrees < 3))
        zoneNumber = 31;
    if ((Lat_Degrees > 55) && (Lat_Degrees < 64) && (Long_Degrees > 2)
        && (Long_Degrees < 12))
        zoneNumber = 32;
    if ((Lat_Degrees > 71) && (Long_Degrees > -1) && (Long_Degrees < 9))
        zoneNumber = 31;
    if ((Lat_Degrees > 71) && (Long_Degrees > 8) && (Long_Degrees < 21))
        zoneNumber = 33;
    if ((Lat_Degrees > 71) && (Long_Degrees > 20) && (Long_Degrees < 33))
        zoneNumber = 35;
    if ((Lat_Degrees > 71) && (Long_Degrees > 32) && (Long_Degrees < 42))
        zoneNumber = 37;

    if(pose.x < 0)
        zoneNumber *= -1;
    return zoneNumber;
}

static bool testZone(std::string zone, std::string zone_calc)
{
    if (zone == zone_calc)
        return true;

    if(zone.length() != 3)
        return false;
    return true;
}

int main() {
    int zone = 30;
    Point p;
    p.x = 50.7186411;
    p.y = 4.59693449;
    int zone_calc = getProjectionZone(p);

    if (zone <= zone_calc - 2 || zone_calc + 2 <= zone)
    {
        cout << "zone to far from calcualted zone: zone_calc: " << zone_calc << zone;
    }
    else
    {

        std::string zone_str = zone < 0 ? std::to_string(zone * -1) + "Z" :
                                          std::to_string(zone) + "N";
        cout << zone_str;
    }

    return 0;
}
