from main import *

#connection class
class Connection(object):
    def __init__(self, str_Type, window_main):
        super(Connection, self).__init__()
        self.str_Type = str_Type
        self.window_main = window_main
    def setType(self, str_type):
        self.str_Type = str_type

    def startConn(self):
        self.n = open("name.fus", "r")
        self.me = self.n.read()

        if(self.str_Type == "server"):
            self.host = self.window_main.ip()
        else:
            self.f = open("ipaddress.fus","r")
            self.host = self.f.read()
            self.f.close()
        
        self.port = 8001 + self.window_main.portAdd
        self.s = socket()

        if(self.str_Type == "server"):
            self.s.bind((self.host, self.port))
            self.s.listen(1)
            #accepting connection
            self.c, self.addr = self.s.accept()
            #receiving name of friend
            self.friend = self.c.recv(1024).decode('utf-8')
            #sending name of me
            self.c.send(self.me.encode('utf-8'))
        else:
            self.s.connect((self.host, self.port))
            #sending name of me
            self.s.send(self.me.encode('utf-8'))
            #receiving name of friend
            self.friend = self.s.recv(1024).decode('utf-8')
            
        
        #incrementing in port no.
        self.window_main.incrementPort()
        
    def setMainWindow(self, window_main):
        self.window_main = window_main

    def getFriend(self):
        return self.friend

    def getHost(self):
        return self.host