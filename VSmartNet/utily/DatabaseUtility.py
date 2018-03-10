#!/usr/bin/python
import sys
from PyQt5.QtWidgets import QMessageBox
from PyQt5.QtGui import QIcon
from PyQt5.QtCore import pyqtSlot
import sqlite3
from configs import database, sqlscriptPath
import uuid
from models import user

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
            self.cur = self.conn.cursor()
        except sqlite3.Error as e:
            print("[-] An Error occurred " + e.args[0])

    def InitTables(self):
        try:
            file = open(sqlscriptPath)
            script = file.read()
            if (script != None):
                try:
                    self.cur.executescript(script)
                    self.___InitData___()
                    return True
                except sqlite3.Error as err:
                    print("[-] Sqlite3 An Error occurred " + err.args[0])
                    return None
        except IOError as err:
            print("[-] An I/O Error occurred " + err.args[1])
            return None

    def ___InitData___(self):
        try:
            admin = self.GetUser(self.username)
            if(admin.id != None):
                return

            self.cur.execute('INSERT INTO user (username,password,fullname) VALUES(?,?,?)',(self.username, self.password, self.fullname))
            self.conn.commit()
            return True
        except sqlite3.Error as err:
            print("An Error occurred " + e.args[0])
            return False

    def GetUser(self, username):
        try:
            self.cur.execute('SELECT id, username, password, fullname FROM user WHERE username = ?', username)
            result = self.cur.fetchone()
            userobj = user.User(result[0], result[1], result[2], result[3])
            return userobj
        except sqlite3.Error as err:
            return None