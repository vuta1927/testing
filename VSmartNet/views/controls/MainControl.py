#!/usr/bin/python
from constants import *
from PyQt5.QtCore import Qt
from PyQt5.QtWidgets import QMainWindow, QDesktopWidget
from views.controls.ServerSettingsControl import ServerSettingsDialog
from views.controls.SplashControl import Splash
import enviroments
import configs
class Main(QMainWindow, FORM_MAIN):
    def __init__(self, parent=None):
        super(Main, self).__init__(parent)
        QMainWindow.__init__(self)
        self.setupUi(self)
        self.btnSetting.clicked.connect(self.open_server_settings_dialog)
        enviroments.init()
        configs.init()

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
