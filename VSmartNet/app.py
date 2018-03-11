#!/usr/bin/python
from PyQt5 import QtCore, QtGui, QtWidgets
from views import MainWindowUI
import sys

def main():
    app = QtWidgets.QApplication(sys.argv)
    MainWindow = QtWidgets.QMainWindow()
    ui = MainWindowUI.Ui_MainWindow()
    ui.setupUi(MainWindow)
    MainWindow.show()
    sys.exit(app.exec_())

if __name__ == "__main__": main()
