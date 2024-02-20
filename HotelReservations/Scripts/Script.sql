
CREATE TABLE room_type (
	room_type_id INT IDENTITY(1,1) PRIMARY KEY,
	room_type_name VARCHAR(255) NOT NULL UNIQUE,
	room_type_is_active BIT NOT NULL
);

CREATE TABLE room (
	room_id INT IDENTITY(1,1) PRIMARY KEY,
	room_number VARCHAR(25) NOT NULL UNIQUE,
	has_TV BIT NOT NULL,
	has_mini_bar BIT NOT NULL,
	room_type_id INT NOT NULL,
	room_is_active BIT NOT NULL,
	CONSTRAINT FK_ROOM_ROOM_TYPE
	FOREIGN KEY (room_type_id) REFERENCES dbo.room_type (room_type_id)
);
CREATE TABLE [user] (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50) NOT NULL,
    surname NVARCHAR(50) NOT NULL,
    jmbg CHAR(13) NOT NULL UNIQUE,
    username NVARCHAR(50) NOT NULL UNIQUE,
    password NVARCHAR(255) NOT NULL,
    user_type NVARCHAR(50) NOT NULL,
    is_active BIT NOT NULL
);
CREATE TABLE guest (
    guest_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50) NOT NULL,
    surname NVARCHAR(50) NOT NULL,
    jmbg CHAR(20) NOT NULL UNIQUE,
    is_active BIT NOT NULL
);
CREATE TABLE price_list (
    price_id INT IDENTITY(1,1) PRIMARY KEY,
    room_type_id INT NOT NULL,
    reservation_type NVARCHAR(50) NOT NULL,
    price_value DECIMAL(10, 2) NOT NULL,
    is_active BIT NOT NULL,
    CONSTRAINT FK_PRICE_LIST_ROOM_TYPE
        FOREIGN KEY (room_type_id) REFERENCES room_type (room_type_id)
);
CREATE TABLE reservation (
    reservation_id INT IDENTITY(1,1) PRIMARY KEY,
    roomNumber NVARCHAR(50) NOT NULL,
    reservation_type NVARCHAR(50) NOT NULL,
    guest NVARCHAR(50) NOT NULL,
    start_date_time DATETIME NOT NULL,
    end_date_time DATETIME NOT NULL,
    total_price DECIMAL(10, 2) NOT NULL,
    is_active BIT NOT NULL

);




