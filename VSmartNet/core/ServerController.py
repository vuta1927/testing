import socket
from threading import Thread, Event
import queue
import configs
from PyQt5.QtCore import pyqtSignal

class ServerThread(Thread):
    # Register the signal handlers
    def __init__(self, parent):
        super(ServerThread, self).__init__()
        self.host_address = configs.hostAddress
        self.host_port = configs.hostPort
        self.stop_request = Event()
        self.client_threads = []
        self._running = True
        self.parent = parent
        self._tcp_server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    def run(self):
        self.sever_loop()

    def sever_loop(self):
        self._tcp_server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        self._tcp_server.bind((self.host_address, self.host_port))
        while self._running:
            self._tcp_server.listen(4)
            self.parent.statusBar().showMessage("Server started! Waiting for connections from TCP clients...")
            (conn, (ip, port)) = self._tcp_server.accept()
            new_client_thread = ClientThread(ip, port, conn)
            new_client_thread.start()
            self.client_threads.append(new_client_thread)
        self.parent.statusBar().showMessage('Server shutdown!')

    def stop(self, timeout=None):
        self._running = False
        socket.socket(socket.AF_INET,
                      socket.SOCK_STREAM).connect((self.host_address, self.host_port))
        self._tcp_server.close()
        # self.stop_request.set()
        super(ServerThread, self).join(timeout)
        if self.client_threads.__len__() > 0:
            for t in self.client_threads:
                t.join()

class ClientThread(Thread):
    def __init__(self, ip, port, conn):
        super(ClientThread, self).__init__()
        self.ip = ip
        self.port = port
        self.conn = conn
        self.stop_request = Event()
        print("[+] New server socket thread started for " + ip + ":" + str(port))

    def run(self):
        while not self.stop_request.isSet():
            data = self.conn.recv(2048)
            print("Server received data:", data.decode())
            message = input("Python server : Enter Response from Server/Enter exit:")
            if message == 'exit':
                break
            self.conn.send(message.encode())  # echo

    def join(self, timeout=None):
        self.stop_request.set()
        # super(ClientThread, self).join(timeout)
