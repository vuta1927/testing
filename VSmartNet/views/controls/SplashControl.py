#!/usr/bin/python
import sys
from constants import *
from PyQt5.QtWidgets import QDialog, QMessageBox
from PyQt5.QtCore import pyqtSlot
from core import Database
from views.controls.LoginControl import Login
import enviroments

class Splash(QDialog, FORM_SPLASH):
    def __init__(self, parent=None):
        super(Splash, self).__init__(parent)
        self.setupUi(self)
        enviroments.dBContext = Database.SqlDatabase(self)
        enviroments.dBContext.mysignal.connect(self.progress)
        enviroments.dBContext.start()

    @pyqtSlot(int, bool, str)
    def progress(self, i, error, message):
        if not error:
            self.progressBar.setValue(i)
            if i == 100:
                self.hide()
                self.is_login()
                # login = Login()
                # login.show()
                # login.exec_()
            if message is not '':
                self.lblLoading.setText(message)
        else:
            self.hide()
            msg = QMessageBox()
            msg.setIcon(QMessageBox.Critical)
            msg.setText(message)
            msg.setWindowTitle("Error")
            msg.show()
            sys.exit(msg.exec_())

    def is_login(self):
        if enviroments.isLogin:
            pass
        else:
            login = Login(self)
            login.show()
            login.exec_()
