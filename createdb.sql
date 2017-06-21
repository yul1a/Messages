CREATE DATABASE Messages
ON (
  NAME = dbName_dat,
  FILENAME = 'C:\workspace\other\Messages\db.mdf'
)
LOG ON (
  NAME = dbName_log,
  FILENAME = 'C:\workspace\other\Messages\db.ldf'
)