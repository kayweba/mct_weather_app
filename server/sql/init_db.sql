CREATE TABLE precipitationTypes  (
	Id INTEGER NOT NULL PRIMARY KEY,
	Description TEXT(255) NOT NULL
)

INSERT INTO precipitationTypes (Id, Description)
VALUES
	(0, "Облачно"),
	(1, "Солнечно"),
	(2, "Дождь"),
	(3, "Снег"),
	(4, "Снег с дождем"),
	(5, "Облачно с прояснениями")
	
CREATE TABLE windDirections (
	Id INTEGER NOT NULL PRIMARY KEY,
	Description TEXT(255) NOT NULL
)

INSERT INTO windDirections (Id, Description)
VALUES
	(0, "Южный"),
	(1, "Северный"),
	(2, "Западный"),
	(3, "Восточный"),
	(4, "Юго-западный"),
	(5, "Юго-восточный"),
	(6, "Северо-западный"),
	(7, "Северо-восточный")
	
CREATE TABLE dayParts (
	Id INTEGER NOT NULL PRIMARY KEY,
	Description TEXT(255) NOT NULL
)

INSERT INTO dayParts (Id, Description)
VALUES
	(0, "Утро"),
	(1, "День"),
	(2, "Вечер")
	
CREATE TABLE measures (
	Measure_date INTEGER NOT NULL,
	Measure_day_partId INTEGER NOT NULL,
	Temperature REAL DEFAULT NULL,
	Pressure REAL DEFAULT NULL,
	Wind_speed REAL DEFAULT NULL,
	Wind_directionId INTEGER DEFAULT NULL,
	Precipitation_typeId INTEGER DEFAULT NULL,
	PRIMARY KEY (Measure_date, Measure_day_part),
	CONSTRAINT FK_Wind_direction FOREIGN KEY (Wind_directionId) REFERENCES windDirections(Id) ON UPDATE CASCADE ON DELETE SET NULL,
	CONSTRAINT FK_Precipitation_type FOREIGN KEY (Precipitation_typeId) REFERENCES precipitationTypes(Id) ON UPDATE CASCADE ON DELETE SET NULL,
	CONSTRAINT FK_Day_Part FOREIGN KEY (Measure_day_part) REFERENCES dayParts(Id) ON UPDATE CASCADE ON DELETE CASCADE
)

INSERT INTO measures (Measure_date, Measure_day_part, Temperature, Pressure, Wind_speed, Wind_directionId, Precipitation_typeId)
VALUES 
	(1739207223, 0, 10, 747, 3, 0, 0),
	(1739207223, 1, 15, 750, 2, 1, 1),
	(1739207223, 2, 11, 751, 6, 2, 1),
	(1739293623, 0, 15, 743, 2, 3, 3),
	(1739293623, 1, 18, 755, 7, 4, 4), 
	(1739293623, 2, 14, 756, 4, 5, 5),
	(1739380023, 0, 15, 743, 2, 0, 0), 
	(1739380023, 1, 3, 755, 7, 7, 1), 
	(1739380023, 2, 10, 756, 4, 0, 2),
	(1740960000, 0, 22, 750, 2, 0, 0)