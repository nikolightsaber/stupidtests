import utm
import math


if __name__ == '__main__':
    u = utm.from_latlon(50.752301916, 4.618929418)
    ang = 180-80
    xoff = 13.997065923989839 * math.cos(ang * math.pi / 180) - 21.2169874728043 * math.sin(ang * math.pi / 180)
    yoff = 13.997065923989839 * math.sin(ang * math.pi / 180) + 21.2169874728043 * math.cos(ang * math.pi / 180)
    print(xoff, yoff)
    e = (u[0] + xoff, u[1] + yoff, u[2], u[3])
    print(u)
    print(e)
    print(utm.to_latlon(*e))

