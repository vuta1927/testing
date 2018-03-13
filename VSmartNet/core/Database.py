#!/usr/bin/python
import sys
from PyQt5.QtCore import QThread, pyqtSignal
import pymysql.cursors
from models import user
import uuid
import time
class SqlDatabase(QThread):
    mysignal = pyqtSignal(int, bool, str)
    def __init__(self, parent=None):
        QThread.__init__(self, parent)
        self.db = 'smartnet'
        self.defaultUserName = 'admin'
        self.defaultUserPassword = 'c7ad44cbad762a5da0a452f9e854fdc1e0e7a52a38015f23f3eab1d80b931dd472634dfac71cd34ebc35d16ab7fb8a90c81f975113d6c7538dc69dd8de9077ec'
        self.defaultUserFullname = 'admin'
        self.conn = None
        # sys.exit(msg.exec_())

    def run(self):
        time.sleep(0.1)
        self.mysignal.emit(5, False, 'Connecting to database ...')
        try:
            self.conn = pymysql.connect(host='localhost', user='root', password='Echo@1927', db=self.db, charset='utf8mb4', cursorclass=pymysql.cursors.DictCursor)
            time.sleep(1)
        except Exception as e:
            # print(e)
            self.mysignal.emit(5, True, 'Cant not connect to database !')
        if(self.conn != None):
            self.cur = self.conn.cursor()
            self.___InitData___()

    def ___InitData___(self):
        self.mysignal.emit(20, False, 'Connected to database ...')
        time.sleep(0.3)
        defaultUser = self.getUser(self.defaultUserName)
        if(defaultUser != None):
            time.sleep(0.3)
            self.mysignal.emit(100, False, '')
            # print("Setup database finished!")
            return
        
        try:
            time.sleep(0.3)
            sql = 'INSERT INTO users (id, username, password, fullname) VALUES(%s, %s, %s, %s)'
            self.cur.execute(sql, (str(uuid.uuid4()), self.defaultUserName, self.defaultUserPassword, self.defaultUserFullname))
            self.conn.commit()
            self.mysignal.emit(70, False, 'initial database ...')
        except mysql.connector.Error as err:
            self.mysignal.emit(70, True, "Something went wrong: {}".format(err))
            # print("Something went wrong: {}".format(err))
        
        time.sleep(0.3)
        self.mysignal.emit(100, False, 'Database initialled')
        # print("Setup database finished!")

    def getUser(self, username):
        try:
            time.sleep(0.3)
            self.mysignal.emit(50, False, '')
            self.cur.execute('SELECT * FROM users WHERE username = %s', username)
        except mysql.connector.Error as err:
            self.mysignal.emit(50, True, "Something went wrong: {}".format(err))
            # print("Something went wrong: {}".format(err))
        result = self.cur.fetchone()
        if(result != None):
            return user.User(result['id'], result['username'], result['password'], result['fullname'])
        else:
            return