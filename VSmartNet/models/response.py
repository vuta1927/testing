class Response:
    def __init__(self, client, command_type, status, message=None):
        self.client = client
        self.command_type = command_type
        self.status = status
        self.message = message
