#!/usr/bin/python
from os import path
from PyQt5.uic import loadUiType

FORM_SPLASH, _ = loadUiType(path.join(path.dirname(__file__), "./views/assets/splashDialog.ui"))
FORM_MAIN, _ = loadUiType(path.join(path.dirname(__file__), "./views/assets/main.ui"))
FORM_CONFIG, _ = loadUiType(path.join(path.dirname(__file__), "./views/assets/ServerSettingsDialog.ui"))
FORM_LOGIN, _ = loadUiType(path.join(path.dirname(__file__), "./views/assets/LoginDialog.ui"))
FORM_TEST, _ = loadUiType(path.join(path.dirname(__file__), "./views/assets/test.ui"))
