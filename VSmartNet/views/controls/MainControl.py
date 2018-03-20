#!/usr/bin/python
from constants import *
from PyQt5.QtCore import Qt, pyqtSlot
from PyQt5.QtWidgets import QMainWindow, QDesktopWidget
from views.controls.ServerSettingsControl import ServerSettingsDialog
from views.controls.SplashControl import Splash
import enviroments
import configs
from core.ServerController import ServerThread

class Main(QMainWindow, FORM_MAIN):
    def __init__(self, parent=None):
        super(Main, self).__init__(parent)
        QMainWindow.__init__(self)
        self.setupUi(self)
        self.btnSetting.clicked.connect(self.open_server_settings_dialog)
        self.btnServerStart.clicked.connect(self.btn_server_start_pressed)
        self.btnServerStop.clicked.connect(self.btn_server_stop_pressed)
        self.btnServerStop.setEnabled(False)
        enviroments.init()
        configs.init()

    def btn_server_start_pressed(self):
        self.btnServerStart.setEnabled(False)
        self.btnServerStop.setEnabled(True)
        enviroments.server = ServerThread(self)
        enviroments.server.response_signal.connect(self.process_response_signal)
        enviroments.server.start()
        self.statusBar().showMessage("Server started! Waiting for connections from TCP clients...")

    def btn_server_stop_pressed(self):
        self.btnServerStart.setEnabled(True)
        self.btnServerStop.setEnabled(False)
        enviroments.server.stop()
        self.statusBar().showMessage('Server shutdown!')

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
        qt_rectangle = splash.frameGeometry()
        center_point = QDesktopWidget().availableGeometry().center()
        qt_rectangle.moveCenter(center_point)
        splash.move(qt_rectangle.topLeft())
        splash.exec_()

    @pyqtSlot(int, bool, object)
    def process_response_signal(self, command_type, status, message):
        print(command_type)
