#!/usr/bin/python
import sys
import hashlib
from constants import *
from PyQt5.QtWidgets import QDialog, QMessageBox
from controllers.UserController import *
from models.user import User

class Login(QDialog, FORM_LOGIN):
    def __init__(self, parent=None):
        super(Login, self).__init__(parent)
        self.setupUi(self)
        self.btnLogin.clicked.connect(self.btn_login_pressed)

    def btn_login_pressed(self):
        login_user = User()
        password_text = self.txtPassword.text().encode("ascii")
        password_hash = hashlib.sha512(password_text).hexdigest()
        login_user.username = self.txtUsername.text()
        login_user.password = password_hash
        result = check_authenticate(login_user)
        if result is not False:
            self.hide()
            # self.hide()
        else:
            msg = QMessageBox()
            msg.setIcon(QMessageBox.Critical)
            msg.setText("Login failed, please check your username and password!")
            msg.setWindowTitle("Error")
            msg.show()
            msg.exec_()
