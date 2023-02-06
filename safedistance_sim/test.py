import matplotlib.pyplot as plt
import matplotlib.animation as animation
import threading
import os
from pynput.keyboard import Key, Listener, KeyCode
from classdef import Point
# import time
# import numpy as np
# ---------------------------------------------------------------------
point = Point()

flags = False
margin = 0.0
# ---------------------------------------------------------------------
# figure for point
figPoint = plt.figure(1)

axisPoint = figPoint.add_subplot(2, 1, 1)
axisBorderDist = figPoint.add_subplot(2, 1, 2)

pointAnim, = axisPoint.plot([0], [0], 'go')
borderDistAnim, = axisBorderDist.plot([0], [0], lw=2)
borderDistAnimMargin, = axisBorderDist.plot([0], [0], 'r--', lw=2)
safedistanceAnim, = axisBorderDist.plot([0], [0], 'g--', lw=2)

pointAnim.set_data(point.x, point.y)
borderDistAnim.set_data(point.frame, point.x)
borderDistAnimMargin.set_data(point.frame, point.x - margin)
safedistanceAnim.set_data(point.frame, point.sd)
# ---------------------------------------------------------------------
selfdrive = True
# ---------------------------------------------------------------------

def initPoint():
    global point
    axisPoint.set_xlim(-10, point.xmax + 10)
    axisPoint.set_ylim(-point.ymax, point.ymax)
    axisPoint.grid(False)

    axisBorderDist.set_xlim(0, point.Imax)
    axisBorderDist.set_ylim(-10, point.xmax + 10)
    axisBorderDist.grid(True)

def drive():
    point.x += point.vx
    point.xmar = point.x - margin
    point.sd -= 1.2
    if (not flags and margin == 0):
        point.sd = point.xmar
    elif (not flags and point.sd < point.xmar):
        point.sd = point.xmar

    pointAnim.set_data(point.x, point.y)
    pIndex = point.fillIndex
    borderDistAnim.set_data(point.T[0: pIndex], point.X[0: pIndex], )
    borderDistAnimMargin.set_data(point.T[0: pIndex], point.Xmar[0: pIndex])
    safedistanceAnim.set_data(point.T[0: pIndex], point.SD[0: pIndex])

    os.system("clear")
    print("border dist", point.x)
    print("border dist margin", point.xmar)
    print("speed", point.vx)
    print("Safedistance", point.sd)
    print("flags", flags)
    print("margin", margin)

def on_release(key):
    global margin
    global flags
    if key == Key.esc:
        plt.close('all')
        print("saving")
        return False
    elif key == Key.right:
        point.vx += 0.1
    elif key == Key.left:
        point.vx -= 0.1
    elif key == Key.down:
        margin -= 0.5
    elif key == Key.up:
        margin += 0.5
    elif key == KeyCode.from_char('j'):
        margin -= 5
    elif key == KeyCode.from_char('k'):
        margin += 5
    elif key == KeyCode.from_char('e'):
        flags = not flags



def keydetect():
    with Listener(on_release=on_release) as listener:
        listener.join()


def animationPoint(frame):
    point.saveData(frame)
    drive()
    return True


def main():
    global selfdrive
    a1 = animation.FuncAnimation(figPoint, animationPoint, frames=None,
                                 init_func=initPoint, interval=100)
    keythread = threading.Thread(target=keydetect, args=[])
    keythread.start()

    try:
        plt.show(block=True)
    except:
        selfdrive = False
        keythread.join()

    selfdrive = False
    keythread.join()


if __name__ == "__main__":
    main()

