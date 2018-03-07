# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'ServerSettingsDialog.ui'
#
# Created by: PyQt5 UI code generator 5.10.1
#
# WARNING! All changes made in this file will be lost!

from PyQt5 import QtCore, QtGui, QtWidgets

class Ui_ServerSettingDialog(object):
    def setupUi(self, ServerSettingDialog):
        ServerSettingDialog.setObjectName("ServerSettingDialog")
        ServerSettingDialog.resize(247, 134)
        self.widget = QtWidgets.QWidget(ServerSettingDialog)
        self.widget.setGeometry(QtCore.QRect(9, 9, 217, 79))
        self.widget.setObjectName("widget")
        self.verticalLayout = QtWidgets.QVBoxLayout(self.widget)
        self.verticalLayout.setContentsMargins(0, 0, 0, 0)
        self.verticalLayout.setObjectName("verticalLayout")
        self.formLayout = QtWidgets.QFormLayout()
        self.formLayout.setObjectName("formLayout")
        self.label = QtWidgets.QLabel(self.widget)
        self.label.setObjectName("label")
        self.formLayout.setWidget(0, QtWidgets.QFormLayout.LabelRole, self.label)
        self.lineEdit = QtWidgets.QLineEdit(self.widget)
        self.lineEdit.setObjectName("lineEdit")
        self.formLayout.setWidget(0, QtWidgets.QFormLayout.FieldRole, self.lineEdit)
        self.label_2 = QtWidgets.QLabel(self.widget)
        self.label_2.setObjectName("label_2")
        self.formLayout.setWidget(1, QtWidgets.QFormLayout.LabelRole, self.label_2)
        self.lineEdit_2 = QtWidgets.QLineEdit(self.widget)
        self.lineEdit_2.setObjectName("lineEdit_2")
        self.formLayout.setWidget(1, QtWidgets.QFormLayout.FieldRole, self.lineEdit_2)
        self.verticalLayout.addLayout(self.formLayout)
        self.buttonBox = QtWidgets.QDialogButtonBox(self.widget)
        self.buttonBox.setOrientation(QtCore.Qt.Horizontal)
        self.buttonBox.setStandardButtons(QtWidgets.QDialogButtonBox.Cancel|QtWidgets.QDialogButtonBox.Ok)
        self.buttonBox.setObjectName("buttonBox")
        self.verticalLayout.addWidget(self.buttonBox)

        self.retranslateUi(ServerSettingDialog)
        self.buttonBox.accepted.connect(ServerSettingDialog.accept)
        self.buttonBox.rejected.connect(ServerSettingDialog.reject)
        QtCore.QMetaObject.connectSlotsByName(ServerSettingDialog)

    def retranslateUi(self, ServerSettingDialog):
        _translate = QtCore.QCoreApplication.translate
        ServerSettingDialog.setWindowTitle(_translate("ServerSettingDialog", "Settings"))
        self.label.setText(_translate("ServerSettingDialog", "Server Address"))
        self.label_2.setText(_translate("ServerSettingDialog", "Port number"))

# if __name__ == "__main__":
#     import sys
#     app = QtWidgets.QApplication(sys.argv)
#     ServerSettingDialog = QtWidgets.QDialog()
#     ui = Ui_ServerSettingDialog()
#     ui.setupUi(ServerSettingDialog)
#     ServerSettingDialog.show()
#     sys.exit(app.exec_())