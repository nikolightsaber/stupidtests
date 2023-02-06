import math
import utm
import numpy as np

def getxy(coord):
    u = utm.from_latlon(coord[0], coord[1])
    return [u[0], u[1]]

if __name__ == '__main__':

    origin = [50.75177897121533, 4.617557409072036]

    map = [[50.75177015292349, 4.617563544841035],
             [50.75164007043928, 4.618594682145757],
             [50.75267257024916, 4.61897355765692],
             [50.75280163657032, 4.61785495679084]]

    grass = [[50.75181071189712, 4.617676415243594],
             [50.75170931709671, 4.618513264423211],
             [50.752695387065074, 4.617944924070083],
             [50.75259540774676, 4.618779349076338]]

    building = [[50.751979121235735, 4.6187249282836484],
                [50.7519646969362, 4.618838922160962],
                [50.752299848630265, 4.618826852221011],
                [50.75228627291207, 4.61893548168057]]

    originxy = getxy(origin)

    print("map")
    for m in map:
        out = (np.array(originxy) - np.array(getxy(m))) * -1
        print("""X="{}" Y="{}" """.format(out[0], out[1]))

    print("grass")
    for g in grass:
        out = (np.array(originxy) - np.array(getxy(g))) * -1
        print("""X="{}" Y="{}" """.format(out[0], out[1]))

    print("obst")
    for b in building:
        out = (np.array(originxy) - np.array(getxy(b))) * -1
        print("""X="{}" Y="{}" """.format(out[0], out[1]))
