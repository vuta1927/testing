class Client:
    def __init__(self, id, macAddress, stationId, dateStart, descriptions):
        self.id = id
        self.macAddress = macAddress
        self.stationId = stationId
        self.dateStart = dateStart
        self.descriptions = descriptions
        return self