from main import *
from PyQt4 import QtCore, QtGui
from socket import *
import subprocess
import ipaddress
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

def ip():  
    ipaddress = gethostbyname(gethostname())
    return ipaddress



######################class server_gui for look window#################################
class server_gui(QtGui.QWidget):
    def __init__(self, window_main):
        super(server_gui, self).__init__()
        self.setObjectName("window_look")
        self.resize(400, 300)
        self.window_main = window_main
        font = QtGui.QFont()
        font.setPointSize(10)
        font.setBold(True)
        font.setUnderline(False)
        font.setWeight(75)
        self.setFont(font)
        self.gridLayout = QtGui.QGridLayout(self)
        self.gridLayout.setObjectName(_fromUtf8("gridLayout"))
        self.ip_here = QtGui.QLabel(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Times New Roman"))
        font.setUnderline(True)
        self.ip_here.setFont(font)
        self.ip_here.setObjectName(_fromUtf8("ip_here"))
        self.gridLayout.addWidget(self.ip_here, 4, 2, 1, 2)
        self.ip_label = QtGui.QLabel(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setPointSize(10)
        font.setBold(False)
        font.setWeight(50)
        self.ip_label.setFont(font)
        self.ip_label.setObjectName(_fromUtf8("ip_label"))
        self.gridLayout.addWidget(self.ip_label, 4, 1, 1, 1)
        self.enter = QtGui.QPushButton(self)
        self.enter.clicked.connect(self.func_entered)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setBold(False)
        font.setWeight(50)
        self.enter.setFont(font)
        self.enter.setObjectName(_fromUtf8("enter"))
        self.gridLayout.addWidget(self.enter, 6, 5, 1, 1)
        self.back = QtGui.QPushButton(self)
        self.back.clicked.connect(self.exit)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setBold(False)
        font.setWeight(50)
        self.back.setFont(font)
        self.back.setObjectName(_fromUtf8("back"))
        self.gridLayout.addWidget(self.back, 11, 5, 1, 1)
        spacerItem = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem, 12, 1, 1, 5)
        spacerItem1 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem1, 8, 1, 1, 5)
        spacerItem2 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem2, 5, 1, 1, 5)
        spacerItem3 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem3, 2, 1, 1, 5)
        spacerItem4 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem4, 0, 1, 1, 5)
        spacerItem5 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem5, 1, 5, 1, 2)
        spacerItem6 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem6, 6, 6, 1, 1)
        self.Look = QtGui.QLabel(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setPointSize(15)
        font.setBold(True)
        font.setWeight(75)
        self.Look.setFont(font)
        self.Look.setObjectName(_fromUtf8("Look"))
        self.gridLayout.addWidget(self.Look, 1, 2, 1, 3)
        spacerItem7 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem7, 11, 6, 1, 1)
        spacerItem8 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem8, 4, 4, 1, 3)
        spacerItem9 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem9, 4, 0, 1, 1)
        spacerItem10 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem10, 6, 0, 1, 1)
        spacerItem11 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem11, 11, 0, 1, 5)
        spacerItem12 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem12, 1, 0, 1, 2)
        self.name_label = QtGui.QLabel(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setBold(False)
        font.setWeight(50)
        self.name_label.setFont(font)
        self.name_label.setObjectName(_fromUtf8("name_label"))
        self.gridLayout.addWidget(self.name_label, 6, 1, 1, 1)
        self.name_here = QtGui.QLineEdit(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        self.name_here.setFont(font)
        self.name_here.setObjectName(_fromUtf8("name_here"))
        self.gridLayout.addWidget(self.name_here, 6, 2, 1, 3)

        self.connNum = 0

        self.setWindowTitle(_translate("window_look", "Look for Friend", None))
        self.setWindowIcon(QtGui.QIcon('images\look.png'))
        self.ip_here.setText(_translate("window_look", self.ip(), None))
        self.ip_label.setText(_translate("window_look", "Your IP is: ", None))
        self.enter.setText(_translate("window_look", "Enter", None))
        self.back.setText(_translate("window_look", "Back", None))
        self.Look.setText(_translate("window_look", "Look For Friend", None))
        self.name_label.setText(_translate("window_look", "Enter Your Name: ", None))

        QtCore.QMetaObject.connectSlotsByName(self)

    def ip(self):  
        ipaddress = gethostbyname(gethostname())
        return ipaddress

    def setMainWindow(self, window_main):
        self.window_main = window_main


    def exit(self):
        self.close()
        

    def func_entered(self):
        self.a = self.name_here.text()
        self.f = open("name.fus", 'w')
        self.f.write(self.a)
        self.f.close()
        self.close()
        #adding a server type connection to window_main
        self.window_main.addConn(Connection("server", self.window_main))

    def showWindow(self):
        self.show()

           
############################class connect_gui for connect window############################
class connect_gui(QtGui.QWidget):
    def __init__(self, window_main):
        super(connect_gui, self).__init__()
        self.setObjectName(_fromUtf8("window_connect"))
        self.resize(400, 300)
        self.window_main = window_main
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Adobe Garamond Pro"))
        font.setPointSize(10)
        font.setBold(False)
        font.setWeight(50)
        self.setFont(font)
        self.gridLayout = QtGui.QGridLayout(self)
        self.gridLayout.setObjectName(_fromUtf8("gridLayout"))
        spacerItem = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem, 2, 3, 1, 3)
        spacerItem1 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem1, 2, 0, 1, 2)
        spacerItem2 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem2, 0, 0, 1, 6)
        spacerItem3 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem3, 4, 0, 1, 1)
        spacerItem4 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem4, 3, 1, 1, 3)
        self.connect = QtGui.QPushButton(self)
        self.connect.clicked.connect(self.func_entered)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setPointSize(8)
        self.connect.setFont(font)
        self.connect.setObjectName(_fromUtf8("connect"))
        self.gridLayout.addWidget(self.connect, 4, 3, 1, 1)
        self.enterip = QtGui.QLabel(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setBold(True)
        font.setWeight(75)
        self.enterip.setFont(font)
        self.enterip.setObjectName(_fromUtf8("enterip"))
        self.gridLayout.addWidget(self.enterip, 4, 1, 1, 1)
        self.ip_here = QtGui.QLineEdit(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Times New Roman"))
        self.ip_here.setFont(font)
        self.ip_here.setText(_fromUtf8(""))
        self.ip_here.setObjectName(_fromUtf8("ip_here"))
        self.gridLayout.addWidget(self.ip_here, 4, 2, 1, 1)
        self.back = QtGui.QPushButton(self)
        self.back.clicked.connect(self.exit)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setBold(False)
        font.setWeight(50)
        self.back.setFont(font)
        self.back.setObjectName(_fromUtf8("back"))
        

        self.gridLayout.addWidget(self.back, 6, 3, 1, 1)
        spacerItem5 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem5, 7, 1, 1, 3)
        self.label = QtGui.QLabel(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setPointSize(15)
        font.setBold(True)
        font.setItalic(False)
        font.setUnderline(True)
        font.setWeight(75)
        font.setStrikeOut(False)
        self.label.setFont(font)
        self.label.setObjectName(_fromUtf8("label"))
        self.gridLayout.addWidget(self.label, 2, 2, 1, 1)
        spacerItem6 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem6, 5, 1, 1, 3)
        spacerItem7 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem7, 6, 4, 1, 1)
        spacerItem8 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem8, 6, 1, 1, 2)
        
        self.connNum = 0
        
        self.setWindowTitle(_translate("window_connect", "Connect to Friend", None))
        self.setWindowIcon(QtGui.QIcon('images\connect.png'))
        self.connect.setText(_translate("window_connect", "Connect", None))
        self.enterip.setText(_translate("window_connect", "Enter the IP", None))
        self.back.setText(_translate("window_connect", "Back", None))
        
        self.back.setMinimumWidth(100)
        self.back.setIcon(QtGui.QIcon('images\back.png'))
        
        self.label.setText(_translate("window_connect", "Connect To Friend", None))

        QtCore.QMetaObject.connectSlotsByName(self)
        

    def func_entered(self):
        ip=self.ip_here.text()
        c = open("ipaddress.fus", "w")
        c.write(ip)
        c.close()
        self.close()
        #adding a client type connection to window_main
        self.window_main.addConn(Connection("client", self.window_main))


    def exit(self):
        self.close()

    
    def showWindow(self):
        self.show()

    def setMainWindow(self, window_main):
        self.window_main = window_main


