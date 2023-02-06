import numpy as np


class Point:
    x = 100
    y = 0
    vx = 0
    xmax = 200
    ymax = 6
    vxmax = 1.0

    xmar = 0
    sd = 100

    fillIndex = 0
    fullBuffer = False
    Imax = 2000

    frame = 0

    T = np.empty(Imax)
    X = np.empty(Imax)
    VX = np.empty(Imax)
    Xmar = np.empty(Imax)
    SD = np.empty(Imax)

    def saveData(self, frame):
        if ((self.fillIndex >= self.Imax - 1) or (self.fullBuffer)):
            self.T = np.roll(self.T, -1)
            self.X = np.roll(self.X, -1)
            self.VX = np.roll(self.VX, -1)
            self.Xmar = np.roll(self.Xmar, -1)
            self.SD = np.roll(self.AY, -1)
            self.fillIndex = self.Imax - 1
            self.fullBuffer = True
        else:
            self.fillIndex += 1

        self.frame = frame
        self.T[self.fillIndex] = frame
        self.X[self.fillIndex] = self.x
        self.VX[self.fillIndex] = self.vx
        self.Xmar[self.fillIndex] = self.xmar
        self.SD[self.fillIndex] = self.sd
