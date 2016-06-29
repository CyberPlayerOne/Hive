create database hiveserver 
use hiveserver 
create table ClientUsers
(
id int not null,
Ip varchar(50),
Name varchar(50),
StatusInfo nvarchar(50),
LastSetOnlineTime varchar(50),
ShareListXml ntext
)

