#!/usr/bin/python
import sys
from PyQt5.QtWidgets import QApplication
from views.controls import MainControl


def main():
    app = QApplication(sys.argv)
    window = MainControl.Main()
    window.show()
    window.setup_config()
    app.exec_()

if __name__ == "__main__":
    try:
        main()
    except Exception as why:
        print(why)
