#!/usr/bin/python
import enviroments
from models.user import User
def check_authenticate(user):
    try:
        sql = 'SELECT * FROM users WHERE username = %s AND password = %s'
        enviroments.dBContext.cur.execute(sql, (user.username, user.password))
    except Exception as e:
        print(e)

    result = enviroments.dBContext.cur.fetchone()
    if(result != None):
        return User(result['id'], result['username'], result['password'], result['fullname'])
    else:
        return False

def getUser(username):
    try:
        sql = 'SELECT * FROM users WHERE username = %s'
        enviroments.dBContext.cur.execute(sql, username)
    except Exception as  e:
        print(e)

    result = enviroments.dBContext.cur.fetchone()
    if(result != None):
        return User(result['id'], result['username'], result['password'], result['fullname'])
    else:
        return None
