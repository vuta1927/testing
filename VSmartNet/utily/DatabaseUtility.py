#!/usr/bin/python
import sys
from PyQt5.QtWidgets import QMessageBox
from PyQt5.QtGui import QIcon
from PyQt5.QtCore import pyqtSlot
import sqlite3
from configs import database

class DatabaseUtility:
    def __init__(self):
        self.db = database
        try:
            conn = sqlite3.connect(self.db)
            cur = conn.cursor()
            return cur
        except sqlite3.Error as e:
            QMessageBox.critical(None, "Cannot open database", "Unable to establish a database connection.\nClick Cancel to exit.", QMessageBox.Cancel)
            return None