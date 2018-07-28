create table Client(
	Id uniqueidentifier primary key, 
	Name varchar(max), 
	Email varchar(max), 
	IsActive bit, 
	Street varchar(max), 
	Number varchar(max), 
	Neighborhood varchar(max), 
	City varchar(max), 
	State varchar(max), 
	Country varchar(max), 
	ZipCode varchar(max)
);

create table Subscription(
	Id  uniqueidentifier primary key, 
	ClientId uniqueidentifier foreign key references Client(id), 
	Name varchar(max), 
	CreatedDate datetime, 
	LastUpdatedDate datetime, 
	ExpireDate datetime, 
	Active bit
);
