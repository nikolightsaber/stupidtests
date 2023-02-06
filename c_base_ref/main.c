#include <stdio.h>
#include <stdint.h>
#include <math.h>

# define M_E		2.7182818284590452354	/* e */
# define M_LOG2E	1.4426950408889634074	/* log_2 e */
# define M_LOG10E	0.43429448190325182765	/* log_10 e */
# define M_LN2		0.69314718055994530942	/* log_e 2 */
# define M_LN10		2.30258509299404568402	/* log_e 10 */
# define M_PI		3.14159265358979323846	/* pi */
# define M_PI_2		1.57079632679489661923	/* pi/2 */
# define M_PI_4		0.78539816339744830962	/* pi/4 */
# define M_1_PI		0.31830988618379067154	/* 1/pi */
# define M_2_PI		0.63661977236758134308	/* 2/pi */
# define M_2_SQRTPI	1.12837916709551257390	/* 2/sqrt(pi) */
# define M_SQRT2	1.41421356237309504880	/* sqrt(2) */
# define M_SQRT1_2	0.70710678118654752440	/* 1/sqrt(2) */

typedef struct ubx_nav_hpposllh_data
{
    uint8_t version;
    uint8_t flags;
    uint32_t iTow;
    int32_t lon;
    int32_t lat;
    int32_t height_mm;
    int32_t h_msl;
    int8_t lon_hp;
    int8_t lat_hp;
    int8_t height_hp;
    int8_t h_msl_hp;
    uint32_t h_acc;
    uint32_t v_acc;
}ubx_nav_hpposllh_data_t;

typedef struct ubx_nav_hpposecef_data
{
    uint8_t version;               //!< uint8_t version      Message version (0x00 for this version)
    uint8_t reserved1[3];          //!< uint8_t reserved1[3] Reserved
    uint32_t iTow;                 //!< uint32_t iTow        GPS time of week of the navigation epoch in ms.
    int32_t ecef_x_cm;             //!< int32_t ecef_x       ECEF X coordinate in cm
    int32_t ecef_y_cm;             //!< int32_t ecef_y       ECEF Y coordinate in cm
    int32_t ecef_z_cm;             //!< int32_t ecef_z       ECEF Z coordinate in cm
    int8_t ecef_x_hp_tenth_of_mm;  //!< int8_t ecef_x_hp     High precision component of ECEF X coordinate in 0.1 mm. Musbe in the range of -99..+99. Precise coordinate in cm = ecefX + (ecefXHp * 1e-2).
    int8_t ecef_y_hp_tenth_of_mm;  //!< int8_t ecef_y_hp     High precision component of ECEF Y coordinate in 0.1 mm. Must be in the range of -99..+99. Precise coordinate in cm = ecefY + (ecefYHp * 1e-2).
    int8_t ecef_z_hp_tenth_of_mm;  //!< int8_t ecef_z_hp     High precision component of ECEF Z coordinate in 0.1 mm. Must be in the range of -99..+99. Precise coordinate in cm = ecefZ + (ecefZHp * 1e-2).
    uint8_t flags;                 //!< uint8_t flags        Additional ﬂags, flags[0]=invalidEcef, if 1 = Invalid ecefX, ecefY, ecefZ, ecefXHp, ecefYHp and ecefZHp
    uint32_t p_acc_hp_tenth_of_mm; //!< uint32_t p_acc       Position Accuracy Estimate in 0.1 mm.
}ubx_nav_hpposecef_data_t;


void zed_llh2ecef(ubx_nav_hpposllh_data_t *llh_data, ubx_nav_hpposecef_data_t *ecef_data)
{
   double lat_rad = (llh_data->lat/1.0e7) * (M_PI / 180.0);
   double lon_rad = (llh_data->lon/1.0e7) * (M_PI / 180.0);

   double a = 6378137.0;
   double finv = 298.257223563;
   double f = 1 / finv;
   double e2 = 1 - (1 - f) * (1 - f);
   double v = a / sqrt(1 - e2 * sin(lat_rad) * sin(lat_rad));

   double height_m = llh_data->height_mm/1.0e3;
   double x = (v + height_m) * cos(lat_rad) * cos(lon_rad);
   double y = (v + height_m) * cos(lat_rad) * sin(lon_rad);
   double z = (v * (1 - e2) + height_m) * sin(lat_rad);

   ecef_data->ecef_x_cm = (int32_t)(100.0*x);
   ecef_data->ecef_y_cm = (int32_t)(100.0*y);
   ecef_data->ecef_z_cm = (int32_t)(100.0*z);
}

ubx_nav_hpposecef_data_t new_llh2ecef_data(int32_t lat, int32_t lon, int32_t height_mm)
{
   ubx_nav_hpposecef_data_t new_ecef_data = {0};
   ubx_nav_hpposllh_data_t llh_data = {0};

   llh_data.lat = lat;
   llh_data.lon = lon;
   llh_data.height_mm = height_mm;

   zed_llh2ecef(&llh_data, &new_ecef_data);

   return new_ecef_data;
}

int main() {
    kkk
}
