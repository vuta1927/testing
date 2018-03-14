#!/usr/bin/python
from constants import *
from PyQt5.QtCore import Qt
from PyQt5.QtWidgets import QMainWindow, QDesktopWidget
from views.controls.ServerSettingsControl import ServerSettingsDialog
from views.controls.SplashControl import Splash
import enviroments
import configs
import socket
from threading import Thread
from core.ServerController import ClientThread

class Main(QMainWindow, FORM_MAIN):
    def __init__(self, parent=None):
        super(Main, self).__init__(parent)
        QMainWindow.__init__(self)
        self.setupUi(self)
        self.btnSetting.clicked.connect(self.open_server_settings_dialog)
        self.btnServerStart.clicked.connect(self.btn_server_start_pressed)
        enviroments.init()
        configs.init()

    def btn_server_start_pressed(self):
        tcpServer = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        tcpServer.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        tcpServer.bind((configs.hostAddress, configs.hostPort))
        threads = []
        while True:
            tcpServer.listen(4)
            print("Multithreaded Python server : Waiting for connections from TCP clients...")
            (conn, (ip, port)) = tcpServer.accept()
            newthread = ClientThread(ip, port)
            newthread.start()
            threads.append(newthread)

        for t in threads:
            t.join()


    def open_server_settings_dialog(self):
        server_settings_dialog = ServerSettingsDialog()
        server_settings_dialog.show()
        server_settings_dialog.exec_()

    def setup_config(self):
        splash = Splash(self)
        splash.setWindowFlags(Qt.Window |
                              Qt.CustomizeWindowHint |
                              Qt.WindowTitleHint |
                              Qt.WindowStaysOnTopHint)
        splash.show()
        qtRectangle = splash.frameGeometry()
        centerPoint = QDesktopWidget().availableGeometry().center()
        qtRectangle.moveCenter(centerPoint)
        splash.move(qtRectangle.topLeft())
        splash.exec_()
