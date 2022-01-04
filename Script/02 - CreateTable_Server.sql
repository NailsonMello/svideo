USE SVIDEO

CREATE TABLE Server (
	Id uniqueidentifier NOT NULL,
	Name varchar(200) NOT NULL,
	IpAddress varchar(200) NOT NULL,
	Port int NOT NULL,
	CONSTRAINT Server_PK PRIMARY KEY (Id),
);