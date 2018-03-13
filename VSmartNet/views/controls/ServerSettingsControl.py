#!/usr/bin/python
from constants import *
from PyQt5.QtWidgets import QDialog

class ServerSettingsDialog(QDialog, FORM_CONFIG):
    def __init__(self, parent=None):
        super(ServerSettingsDialog, self).__init__(parent)
        self.setupUi(self)
