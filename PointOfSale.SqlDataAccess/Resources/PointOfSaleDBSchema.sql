CREATE DATABASE IF NOT EXISTS pointofsale;

USE pointofsale;

CREATE TABLE Item(
    barcode VARCHAR(8) NOT NULL,
    PRIMARY KEY (barcode),
    Name varchar(50) NOT NULL,
    price decimal(4,2) NOT NULL
);


