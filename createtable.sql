USE Messages

CREATE TABLE Message(
  ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  header nvarchar(255),
  body nvarchar(4000)
	
)