import random


def gettryold(val):
    som = 0
    for i in range(512):
        som += val
        if(i > 32 and (som > i * 3072 or som < i * 1024)):
            return som, i, 1
    return som, i, 0

def gettry(val):
    som = 0
    for i in range(512):
        som += val
        if(i > 32 and som - i * 2048 > i * 1024):
            return som, i, 1
        if(i > 32 and som - i * 2048 < i * -1024):
            return som, i, -1
    return som, i, 0


def gettryoptimal(val):
    som = 0
    for i in range(512):
        som += val
        if(i > 32 and som > 32 * 2048):
            return som, i, 1
        if(i > 32 and som < -2048 * 32):
            return som, i, -1
    return som, i, 0


if __name__ == '__main__':
    for i in range(-2048, 2048):
        # som, j = gettryold(i)
        som, j, state = gettryoptimal(i)
        # som, j, state = gettry(i)
        print("input = " + str(i) + " som = " + str(som) + " i = " + str(j) + " state = " + str(state))
