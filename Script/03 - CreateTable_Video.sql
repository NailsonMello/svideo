USE SVIDEO

CREATE TABLE Video (
	Id uniqueidentifier NOT NULL,
	IdServer uniqueidentifier NOT NULL,
	Description varchar(255) NOT NULL,
	ContentType varchar(50) NOT NULL,
	SizeInBytes varbinary(max) NULL,
	CreatedAt datetime NOT NULL,
	
	CONSTRAINT Video_PK PRIMARY KEY (Id),
	CONSTRAINT Video_Server_FK FOREIGN KEY (IdServer) REFERENCES Server(Id),
);