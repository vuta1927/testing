#!/usr/bin/python
import enviroments
from models.client import Client
def is_exist(client):
    try:
        sql = 'SELECT * FROM clients WHERE macAddress = %s'
        enviroments.dBContext.cur.execute(sql, client.clientId)
    except Exception as e:
        print(e)

    result = enviroments.dBContext.cur.fetchone()
    if result is not None:
        return True
    else:
        return False

def get_client(clientId):
    try:
        sql = 'SELECT * FROM clients WHERE macAddress = %s'
        enviroments.dBContext.cur.execute(sql, clientId)
    except Exception as e:
        print(e)

    result = enviroments.dBContext.cur.fetchone()
    if result is not None:
        return Client(result['id'], result['clientId'], result['macAddress'], result['stationId'], result['dateStart'], result['descriptions'])
    else:
        return None

def add(client):
    try:
        sql = 'INSERT INTO clients (clientId, macAddress, stationId, dateStart, descriptions) VALUES(%s, %s, %s, %s, %s)'
        enviroments.dBContext.cur.execute(sql, (client.clientId, client.macAddress, client.stationId, client.dateStart, client.descriptions))
    except Exception as e:
        print(e)

    result = enviroments.dBContext.cur.fetchone()
    if result is not None:
        return Client(result['id'], result['clientId'], result['macAddress'], result['stationId'], result['dateStart'], result['descriptions'])
    else:
        return None
