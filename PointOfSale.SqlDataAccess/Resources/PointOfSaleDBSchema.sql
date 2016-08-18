DROP DATABASE IF EXISTS pointofsale;

CREATE DATABASE pointofsale;

USE pointofsale;

CREATE TABLE ITEM(
    barcode VARCHAR(8) NOT NULL,
    PRIMARY KEY (barcode),
    Name varchar(50) NOT NULL,
    price decimal(4,2) NOT NULL
);

INSERT INTO ITEM(barcode, name, price)
VALUES
('123456','Bowl', 12.50),
('900000', 'Phone', 7.50),
('456789', 'Crab', 24.50),
('345670', 'Plunger', 6.50),
('789010', 'Fish', 10.25);
