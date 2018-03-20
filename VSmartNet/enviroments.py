#!/usr/bin/python
import queue
def init():
    global dBContext
    dBContext = None
    global isLogin
    isLogin = False
    global stopSignal
    stopSignal = False
    global server
    server = None
    global client_response
    client_response = queue.Queue()
