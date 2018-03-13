#!/usr/bin/python
from constants import *
from PyQt5 import QtGui
from PyQt5.QtWidgets import QMainWindow
from views.controls.ServerSettingsControl import ServerSettingsDialog
from views.controls.SplashControl import Splash
from core import Database
import enviroments

class Main(QMainWindow, FORM_MAIN):
    def __init__(self, parent=None):
        super(Main, self).__init__(parent)
        QMainWindow.__init__(self)
        self.setupUi(self)
        self.btnSetting.clicked.connect(self.open_server_settings_dialog)
        enviroments.init()

    def open_server_settings_dialog(self):
        server_settings_dialog = ServerSettingsDialog()
        server_settings_dialog.show()
        server_settings_dialog.exec_()

    def setup_config(self):
        splash = Splash(self)
        splash.show()
        splash.exec_()
