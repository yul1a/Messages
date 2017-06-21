CREATE DATABASE TestMessages
ON (
  NAME = dbName_dat,
  FILENAME = 'C:\workspace\other\Messages\Test.Messages\TestMessagesdb.mdf'
)
LOG ON (
  NAME = dbName_log,
  FILENAME = 'C:\workspace\other\Messages\Test.Messages\TestMessagesdb.ldf'
)