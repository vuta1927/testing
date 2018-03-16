import socket
from threading import Thread
import configs
from PyQt5.QtCore import QThread
import signal

class Server(QThread):
    # Register the signal handlers
    signal.signal(signal.SIGTERM, stop)
    signal.signal(signal.SIGINT, stop)
    def __init__(self):
        Thread.__init__(self)
        self.host_address = configs.hostAddress
        self.host_port = configs.hostPort
        self.client_threads = []

    def run(self):
        tcp_server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        tcp_server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        tcp_server.bind((self.host_address, self.host_port))
        while True:
            tcp_server.listen(4)
            print("Python server : Waiting for connections from TCP clients...")
            (conn, (ip, port)) = tcp_server .accept()
            new_client_thread = ClientThread(ip, port, conn)
            new_client_thread.start()
            self.client_threads.append(new_client_thread)

        for t in threads:
            t.join()

    def stop(self):
        self.server_signal.emit()

class ClientThread(Thread):
    def __init__(self, ip, port, conn):
        Thread.__init__(self)
        self.ip = ip
        self.port = port
        self.conn = conn
        print("[+] New server socket thread started for " + ip + ":" + str(port))

    def run(self):
        while True:
            data = self.conn.recv(2048)
            print("Server received data:", data.decode())
            message = input("Python server : Enter Response from Server/Enter exit:")
            if message == 'exit':
                break
            self.conn.send(message.encode())  # echo