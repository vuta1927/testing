#!/usr/bin/python
import sys
from PyQt5.QtWidgets import QMessageBox
from PyQt5.QtGui import QIcon
from PyQt5.QtCore import pyqtSlot
import sqlite3
from configs import database, sqlscriptPath
import uuid

sqlite3.register_converter('GUID', lambda b: uuid.UUID(bytes_le=b))
sqlite3.register_adapter(uuid.UUID, lambda u: buffer(u.bytes_le))


class DatabaseUtility:
    def __init__(self):
        self.db = database
        self.username = 'admin'
        self.password = '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918'
        self.fullname = 'admin'

        try:
            self.conn = sqlite3.connect(self.db)
            self.cur = conn.cursor()
            return self
        except sqlite3.Error as e:
            QMessageBox.critical(None, "An Error occurred",
                                 e.args[0], QMessageBox.Cancel)
            return None

    def InitTables(self):
        try:
            file = open(sqlscriptPath)
            script = file.read()
            if (script != None):
                try:
                    self.cur.executescript(script)
                    return True
                except sqlite3.Error as err:
                    QMessageBox.critical(
                        None, "An Error occurred", e.args[0], QMessageBox.Cancel)
                    return None
        except IOError as (errno, strerro):
            QMessageBox.critical(None, "An I/O Error occurred",
                                 format(errno, strerror), QMessageBox.Cancel)
            return None

    def InitData(self):
        try:
            admin = self.GetUser(self.username)
            if(admin != None or len(admin) > 0):
                return

            self.cur.execute('INSERT INTO user (username,password,fullname) VALUES(?,?,?)', [
                             (self.username, self.password, self.fullname)])

        except sqlite3.Error as err:
            pass

    def GetUser(self, username):
        try:
            self.cur.execute('SELECT id FROM user WHERE username = ?', username)
            result = self.cur.fetchone()
            return result
        except sqlite3.Error as err:
            return None