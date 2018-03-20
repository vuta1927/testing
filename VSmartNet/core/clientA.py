# Python TCP Client A
import socket 

host = "127.0.0.1"#socket.gethostname()
port = 6268
BUFFER_SIZE = 2000 
MESSAGE = "1#1,1,48-2C-6A-1E-59-3D,1,20-03-2018,test"
 
tcpClientA = socket.socket(socket.AF_INET, socket.SOCK_STREAM) 
tcpClientA.connect((host, port))

while MESSAGE != 'shutdown':
    tcpClientA.send(MESSAGE.encode())     
    data = tcpClientA.recv(BUFFER_SIZE)
    print(" Client2 received data:", data.decode())
    MESSAGE = input("tcpClientA: Enter message to continue/ Enter exit:")

tcpClientA.close() 