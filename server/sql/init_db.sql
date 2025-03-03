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
	
CREATE TABLE measures (
	Measure_date INTEGER NOT NULL PRIMARY KEY,
	Morning_temperature REAL DEFAULT NULL,
	Morning_pressure REAL DEFAULT NULL,
	Morning_wind_speed REAL DEFAULT NULL,
	Morning_wind_directionId INTEGER DEFAULT NULL REFERENCES windDirections(Id) ON UPDATE CASCADE ON DELETE SET NULL,
	Morning_precipitation_typeId INTEGER DEFAULT NULL REFERENCES precipitationTypes(Id) ON UPDATE CASCADE ON DELETE SET NULL,
	Afternoon_temperature REAL DEFAULT NULL,
	Afternoon_pressure REAL DEFAULT NULL,
	Afternoon_wind_speed REAL DEFAULT NULL,
	Afternoon_wind_directionId INTEGER DEFAULT NULL REFERENCES windDirections(Id) ON UPDATE CASCADE ON DELETE SET NULL,
	Afternoon_precipitation_typeId INTEGER DEFAULT NULL REFERENCES precipitationTypes(Id) ON UPDATE CASCADE ON DELETE SET NULL,
	Evening_temperature REAL DEFAULT NULL,
	Evening_pressure REAL DEFAULT NULL,
	Evening_wind_speed REAL DEFAULT NULL,
	Evening_wind_directionId INTEGER DEFAULT NULL REFERENCES windDirections(Id) ON UPDATE CASCADE ON DELETE SET NULL,
	Evening_precipitation_typeId INTEGER DEFAULT NULL REFERENCES precipitationTypes(Id) ON UPDATE CASCADE ON DELETE SET NULL
)

INSERT INTO measures(Measure_date, Morning_temperature, Morning_pressure, Morning_wind_speed, Morning_wind_directionId, Morning_precipitation_typeId,
					Afternoon_temperature, Afternoon_pressure, Afternoon_wind_speed, Afternoon_wind_directionId, Afternoon_precipitation_typeId,
					Evening_temperature, Evening_pressure, Evening_wind_speed, Evening_wind_directionId, Evening_precipitation_typeId)
VALUES 
	(1739207223, 10, 747, 3, 0, 0, 15, 750, 2, 1, 1, 11, 751, 6, 2, 1),
	(1739293623, 15, 743, 2, 3, 3, 18, 755, 7, 4, 4, 14, 756, 4, 5, 5),
	(1739380023, 15, 743, 2, 0, 0, 3, 755, 7, 7, 1, 10, 756, 4, 0, 2),
	(1740960000, 22, 750, 2, 0, 0, null, null, null, null, null, null, null, null, null, null)