#!/usr/bin/python
from constants import *
from PyQt5.QtWidgets import QDialog, QApplication
import configs
import json
import enviroments

class ServerSettingsDialog(QDialog, FORM_CONFIG):
    def __init__(self, parent=None):
        super(ServerSettingsDialog, self).__init__(parent)
        self.setupUi(self)
        self.txtServerAddress.setText(configs.hostAddress)
        self.txtPortNumber.setText(configs.hostPort)
        self.btnSave.clicked.connect(self.btn_save_pressed)
        self.btnCancel.clicked.connect(self.btn_cancel_pressed)

    def btn_cancel_pressed(self):
        self.hide()

    def btn_save_pressed(self):
        configs.hostAddress = self.txtServerAddress.text()
        configs.hostPort = self.txtPortNumber.text()
        with open('config.json', 'w') as outfile:
            json.dump({'HostAddress':configs.hostAddress, 'HostPort':configs.hostPort}, outfile)
        self.hide()
