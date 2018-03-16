################### IMPORTING MODULES #########################

from PyQt4 import QtCore, QtGui
from socket import *
from threading import Thread
import sys, os
import gui_classes
from Connection_class import *
import subprocess

###############################################################
    
try:
    _fromUtf8 = QtCore.QString.fromUtf8
except AttributeError:
    def _fromUtf8(s):
        return s

try:
    _encoding = QtGui.QApplication.UnicodeUTF8
    def _translate(context, text, disambig):
        return QtGui.QApplication.translate(context, text, disambig, _encoding)
except AttributeError:
    def _translate(context, text, disambig):
        return QtGui.QApplication.translate(context, text, disambig)

##################### MainWindow class ############################

class MainWindow(QtGui.QMainWindow):
    
    def __init__(self):
        super(MainWindow, self).__init__()
        QtGui.QApplication.setStyle(QtGui.QStyleFactory.create('Cleanlooks'))

        ####################### icons #########################

        self.icon_connect = QtGui.QIcon('images\connect.png')
        self.icon_look = QtGui.QIcon('images\look.png')
        self.icon_scan=QtGui.QIcon('images\scan.png')
        self.icon_myName = QtGui.QIcon('images\myName.png')
        self.icon_exit = QtGui.QIcon('images\exit.png')
        self.icon_readMe = QtGui.QIcon('images\readMe.png')
        self.icon_about = QtGui.QIcon("images\about.png")
        self.icon_contact = QtGui.QIcon('images\contact.png')
        self.icon_tutorial = QtGui.QIcon('images\tutorial.png')
             
        self.icon_startChat = QtGui.QIcon('images\startChat.png')
        self.icon_back = QtGui.QIcon('images\back.png')
        self.icon_main = QtGui.QIcon('images\logo.png')

        self.tray_icon = QtGui.QSystemTrayIcon()
        self.tray_icon.setIcon(QtGui.QIcon('images\logo.png'))
        self.tray_icon.activated.connect(self.restore_window)
        

        self.portAdd = 0
        self.connections = []

        ########################### Sub Windows #####################

        self.window_connect = gui_classes.connect_gui(self)
        self.window_look = gui_classes.server_gui(self)
        self.window_scan=gui_classes.scan_gui(self)
        self.window_myName = gui_classes.myName_gui(self)
        self.window_chat = []

        self.setWindowTitle("ChatLAN by CLYT")
        self.setWindowIcon(self.icon_main)
        self.setObjectName(_fromUtf8("MainWindow"))
        self.resize(290, 550)
        self.centralwidget = QtGui.QWidget(self)
        self.centralwidget.setObjectName(_fromUtf8("centralwidget"))
        self.gridLayout_2 = QtGui.QGridLayout(self.centralwidget)
        self.gridLayout_2.setObjectName(_fromUtf8("gridLayout_2"))
        self.horizontalLayout = QtGui.QHBoxLayout()
        self.horizontalLayout.setObjectName(_fromUtf8("horizontalLayout"))
        spacerItem = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.horizontalLayout.addItem(spacerItem)
        self.verticalLayout = QtGui.QVBoxLayout()
        self.verticalLayout.setObjectName(_fromUtf8("verticalLayout"))
        self.gridLayout = QtGui.QGridLayout()
        self.gridLayout.setObjectName(_fromUtf8("gridLayout"))
        
        ######################## start chatting button #####################

        self.btn_startChatting = QtGui.QPushButton(self.centralwidget)
        self.btn_startChatting.setMinimumSize(QtCore.QSize(0, 50))
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setPointSize(14)
        self.btn_startChatting.setFont(font)
        icon = QtGui.QIcon()
        icon.addPixmap(QtGui.QPixmap(_fromUtf8("images/startChat.png")), QtGui.QIcon.Normal, QtGui.QIcon.Off)
        self.btn_startChatting.setIcon(icon)
        self.btn_startChatting.setIconSize(QtCore.QSize(64, 64))
        self.btn_startChatting.setObjectName(_fromUtf8("btn_startChatting"))
        self.btn_startChatting.clicked.connect(self.func_startChatting)
        self.gridLayout.addWidget(self.btn_startChatting, 3, 0, 1, 1)
        
        self.list_connectedFriends = QtGui.QListWidget(self.centralwidget)
        self.list_connectedFriends.setObjectName(_fromUtf8("list_connectedFriends"))
        self.gridLayout.addWidget(self.list_connectedFriends, 1, 0, 1, 1)
        self.lbl_connectedFriends = QtGui.QLabel(self.centralwidget)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Arial"))
        font.setPointSize(10)
        self.lbl_connectedFriends.setFont(font)
        self.lbl_connectedFriends.setObjectName(_fromUtf8("lbl_connectedFriends"))
        self.gridLayout.addWidget(self.lbl_connectedFriends, 0, 0, 1, 1)
        self.verticalLayout.addLayout(self.gridLayout)
        self.gridLayout_4 = QtGui.QGridLayout()
        self.gridLayout_4.setObjectName(_fromUtf8("gridLayout_4"))
        self.lbl_logo = QtGui.QLabel(self.centralwidget)
        self.lbl_logo.setText(_fromUtf8(""))
        self.lbl_logo.setPixmap(QtGui.QPixmap(_fromUtf8("images/logo.png")))
        self.lbl_logo.setObjectName(_fromUtf8("lbl_logo"))
        self.gridLayout_4.addWidget(self.lbl_logo, 0, 1, 1, 1)
        spacerItem1 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout_4.addItem(spacerItem1, 0, 0, 1, 1)
        spacerItem2 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout_4.addItem(spacerItem2, 0, 2, 1, 1)
        self.verticalLayout.addLayout(self.gridLayout_4)
        self.horizontalLayout.addLayout(self.verticalLayout)
        spacerItem3 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.horizontalLayout.addItem(spacerItem3)
        self.gridLayout_2.addLayout(self.horizontalLayout, 0, 0, 1, 1)
        self.setCentralWidget(self.centralwidget)

        ########################## Menu Bar #############################################

        self.menubar = QtGui.QMenuBar(self)
        self.menubar.setGeometry(QtCore.QRect(0, 0, 291, 21))
        self.menubar.setObjectName(_fromUtf8("menubar"))
        self.menu_main = QtGui.QMenu(self.menubar)
        self.menu_main.setObjectName(_fromUtf8("menu_main"))
        self.menu_theme = QtGui.QMenu(self.menubar)
        self.menu_theme.setObjectName(_fromUtf8("menu_theme"))
        self.menu_help = QtGui.QMenu(self.menubar)
        self.menu_help.setObjectName(_fromUtf8("menu_help"))
        self.setMenuBar(self.menubar)
        
        self.statusbar = QtGui.QStatusBar(self)
        self.statusbar.setAutoFillBackground(False)
        self.statusbar.setStyleSheet(_fromUtf8(""))
        self.statusbar.setSizeGripEnabled(True)
        self.statusbar.setObjectName(_fromUtf8("statusbar"))
        self.setStatusBar(self.statusbar)
 
        ####################### Actions #####################################
       
        #action_connect
        self.action_connect = QtGui.QAction(self.icon_connect, '&Connect to a Friend', self)
        self.action_connect.setObjectName(_fromUtf8("action_connect"))
        self.action_connect.setStatusTip('Connect to a friend')
        self.action_connect.setShortcut('Ctrl+F')
        self.action_connect.triggered.connect(self.func_connect)
        
        #action_look
        self.action_look = QtGui.QAction(self.icon_look, '&Look for a Friend', self)
        self.action_look.setObjectName(_fromUtf8("action_look"))
        self.action_look.setStatusTip('Look for a friend')
        self.action_look.setShortcut('Ctrl+L')
        self.action_look.triggered.connect(self.func_look)

        #action_scan
        self.action_scan = QtGui.QAction(self.icon_scan, '&Scan for live Friends', self)
        self.action_scan.setObjectName(_fromUtf8("action_scan"))
        self.action_scan.setStatusTip('Scan live friends')
        self.action_scan.setShortcut('Ctrl+S')
        self.action_scan.triggered.connect(self.func_scan)       
        
        #action_myName
        self.action_myName = QtGui.QAction(self.icon_myName, '&My Username', self)
        self.action_myName.setObjectName(_fromUtf8("action_myName"))
        self.action_myName.setStatusTip('Edit My Username')
        self.action_myName.setShortcut('Ctrl+N')
        self.action_myName.triggered.connect(self.func_myName)
        
        #action_exit
        self.action_exit = QtGui.QAction(self.icon_exit, '&Exit',  self)
        self.action_exit.setObjectName(_fromUtf8("action_exit"))
        self.action_exit.setStatusTip('Exit Application')
        self.action_exit.setShortcut('Ctrl+Q')
        self.action_exit.triggered.connect(self.exitApp)

        #action_motif
        self.action_motif = QtGui.QAction("&Motif", self)
        self.action_motif.setObjectName(_fromUtf8("action_motif"))
        self.action_motif.setStatusTip("Select 'motif' Theme")
        self.action_motif.setShortcut('Ctrl+1')
        self.action_motif.triggered.connect(self.func_motif)
        
        #action_Windows
        self.action_Windows = QtGui.QAction("Windows", self)
        self.action_Windows.setObjectName(_fromUtf8("action_Windows"))
        self.action_Windows.setStatusTip("Select 'Windows' Theme")
        self.action_Windows.setShortcut('Ctrl+2')
        self.action_Windows.triggered.connect(self.func_Windows)
       
        #action_Cleanlooks
        self.action_Cleanlooks = QtGui.QAction("Cleanlooks(default)", self)
        self.action_Cleanlooks.setObjectName(_fromUtf8("action_Cleanlooks"))
        self.action_Cleanlooks.setStatusTip("Select 'Cleanlooks' Theme")
        self.action_Cleanlooks.setShortcut('Ctrl+3')
        self.action_Cleanlooks.triggered.connect(self.func_Cleanlooks)

        #action_readMe
        self.action_readMe = QtGui.QAction(QtGui.QIcon('images\readMe.png'), "&Read Me", self)
        self.action_readMe.setObjectName(_fromUtf8("action_readMe"))
        self.action_readMe.setStatusTip("Open 'Read Me' File")
        self.action_readMe.setShortcut('F1')
        self.action_readMe.triggered.connect(self.func_readMe)
        
        #action_tutorial
        self.action_tutorial = QtGui.QAction(self.icon_tutorial, "&Tutorial", self)
        self.action_tutorial.setObjectName(_fromUtf8("action_tutorial"))
        self.action_tutorial.setStatusTip('Open Online Tutorial')
        self.action_tutorial.setShortcut('Ctrl+F2')
        self.action_tutorial.triggered.connect(self.func_tutorial)
        
        #action_about
        self.action_about = QtGui.QAction(self.icon_about, '&About', self)
        self.action_about.setObjectName(_fromUtf8("action_about"))
        self.action_about.setStatusTip('About this App')
        self.action_about.setShortcut('Ctrl+F1')
        self.action_about.triggered.connect(self.func_about)
        
        #action_contact
        self.action_contact = QtGui.QAction(self.icon_contact, "&Contact", self)
        self.action_contact.setObjectName(_fromUtf8("action_contact"))
        self.action_contact.setStatusTip('Contact Us')
        self.action_contact.setShortcut('Ctrl+M')
        self.action_contact.triggered.connect(self.func_contact)

        ########################### Sub Menus ###########################################
       
        #menubar
        self.menubar.addAction(self.menu_main.menuAction())
        self.menubar.addAction(self.menu_theme.menuAction())
        self.menubar.addAction(self.menu_help.menuAction())

        #menu_main
        self.menu_main.addAction(self.action_connect)
        self.menu_main.addAction(self.action_look)
        self.menu_main.addAction(self.action_scan)
        self.menu_main.addAction(self.action_myName)
        self.menu_main.addSeparator()
        self.menu_main.addAction(self.action_exit)
        
        #menu_theme
        self.menu_theme.addAction(self.action_motif)
        self.menu_theme.addAction(self.action_Windows)
        self.menu_theme.addAction(self.action_Cleanlooks)

        #menu_help
        self.menu_help.addAction(self.action_readMe)
        self.menu_help.addAction(self.action_tutorial)
        self.menu_help.addSeparator()
        self.menu_help.addAction(self.action_about)
        self.menu_help.addAction(self.action_contact)
 
               
        self.btn_startChatting.setText(_translate("MainWindow", "Start Chatting!", None))
        self.lbl_connectedFriends.setText(_translate("MainWindow", "Connected Friends:", None))
     
        self.menu_main.setTitle(_translate("MainWindow", "Main", None))
        self.menu_help.setTitle(_translate("MainWindow", "Help", None))
        self.menu_theme.setTitle(_translate("MainWindow", "Theme", None))
        
        self.connNum = 0
        
        QtCore.QMetaObject.connectSlotsByName(self)        
  
    ######################## Functions ########################
      
    def func_connect(self):
        self.window_connect.showWindow()

    def func_look(self):
        self.window_look.showWindow()

    def func_scan(self):
        self.window_scan.showWindow()

    def func_myName(self):
        self.window_myName.showWindow()
        
    def exitApp(self):
        for item in self.window_chat:
            item.s.close()
        QtGui.qApp.quit()

    def func_motif(self):
        QtGui.QApplication.setStyle(QtGui.QStyleFactory.create('motif'))

    def func_Windows(self):
        QtGui.QApplication.setStyle(QtGui.QStyleFactory.create('Windows'))

    def func_Cleanlooks(self):
        QtGui.QApplication.setStyle(QtGui.QStyleFactory.create('Cleanlooks'))

    def func_readMe(self):
        rm = open('readme.txt', 'r')
        str_readMe = rm.read()
        rm.close()
        msgBox_readMe = QtGui.QMessageBox()
        msgBox_readMe.setIcon(QtGui.QMessageBox.Information)
        msgBox_readMe.setWindowTitle("Read Me")
        msgBox_readMe.setText(str_readMe)
        msgBox_readMe.setWindowIcon(self.icon_main)
        msgBox_readMe.setStandardButtons(QtGui.QMessageBox.Ok)
        msgBox_readMe.exec_()

    def func_tutorial(self):
        str_tutorial = "Please visit http:\\dashdash.com"
        msgBox_tutorial = QtGui.QMessageBox()
        msgBox_tutorial.setIcon(QtGui.QMessageBox.Information)
        msgBox_tutorial.setWindowTitle("Tutorial")
        msgBox_tutorial.setText(str_tutorial)
        msgBox_tutorial.setWindowIcon(self.icon_main)
        msgBox_tutorial.setStandardButtons(QtGui.QMessageBox.Ok)
        msgBox_tutorial.exec_()

    def func_about(self):
        str_about = "This application is created by CLYT\nDevelopers:\nUssama Zahid\nFahad bin Khalid\nhttp:\\codelikeyoutalk.blogspot.com"
        msgBox_about = QtGui.QMessageBox()
        msgBox_about.setIcon(QtGui.QMessageBox.Information)
        msgBox_about.setWindowTitle("About")
        msgBox_about.setText(str_about)
        msgBox_about.setWindowIcon(self.icon_main)
        msgBox_about.setStandardButtons(QtGui.QMessageBox.Ok)
        msgBox_about.exec_()

    def func_contact(self):
        str_fahad = "Fahad bin Khalid\nfkhalid.bee15seecs@seecs.edu.pk"
        str_ussama = "Ussama Zahid\nuzahid.bee15seecs@seecs.edu.pk"
        str_contact = str_fahad + "\n" + str_ussama + "\n" + "School of Electrical Engineering and Computer Science(SEECS)\nNational University of Science and Technology(NUST),\nIslamabad"

        msgBox_contact = QtGui.QMessageBox()
        msgBox_contact.setIcon(QtGui.QMessageBox.Information)
        msgBox_contact.setWindowTitle("Contact")
        msgBox_contact.setText(str_contact)
        msgBox_contact.setWindowIcon(self.icon_main)
        msgBox_contact.setStandardButtons(QtGui.QMessageBox.Ok)
        msgBox_contact.exec_()

    
    def func_startChatting(self):
        selected=0
        for item in self.window_chat:
            if (self.list_connectedFriends.currentItem()):
                selected+=1
                self.tray_icon.show()
                self.hide()
                item.setAttributes()
                item.show()
                if(not item.trd_recv.is_alive()):
                    item.trd_recv.start() 
                break
        if(selected==0):
            str_conn_error = "Please select a valid connection!"
            msgBox_conn_error = QtGui.QMessageBox(self)
            msgBox_conn_error.setIcon(QtGui.QMessageBox.Warning)
            msgBox_conn_error.setWindowTitle("Error")
            msgBox_conn_error.setText(str_conn_error)
            msgBox_conn_error.setWindowIcon(self.icon_main)
            msgBox_conn_error.setStandardButtons(QtGui.QMessageBox.Ok)
            msgBox_conn_error.show()

    def getConnNum(self):
        return self.connNum

    def incrementConnNum(self):
        self.connNum = self.connNum + 1

    def addChatWindow(self):
        self.window_chat.append( gui_classes.chatWindow_gui(self, self.connections[self.getConnNum()]) )
        

    def addConn(self, connection):
        self.connections.append(connection)
        self.connections[self.getConnNum()].startConn()
        self.list_connectedFriends.addItem(self.connections[self.getConnNum()].getFriend())
        #adding a window_chat for this new connection
        self.addChatWindow()
        self.incrementConnNum()


    def addItemInList(self, string):
        self.list_connectedFriends.addItem(QtGui.QListWidgetItem(string, self.list_connectedFriends))

    def ip(self):  
        ipaddress = gethostbyname(gethostname())
        return ipaddress

    def incrementPort(self):
        self.portAdd = self.portAdd + 2
############################ minimizing to tray functionality ############################
    def event(self, event):
        if (event.type() == QtCore.QEvent.WindowStateChange or self.isMinimized()):
            self.setWindowFlags(self.windowFlags() & ~QtCore.Qt.Tool)
            self.tray_icon.show()
            return True
        else:
            return super(MainWindow, self).event(event)

    def closeEvent(self, event):
        reply = QtGui.QMessageBox.question(self,'Message',"Would you like to minimize to the tray?",QtGui.QMessageBox.Yes | QtGui.QMessageBox.No,QtGui.QMessageBox.No)
        if reply == QtGui.QMessageBox.No:
            self.exitApp()
        else:
            self.tray_icon.show()
            self.hide()
            event.ignore()

    def restore_window(self, reason):
        if reason == QtGui.QSystemTrayIcon.DoubleClick:
            self.tray_icon.hide()
            self.showNormal()


################ MAIN PROGRAM ###############
if __name__ == "__main__":
    app = QtGui.QApplication(sys.argv) 
    #window_main
    window_main = MainWindow()
    window_main.show()
    sys.exit(app.exec_())
    

############## MAIN ENDS ####################