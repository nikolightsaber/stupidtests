import sys

def add_front(out, i):
    for j in range(0, i):
        out += "{0}:".format(time_split[j])
    return out

def add_end(out, i):
    for j in range(i, 2):
        out += ":[0-9]{2}"
    out += "|"
    return out


if __name__ == '__main__':
    time = sys.argv[1]
    time_split = time.split(":")
    if len(time_split) != 3:
        print("Bad time")
        exit

    out = ""

    for i, t in enumerate(time_split):
        if len(t) != 2:
            print("bad time")
            exit

        next = int(t[0]) + 1
        if next < 10:
            out = add_front(out, i)
            out += "[{0}-9][0-9]".format(next)
            out = add_end(out, i)

        next = int(t[1]) + 1
        if next < 10:
            out = add_front(out, i)
            out += "{0}[{1}-9]".format(t[0], next)
            out = add_end(out, i)


    out += time
    print(out)
