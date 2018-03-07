# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file '.\LoginDialog.ui'
#
# Created by: PyQt5 UI code generator 5.10.1
#
# WARNING! All changes made in this file will be lost!

from PyQt5 import QtCore, QtGui, QtWidgets


class Ui_LoginDialog(object):
    def setupUi(self, LoginDialog):
        LoginDialog.setObjectName("LoginDialog")
        LoginDialog.resize(235, 173)
        self.gridLayout = QtWidgets.QGridLayout(LoginDialog)
        self.gridLayout.setObjectName("gridLayout")
        self.widget = QtWidgets.QWidget(LoginDialog)
        self.widget.setObjectName("widget")
        self.gridLayout_2 = QtWidgets.QGridLayout(self.widget)
        self.gridLayout_2.setObjectName("gridLayout_2")
        self.label_2 = QtWidgets.QLabel(self.widget)
        self.label_2.setObjectName("label_2")
        self.gridLayout_2.addWidget(self.label_2, 2, 0, 1, 1)
        self.label = QtWidgets.QLabel(self.widget)
        self.label.setObjectName("label")
        self.gridLayout_2.addWidget(self.label, 0, 0, 1, 1)
        self.txtPassword = QtWidgets.QLineEdit(self.widget)
        self.txtPassword.setEchoMode(QtWidgets.QLineEdit.Password)
        self.txtPassword.setClearButtonEnabled(False)
        self.txtPassword.setObjectName("txtPassword")
        self.gridLayout_2.addWidget(self.txtPassword, 3, 0, 1, 1)
        self.txtUsername = QtWidgets.QLineEdit(self.widget)
        self.txtUsername.setObjectName("txtUsername")
        self.gridLayout_2.addWidget(self.txtUsername, 1, 0, 1, 1)
        self.horizontalLayout = QtWidgets.QHBoxLayout()
        self.horizontalLayout.setObjectName("horizontalLayout")
        self.btnLogin = QtWidgets.QPushButton(self.widget)
        icon = QtGui.QIcon()
        icon.addPixmap(QtGui.QPixmap(":/icon/icon/login-24.png"),
                       QtGui.QIcon.Normal, QtGui.QIcon.Off)
        self.btnLogin.setIcon(icon)
        self.btnLogin.setObjectName("btnLogin")
        self.horizontalLayout.addWidget(self.btnLogin)
        self.btnExit = QtWidgets.QPushButton(self.widget)
        icon1 = QtGui.QIcon()
        icon1.addPixmap(QtGui.QPixmap(
            ":/icon/icon/150_-_Cancel-20.png"), QtGui.QIcon.Normal, QtGui.QIcon.Off)
        self.btnExit.setIcon(icon1)
        self.btnExit.setObjectName("btnExit")
        self.horizontalLayout.addWidget(self.btnExit)
        self.gridLayout_2.addLayout(self.horizontalLayout, 6, 0, 1, 1)
        spacerItem = QtWidgets.QSpacerItem(
            20, 40, QtWidgets.QSizePolicy.Minimum, QtWidgets.QSizePolicy.Expanding)
        self.gridLayout_2.addItem(spacerItem, 5, 0, 1, 1)
        self.lblMessage = QtWidgets.QLabel(self.widget)
        self.lblMessage.setEnabled(True)
        self.lblMessage.setStyleSheet("color: rgb(255, 0, 0);")
        self.lblMessage.setObjectName("lblMessage")
        self.lblMessage.hide()
        self.gridLayout_2.addWidget(self.lblMessage, 4, 0, 1, 1)
        self.gridLayout.addWidget(self.widget, 0, 0, 1, 1)

        self.retranslateUi(LoginDialog)
        QtCore.QMetaObject.connectSlotsByName(LoginDialog)

    def retranslateUi(self, LoginDialog):
        _translate = QtCore.QCoreApplication.translate
        LoginDialog.setWindowTitle(_translate(
            "LoginDialog", "VCenter Monitor"))
        self.label_2.setText(_translate("LoginDialog", "Password"))
        self.label.setText(_translate("LoginDialog", "Username"))
        self.btnLogin.setText(_translate("LoginDialog", "Login"))
        self.btnExit.setText(_translate("LoginDialog", "Exit"))
        self.lblMessage.setText(_translate("LoginDialog", "TextLabel"))


import resource_rc

# if __name__ == "__main__":
#     import sys
#     app = QtWidgets.QApplication(sys.argv)
#     LoginDialog = QtWidgets.QDialog()
#     ui = Ui_LoginDialog()
#     ui.setupUi(LoginDialog)
#     LoginDialog.show()
#     sys.exit(app.exec_())