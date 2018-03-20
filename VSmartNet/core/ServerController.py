import socket
from threading import Thread, Event
from PyQt5.QtCore import pyqtSignal, pyqtSlot
import queue
import configs
from models.command import Command
from models.client import Client
import controllers.ClientController as clientControl
from models.response import Response
class ServerThread(Thread):
    # Register the signal handlers
    response_signal = pyqtSignal(int, bool, object)
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
            (conn, (ip, port)) = self._tcp_server.accept()
            new_client_thread = ClientThread(ip, port, conn)
            new_client_thread.response_signal.connect(self.callback_response_signal)
            new_client_thread.start()
            self.client_threads.append(new_client_thread)

    def stop(self, timeout=None):
        self._running = False
        quit_sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        quit_sock.connect((self.host_address, self.host_port))
        # quit_message = 'shutdown'
        # quit_sock.send(quit_message.encode())
        quit_sock.close()
        self._tcp_server.close()
        # self.stop_request.set()
        super(ServerThread, self).join(timeout)
        if self.client_threads.__len__() > 0:
            for t in self.client_threads:
                t.join()

    @pyqtSlot(int, bool, object)
    def callback_response_signal(self, command_type, status, message):
        self.response_signal.emit(command_type, status, message)

class ClientThread(Thread):
    response_signal = pyqtSignal(int, bool, object)
    def __init__(self, ip, port, conn):
        super(ClientThread, self).__init__()
        self.ip = ip
        self.port = port
        self.conn = conn
        self.client = None
        self.stop_request = Event()
        self.command_queue = queue.Queue()
        self.response_queue = queue.Queue()
        self.response_thread = None
        print("[+] New server socket thread started for " + ip + ":" + str(port))

    def run(self):
        while not self.stop_request.isSet():
            try:
                data = self.conn.recv(2048)
                if not data:
                    break
                res = data.decode().split('#')
                header = res[0]
                mess = res[1]
                print("Server received data:", data.decode())

                if header == Command.Response.close:
                    break
                    self.join()
                elif header == Command.Response.echo:
                    if mess is None:
                        self.response_queue.put(Response(host, Command.Type.echo, True))
                    else:
                        self.response_queue.put(Response(host, Command.Type.echo, False, mess))
                elif header == Command.Response.wifi_checking:
                    if mess is None:
                        self.response_queue.put(Response(host, Command.Type.wifi_checking, True))
                    else:
                        self.response_queue.put(Response(host, Command.Type.wifi_checking, False, mess))
                elif header == Command.Response.sensor_checking:
                    if mess is None:
                        self.response_queue.put(Response(host, Command.Type.sensor_checking, True))
                    else:
                        self.response_queue.put(Response(host, Command.Type.sensor_checking, False, mess))
                elif header == Command.Response.info:
                    temp = mess.split(',')
                    host = Client(int(temp[0]), int(temp[1]), temp[2], int(temp[3]), temp[4])
                    if clientControl.is_exist(host):
                        self.response_queue.put(Response(host, Command.Type.info, True))
                    else:
                        self.response_queue.put(Response(host, Command.Type.info, False, 'client not found!'))
                elif header == Command.Response.identify:
                    temp = mess.split(',')
                    host = Client(int(temp[0]), int(temp[1]), temp[2], int(temp[3]), temp[4])
                    if not clientControl.is_exist(host):
                        new_host = clientControl.add(host)
                        self.response_queue.put(Response(host, Command.Type.identify, True))
                        self.response_thread = ResponseTheard(new_host, self.response_queue)
                        self.response_thread.response_signal.connect(self.callback_response_signal)
                        self.response_thread.start()
                    else:
                        self.response_queue.put(Response(host, Command.Type.identify, False, 'client is exist!'))
                else:
                    print(res)

                command = self.command_queue.get(True, 0.05)
                self.conn.send(command.encode())
            except Exception:
                break

    def join(self, timeout=None):
        self.stop_request.set()
        try:
            self.conn.shutdown(socket.SHUT_RDWR)
            self.conn.close()
            self.response_thread.join()
        except Exception:
            pass
        super(ClientThread, self).join(timeout)

    def send_command(self, command):
        self.command_queue.put(command)

    @pyqtSlot(int, bool, object)
    def callback_response_signal(self, command_type, status, message):
        self.response_signal.emit(command_type, status, message)

class ResponseTheard(Thread):
    response_signal = pyqtSignal(int, bool, object)
    def __init__(self, client, response_q):
        super(ResponseTheard, self).__init__()
        self.client = client
        self.response_queue = response_q
        self.stop_request = Event()

    def run(self):
        while not self.stop_request.isSet():
            try:
                response = self.response_queue.get(True, 0.05)
                self.response_signal.emit(response.command_type, response.status, response.message)
            except queue.Empty:
                continue

    def join(self, timeout=None):
        self.stop_request.set()
        super(ResponseTheard, self).join(timeout)

