#!/usr/bin/python
import json
def init():
    global hostAddress, hostPort
    with open('./config.json', encoding='utf-8-sig') as json_data:
        config = json.load(json_data)
        if config['HostAddress'] is not None:
            hostAddress = config['HostAddress']
        if config['HostPort'] is not None:
            hostPort = config['HostPort']
