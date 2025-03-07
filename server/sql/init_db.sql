CREATE TABLE precipitationTypes  (
	Id INTEGER NOT NULL PRIMARY KEY,
	Description TEXT(255) NOT NULL
)

INSERT INTO precipitationTypes (Id, Description)
VALUES
	(1, "Облачно"),
	(2, "Солнечно"),
	(3, "Дождь"),
	(4, "Снег"),
	(5, "Снег с дождем"),
	(6, "Облачно с прояснениями")
	
CREATE TABLE windDirections (
	Id INTEGER NOT NULL PRIMARY KEY,
	Description TEXT(255) NOT NULL
)

INSERT INTO windDirections (Id, Description)
VALUES
	(1, "Южный"),
	(2, "Северный"),
	(3, "Западный"),
	(4, "Восточный"),
	(5, "Юго-западный"),
	(6, "Юго-восточный"),
	(7, "Северо-западный"),
	(8, "Северо-восточный")
	
CREATE TABLE dayParts (
	Id INTEGER NOT NULL PRIMARY KEY,
	Description TEXT(255) NOT NULL
)

INSERT INTO dayParts (Id, Description)
VALUES
	(1, "Утро"),
	(2, "День"),
	(3, "Вечер")
	
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
	(1739207223, 1, 10, 747, 3, 1, 1),
	(1739207223, 2, 15, 750, 2, 2, 2),
	(1739207223, 3, 11, 751, 6, 3, 2),
	(1739293623, 1, 15, 743, 2, 4, 4),
	(1739293623, 2, 18, 755, 7, 5, 5), 
	(1739293623, 3, 14, 756, 4, 6, 6),
	(1739380023, 1, 15, 743, 2, 1, 1), 
	(1739380023, 2, 3, 755, 7, 8, 2), 
	(1739380023, 3, 10, 756, 4, 1, 3),
	(1740960000, 1, 22, 750, 2, 1, 1)