#################################scan_gui class#############################

class scan_gui(QtGui.QWidget):
    def __init__(self, window_main):
        super(scan_gui, self).__init__()
        self.setObjectName(_fromUtf8("scan_gui"))
        self.resize(400, 400)
        self.window_main = window_main
        self.setWindowIcon(self.window_main.icon_scan)
        self.gridLayout = QtGui.QGridLayout(self)
        self.gridLayout.setObjectName(_fromUtf8("gridLayout"))
        spacerItem = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem, 5, 2, 1, 3)
        spacerItem1 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem1, 6, 2, 1, 1)
        self.back = QtGui.QPushButton(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setPointSize(10)
        font.setBold(True)
        font.setWeight(75)
        self.back.setFont(font)
        self.back.setObjectName(_fromUtf8("back"))
        self.gridLayout.addWidget(self.back, 6, 4, 1, 1)
        spacerItem2 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem2, 0, 2, 1, 3)
        spacerItem3 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem3, 0, 0, 8, 1)
        spacerItem4 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem4, 3, 2, 1, 3)
        self.progressBar = QtGui.QProgressBar(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        self.progressBar.setFont(font)
        self.progressBar.setProperty("value", 0)
        self.progressBar.setObjectName(_fromUtf8("progressBar"))
        self.gridLayout.addWidget(self.progressBar, 4, 2, 1, 3)
        spacerItem5 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem5, 7, 2, 1, 3)
        self.scanning_label = QtGui.QLabel(self)
        self.scanning_label.setObjectName(_fromUtf8("scanning_label"))
        self.gridLayout.addWidget(self.scanning_label, 1, 2, 1, 1)
        self.scan_results = QtGui.QListWidget(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("MS Shell Dlg 2"))
        font.setPointSize(8)
        font.setBold(False)
        font.setWeight(60)
        self.scan_results.setFont(font)
        self.scan_results.setObjectName(_fromUtf8("scan_results"))
        self.gridLayout.addWidget(self.scan_results, 2, 2, 1, 3)
        spacerItem6 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem6, 0, 5, 8, 1)
        self.scan_btn = QtGui.QPushButton(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setPointSize(10)
        font.setBold(True)
        font.setWeight(75)
        self.scan_btn.setFont(font)
        self.scan_btn.setObjectName(_fromUtf8("scan_btn"))
        self.gridLayout.addWidget(self.scan_btn, 6, 3, 1, 1)
        self.back.clicked.connect(self.func_back)
        self.scan_btn.clicked.connect(self.func_scan1)

        self.retranslateUi(self)
        QtCore.QMetaObject.connectSlotsByName(self)
    
    def func_back(self):
        self.close()

    def func_scan1(self):
        ipfull = ip()
        ipsub=ipfull[:ipfull.rfind(".")+1]
        ipcomp=ipsub+ "0/24"
        ip_net = ipaddress.ip_network(ipcomp)
        all_hosts = list(ip_net.hosts())
        info = subprocess.STARTUPINFO()
        info.dwFlags |= subprocess.STARTF_USESHOWWINDOW
        info.wShowWindow = subprocess.SW_HIDE
        self.completed=0
        for i in range(len(all_hosts)):
            output = subprocess.Popen(['ping', '-n', '1', '-w', '500', str(all_hosts[i])], stdout=subprocess.PIPE, startupinfo=info).communicate()[0]  
            if "Destination host unreachable" in output.decode('utf-8'):
                pass
            elif "Request timed out" in output.decode('utf-8'):
                pass
            else:
				#str(gethostbyaddr(str(all_hosts[i]))[0])+
                self.scan_results.addItem(str(gethostbyaddr(str(all_hosts[i]))[0])+"\n"+str(all_hosts[i])+ "\n\n")
            self.progressBar.setValue(self.completed)
            self.completed=int((i/len(all_hosts))*100)+1

    def showWindow(self):
        self.show()

    def setMainWindow(self, window_main):
        self.window_main = window_main

    def retranslateUi(self, scan_gui):
        self.setWindowTitle(_translate("self", "Scan for Friends", None))
        self.setWindowIcon(QtGui.QIcon(self.window_main.icon_scan))
        self.back.setText(_translate("self", "Back", None))
        self.scanning_label.setText(_translate("self", "Scanning online friends on the network:", None))
        self.scan_btn.setText(_translate("self", "Scan", None))
        

###############################myName_gui class#############################
class myName_gui(QtGui.QWidget):
    def __init__(self, window_main):
        super(myName_gui, self).__init__()
        self.setObjectName(_fromUtf8("window_myName"))
        self.resize(400, 300)
        self.window_main = window_main
        font = QtGui.QFont()
        font.setPointSize(10)
        font.setBold(True)
        font.setUnderline(False)
        font.setWeight(75)
        self.setFont(font)
        self.gridLayout = QtGui.QGridLayout(self)
        self.gridLayout.setObjectName(_fromUtf8("gridLayout"))
        self.name_here = QtGui.QLabel(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Times New Roman"))
        font.setUnderline(True)
        self.name_here.setFont(font)
        self.name_here.setObjectName(_fromUtf8("name_here"))
        self.gridLayout.addWidget(self.name_here, 4, 2, 1, 2)
        self.ip_label = QtGui.QLabel(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setPointSize(10)
        font.setBold(False)
        font.setWeight(50)
        self.ip_label.setFont(font)
        self.ip_label.setObjectName(_fromUtf8("ip_label"))
        self.gridLayout.addWidget(self.ip_label, 4, 1, 1, 1)
        self.enter = QtGui.QPushButton(self)
        self.enter.clicked.connect(self.func_entered)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setBold(False)
        font.setWeight(50)
        self.enter.setFont(font)
        self.enter.setObjectName(_fromUtf8("enter"))
        self.gridLayout.addWidget(self.enter, 6, 5, 1, 1)
        self.back = QtGui.QPushButton(self)
        self.back.clicked.connect(self.exit)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setBold(False)
        font.setWeight(50)
        self.back.setFont(font)
        self.back.setObjectName(_fromUtf8("back"))
        self.gridLayout.addWidget(self.back, 11, 5, 1, 1)
        spacerItem = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem, 12, 1, 1, 5)
        spacerItem1 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem1, 8, 1, 1, 5)
        spacerItem2 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem2, 5, 1, 1, 5)
        spacerItem3 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem3, 2, 1, 1, 5)
        spacerItem4 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout.addItem(spacerItem4, 0, 1, 1, 5)
        spacerItem5 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem5, 1, 5, 1, 2)
        spacerItem6 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem6, 6, 6, 1, 1)
        self.Look = QtGui.QLabel(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setPointSize(15)
        font.setBold(True)
        font.setWeight(75)
        self.Look.setFont(font)
        self.Look.setObjectName(_fromUtf8("Look"))
        self.gridLayout.addWidget(self.Look, 1, 2, 1, 3)
        spacerItem7 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem7, 11, 6, 1, 1)
        spacerItem8 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem8, 4, 4, 1, 3)
        spacerItem9 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem9, 4, 0, 1, 1)
        spacerItem10 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem10, 6, 0, 1, 1)
        spacerItem11 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem11, 11, 0, 1, 5)
        spacerItem12 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem12, 1, 0, 1, 2)
        self.name_label = QtGui.QLabel(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setBold(False)
        font.setWeight(50)
        self.name_label.setFont(font)
        self.name_label.setObjectName(_fromUtf8("name_label"))
        self.gridLayout.addWidget(self.name_label, 6, 1, 1, 1)
        self.name_here = QtGui.QLineEdit(self)
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        self.name_here.setFont(font)
        self.name_here.setObjectName(_fromUtf8("name_here"))
        self.gridLayout.addWidget(self.name_here, 6, 2, 1, 3)
        self.getMyName() # reading and setting name in the name_label

        self.setWindowTitle(_translate("window_myName", "My Username", None))
        self.setWindowIcon(QtGui.QIcon('images\myName.png'))
        
        self.enter.setText(_translate("window_myName", "Enter", None))
        self.back.setText(_translate("window_myName", "Back", None))
        self.Look.setText(_translate("window_myName", "My Username", None))
        self.name_label.setText(_translate("window_myName", "Enter New Name: ", None))

        QtCore.QMetaObject.connectSlotsByName(self)

    def exit(self):
        self.close()

    def func_entered(self):
        a = self.name_here.text()
        c = open("name.fus", "w")
        c.write(a)
        c.close()
        self.getMyName() # reading and setting name in the name_label
        self.close()

    # reading and setting name in the name_label
    def getMyName(self):
        n = open("name.fus", "r")
        name = n.read()
        n.close()
        if(name):
            self.myName = name
        else:
            self.myName = gethostname()

        self.ip_label.setText(_translate("window_myName", "Your Username is: " + self.myName, None))

    def showWindow(self):
        self.show()

###########################chat window##############################
class chatWindow_gui(QtGui.QWidget):
    def __init__(self, window_main, connection):
        super(chatWindow_gui, self).__init__()
        self.setObjectName(_fromUtf8("chatWindow"))
        self.tray_icon = QtGui.QSystemTrayIcon()
        self.tray_icon.setIcon(QtGui.QIcon('images\logo.png'))
        self.tray_icon.activated.connect(self.restore_window)
        self.window_main = window_main
        self.setWindowIcon(self.window_main.icon_main)
        self.resize(260, 480)
        self.connection = connection
        self.verticalLayout_2 = QtGui.QVBoxLayout(self)
        self.verticalLayout_2.setObjectName(_fromUtf8("verticalLayout_2"))
        self.horizontalLayout = QtGui.QHBoxLayout()
        self.horizontalLayout.setObjectName(_fromUtf8("horizontalLayout"))
        spacerItem = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.horizontalLayout.addItem(spacerItem)
        self.verticalLayout = QtGui.QVBoxLayout()
        self.verticalLayout.setObjectName(_fromUtf8("verticalLayout"))
        self.gridLayout = QtGui.QGridLayout()
        self.gridLayout.setObjectName(_fromUtf8("gridLayout"))
        self.text_browser = QtGui.QTextBrowser(self)
        self.text_browser.setMinimumSize(QtCore.QSize(0, 250))
        self.text_browser.setObjectName(_fromUtf8("text_browser"))
        self.gridLayout.addWidget(self.text_browser, 1, 0, 2, 1)
        self.gridLayout_2 = QtGui.QGridLayout()
        self.gridLayout_2.setObjectName(_fromUtf8("gridLayout_2"))
        
        
        self.text_chatBox = QtGui.QLineEdit()
        self.text_chatBox.setEnabled(True)
        self.text_chatBox.setMaximumSize(QtCore.QSize(16777215, 50))
        self.text_chatBox.setObjectName(_fromUtf8("text_chatBox"))
        self.text_chatBox.editingFinished.connect(self.func_send) # send when enter key is pressed

        self.gridLayout_2.addWidget(self.text_chatBox, 0, 0, 1, 1)
        
        self.btn_send = QtGui.QPushButton(self)
        self.btn_send.setMinimumSize(QtCore.QSize(0, 50))
        font = QtGui.QFont()
        font.setFamily(_fromUtf8("Segoe Print"))
        font.setPointSize(14)
        self.btn_send.setFont(font)
        self.btn_send.setIconSize(QtCore.QSize(64, 64))
        self.btn_send.setObjectName(_fromUtf8("btn_send"))

        self.gridLayout_2.addWidget(self.btn_send, 0, 1, 1, 1)
        self.gridLayout.addLayout(self.gridLayout_2, 3, 0, 1, 1)
        self.lbl_friendName = QtGui.QLabel(self)
        self.lbl_friendName.setAlignment(QtCore.Qt.AlignCenter)
        self.lbl_friendName.setObjectName(_fromUtf8("lbl_friendName"))
        self.gridLayout.addWidget(self.lbl_friendName, 0, 0, 1, 1)
        self.verticalLayout.addLayout(self.gridLayout)
        self.gridLayout_4 = QtGui.QGridLayout()
        self.gridLayout_4.setObjectName(_fromUtf8("gridLayout_4"))
        spacerItem1 = QtGui.QSpacerItem(20, 40, QtGui.QSizePolicy.Minimum, QtGui.QSizePolicy.Expanding)
        self.gridLayout_4.addItem(spacerItem1, 1, 1, 1, 1)
        self.lbl_logo = QtGui.QLabel(self)
        self.lbl_logo.setText(_fromUtf8(""))
        self.lbl_logo.setPixmap(QtGui.QPixmap(_fromUtf8("images/logo.png")))
        self.lbl_logo.setObjectName(_fromUtf8("lbl_logo"))
        self.gridLayout_4.addWidget(self.lbl_logo, 0, 1, 1, 1)
        spacerItem2 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout_4.addItem(spacerItem2, 0, 0, 1, 1)
        spacerItem3 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.gridLayout_4.addItem(spacerItem3, 0, 2, 1, 1)
        self.verticalLayout.addLayout(self.gridLayout_4)
        self.horizontalLayout.addLayout(self.verticalLayout)
        spacerItem4 = QtGui.QSpacerItem(40, 20, QtGui.QSizePolicy.Expanding, QtGui.QSizePolicy.Minimum)
        self.horizontalLayout.addItem(spacerItem4)
        self.verticalLayout_2.addLayout(self.horizontalLayout)

        
        #send
        self.btn_send.clicked.connect(self.func_send)
        self.str_message = "msg"

        #recv
        self.recv_thread()

        
        self.btn_send.setText(_translate("chatWindow", "Send", None))
        QtCore.QMetaObject.connectSlotsByName(self)
        

    def setMainWindow(self, window_main):
        self.window_main = window_main

    def recv_thread(self):
        self.trd_recv = Thread(target=self.func_recv)

    def setConnection(self, connection):
        self.connection = connection

    def func_send(self):
        self.str_message = self.text_chatBox.text()
        self.text_chatBox.setText("")
        if (self.str_message):
            self.s.send(self.str_message.encode('utf-8'))
            self.text_browser.append("Me: " + self.str_message)
        
    def func_recv(self):
        while True:
            self.data = self.s.recv(1024).decode('utf-8')
            if not self.data:
                break
            self.text_browser.append(self.str_friendName + ": " + self.data)

    def setAttributes(self):
        #adjusting socket w.r.t server or client
        if(self.connection.str_Type == "server"):
                self.s = self.connection.c  # because   c, addr = s.accept()
        else:
                self.s = self.connection.s
        
        self.str_friendName = self.connection.friend
        self.str_host = self.connection.host
        
        self.lbl_friendName.setText(_translate("chatWindow", "Chatting with: " + self.str_friendName, None))
        self.setWindowTitle(_translate("Chat: " + self.str_friendName, "Chat: " + self.str_friendName, None))

############################minimizing to tray functionality############################

    def event(self, event):
        if (event.type() == QtCore.QEvent.WindowStateChange and self.isMinimized()):
            self.setWindowFlags(self.windowFlags() & ~QtCore.Qt.Tool)
            self.tray_icon.show()
            return True
        else:
            return super(chatWindow_gui, self).event(event)

    def closeEvent(self, event):
        reply = QtGui.QMessageBox.question(self,'Message',"Would you like to minimize to the tray?",QtGui.QMessageBox.Yes | QtGui.QMessageBox.No,QtGui.QMessageBox.No)
        if reply == QtGui.QMessageBox.No:
            self.close()
            self.window_main.tray_icon.hide()
            self.window_main.showNormal()
        else:
            self.tray_icon.show()
            self.hide()
            event.ignore()

    def restore_window(self, reason):
        if reason == QtGui.QSystemTrayIcon.DoubleClick:
            self.tray_icon.hide()
            self.showNormal()
