#!/usr/bin/python
from PyQt5 import QtCore, QtGui, QtWidgets
from utily import DatabaseUtility

databaseControl = DatabaseUtility.DatabaseUtility()
databaseControl.InitTables()