CREATE DATABASE Hotel_Management;

USE Hotel_Management;

CREATE TABLE Clienti (
    ID_Client INT IDENTITY(1,1) PRIMARY KEY,
    Email VARCHAR(100),
    Telefon VARCHAR(20),
    Nume VARCHAR(50),
    Username VARCHAR(50),
    Parola VARCHAR(50)
);

USE Hotel_Management;

INSERT INTO Clienti (Email, Telefon, Nume, Username, Parola) VALUES 
('client1@example.com', '1234567890', 'John Doe', 'john_doe', 'parola123'),
('client2@example.com', '0987654321', 'Alice Smith', 'alice_smith', 'password'),
('client3@example.com', '1112223333', 'Emma Johnson', 'emma_j', 'securepwd'),
('client4@example.com', '4445556666', 'Michael Brown', 'mike_b', 'mysecretpass'),
('client5@example.com', '7778889999', 'Sophia Wilson', 'sophia_w', 'p@ssw0rd'),
('client6@example.com', '3332221111', 'William Taylor', 'will_t', 'clientpass'),
('client7@example.com', '9998887777', 'Olivia Anderson', 'olivia_a', 'pass1234'),
('client8@example.com', '6663339999', 'Daniel Martinez', 'danny_m', 'mypass123'),
('client9@example.com', '5551117777', 'Ava Garcia', 'ava_g', 'letmein'),
('client10@example.com', '2224446666', 'Ethan Nguyen', 'ethan_n', '123456789');

ALTER TABLE Clienti
ADD Varsta INT,
    DataNastere DATE;

UPDATE Clienti
SET Varsta = 25, DataNastere = '1997-02-10'
WHERE Nume = 'John Doe';

UPDATE Clienti
SET Varsta = 28, DataNastere = '1994-08-23'
WHERE Nume = 'Alice Smith';

UPDATE Clienti
SET Varsta = 30, DataNastere = '1992-05-20'
WHERE Nume = 'Emma Johnson';

UPDATE Clienti
SET Varsta = 35, DataNastere = '1987-11-05'
WHERE Nume = 'Michael Brown';

UPDATE Clienti
SET Varsta = 28, DataNastere = '1993-09-18'
WHERE Nume = 'Ethan Nguyen';

UPDATE Clienti
SET Varsta = 23, DataNastere = '1999-07-02'
WHERE Nume = 'Sophia Wilson';

UPDATE Clienti
SET Varsta = 32, DataNastere = '1990-04-15'
WHERE Nume = 'William Taylor';

UPDATE Clienti
SET Varsta = 27, DataNastere = '1995-12-28'
WHERE Nume = 'Olivia Anderson';

UPDATE Clienti
SET Varsta = 29, DataNastere = '1993-03-22'
WHERE Nume = 'Daniel Martinez';

UPDATE Clienti
SET Varsta = 31, DataNastere = '1991-08-14'
WHERE Nume = 'Ava Garcia';

ALTER TABLE Clienti
ADD Adresa VARCHAR(150);


CREATE TABLE Recenzii (
    ID_Recenzie INT IDENTITY(1,1) PRIMARY KEY, 
    ID_Client INT FOREIGN KEY REFERENCES Clienti(ID_Client), 
    Nota TINYINT CHECK (Nota BETWEEN 0 AND 5), 
    Descriere_Text NVARCHAR(MAX)
);


INSERT INTO Recenzii (ID_Client, Nota, Descriere_Text) VALUES 
(1, 4, 'Servicii excelente, personal amabil.'),
(3, 3, 'Camere curate, dar serviciul la restaurant poate fi imbunatatit.'),
(2, 5, 'Experienta fantastica! Totul a fost perfect.'),
(4, 2, 'Nu am fost multumit de curatenie, dar personalul a fost politicos.'),
(5, 4, 'Un hotel minunat, dar micsorarea spatiului de parcare ar trebui rezolvata.');


CREATE TABLE Functii (
    ID_Functie INT IDENTITY(1,1) PRIMARY KEY,
    Nume VARCHAR(50),
    Renumeratie DECIMAL(10, 2),
    Departament VARCHAR(50)
);

INSERT INTO Functii (Nume, Renumeratie, Departament) VALUES 
('Manager', 5000.00, 'Receptie'),
('Chelner', 2500.00, 'Restaurant'),
('Administrator', 4500.00, 'Administrativ'),
('Paznic', 2800.00, 'Securitate'),
('Receptioner', 3000.00, 'Receptie'),
('Bucatar', 4000.00, 'Restaurant'),
('Curatator', 2000.00, 'Curatenie');

CREATE TABLE Angajati (
    ID_Angajat INT IDENTITY(1,1) PRIMARY KEY, 
    Nume VARCHAR(50),
    Varsta INT,
    ID_Functie INT FOREIGN KEY REFERENCES Functii(ID_Functie) 
);

INSERT INTO Angajati (Nume, Varsta, ID_Functie) VALUES 
('Alex Popescu', 28, 1),
('Maria Ionescu', 35, 2),
('Andrei Neagu', 24, 3),
('Elena Vasile', 30, 4),
('Mihai Georgescu', 26, 5);

CREATE TABLE Camere (
    ID_Camera INT IDENTITY(1,1) PRIMARY KEY, 
    Nr_Camera INT,
    Tip_Camera VARCHAR(50),
    Nr_Max_Persoane INT,
    Pret_Noapte DECIMAL(10, 2)
);

INSERT INTO Camere (Nr_Camera, Tip_Camera, Nr_Max_Persoane, Pret_Noapte) VALUES 
(101, 'Dubla', 2, 150.00),
(102, 'Dubla', 2, 150.00),
(103, 'Tripla', 3, 200.00),
(104, 'Single', 1, 100.00),
(105, 'Dubla', 2, 150.00),
(106, 'Single', 1, 100.00),
(107, 'Tripla', 3, 200.00),
(108, 'Dubla', 2, 150.00),
(109, 'Dubla', 2, 150.00),
(110, 'Single', 1, 100.00);


CREATE TABLE Rezervari (
    ID_Rezervare INT IDENTITY(1,1) PRIMARY KEY, 
    ID_Client INT FOREIGN KEY REFERENCES Clienti(ID_Client),
    Data_Rezervarii DATE,
    Durata_Cazarii INT,
    Check_In DATE,
    Check_Out DATE,
    All_Inclusive VARCHAR(3) CHECK (All_Inclusive IN ('da', 'nu')),
    Half_Board VARCHAR(3) CHECK (Half_Board IN ('da', 'nu')),
    Acces_Spa VARCHAR(3) CHECK (Acces_Spa IN ('da', 'nu')),
    Acces_Piscina VARCHAR(3) CHECK (Acces_Piscina IN ('da', 'nu')),
    Status_Plata VARCHAR(3) CHECK (Status_Plata IN ('da', 'nu'))
);


INSERT INTO Rezervari (ID_Client, Data_Rezervarii, Durata_Cazarii, Check_In, Check_Out, All_Inclusive, Half_Board, Acces_Spa, Acces_Piscina, Status_Plata) VALUES 
(1, '2024-02-05', 4, '2024-02-10', '2024-02-14', 'da', 'nu', 'da', 'da', 'da'),
(3, '2024-03-20', 7, '2024-04-05', '2024-04-12', 'da', 'da', 'da', 'da', 'nu'),
(2, '2024-04-12', 3, '2024-04-15', '2024-04-18', 'nu', 'da', 'nu', 'nu', 'da'),
(4, '2024-05-01', 5, '2024-05-10', '2024-05-15', 'da', 'nu', 'da', 'da', 'nu'),
(5, '2024-06-15', 2, '2024-06-17', '2024-06-19', 'nu', 'nu', 'nu', 'nu', 'nu');


CREATE TABLE Rezervari_Camere (
    ID INT IDENTITY(1,1) PRIMARY KEY, 
    ID_Rezervare INT,
    ID_Camera INT,
    FOREIGN KEY (ID_Rezervare) REFERENCES Rezervari(ID_Rezervare), 
    FOREIGN KEY (ID_Camera) REFERENCES Camere(ID_Camera) 
);


INSERT INTO Rezervari_Camere (ID_Rezervare, ID_Camera) VALUES 
(1, 1),
(2, 3),
(3, 6),
(4, 9),
(5, 10);

CREATE TABLE Plati (
    ID_Plata INT IDENTITY(1,1) PRIMARY KEY, 
    ID_Rezervare INT,
    ID_Client INT,
    ID_Angajat INT,
    Suma_Totala DECIMAL(10, 2),
    Data_Tranzactiei DATE,
    Tipul_Tranzactiei VARCHAR(10),
    FOREIGN KEY (ID_Rezervare) REFERENCES Rezervari(ID_Rezervare), 
    FOREIGN KEY (ID_Client) REFERENCES Clienti(ID_Client), 
    FOREIGN KEY (ID_Angajat) REFERENCES Angajati(ID_Angajat) 
);


INSERT INTO Plati (ID_Rezervare, ID_Client, ID_Angajat, Suma_Totala, Data_Tranzactiei, Tipul_Tranzactiei) VALUES 
(1, 1, 5, 350.00, '2024-02-14', 'card'),
(2, 3, 5, 600.00, '2024-04-12', 'cash'),
(3, 2, 5, 200.00, '2024-05-15', 'card'),
(4, 4, 5, 450.00, '2024-06-25', 'cash'),
(5, 5, 5, 300.00, '2024-07-02', 'card');


CREATE TABLE Produse (
    ID_Produs INT IDENTITY(1,1) PRIMARY KEY, 
    Nume NVARCHAR(50),
    Cantitate INT,
    Cantitate_Minima INT
);

INSERT INTO Produse (Nume, Cantitate, Cantitate_Minima) VALUES 
('Prosoape', 50, 20),
('Sapun', 100, 30),
('Hartie igienica', 80, 25),
('Produse de curatenie', 120, 40),
('Sampon', 60, 15),
('Balsam', 50, 20),
('Bureti de vase', 90, 30),
('Deodorant camera', 70, 25),
('Hartie pentru maini', 80, 25),
('Manusi de curatenie', 100, 30),
('Pungi pentru gunoi', 110, 35),
('Sapun lichid', 85, 30),
('Dezinfectant suprafete', 75, 20),
('Dezinfectant maini', 60, 15),
('Masti de protectie', 120, 40),
('Saci de aspirator', 70, 25),
('Solutie curatare geamuri', 80, 30),
('Periute de dinti', 90, 25),
('Pasta de dinti', 80, 20),
('Balsam de rufe', 100, 30);

CREATE TABLE Furnizori (
    ID_Furnizor INT IDENTITY(1,1) PRIMARY KEY, 
    Nume VARCHAR(50),
    Sediu VARCHAR(100)
);

INSERT INTO Furnizori (Nume, Sediu) VALUES 
('Furnizor1', 'Sediu1'),
('Furnizor2', 'Sediu2'),
('Furnizor3', 'Sediu3'),
('Furnizor4', 'Sediu4'),
('Furnizor5', 'Sediu5'),
('Furnizor6', 'Sediu6'),
('Furnizor7', 'Sediu7'),
('Furnizor8', 'Sediu8'),
('Furnizor9', 'Sediu9'),
('Furnizor10', 'Sediu10'),
('Furnizor11', 'Sediu11'),
('Furnizor12', 'Sediu12');


CREATE TABLE Detalii_Achizitie (
    ID INT IDENTITY(1,1) PRIMARY KEY, 
    ID_Produs INT,
    ID_Furnizor INT,
    Cantitate INT,
    FOREIGN KEY (ID_Produs) REFERENCES Produse(ID_Produs), 
    FOREIGN KEY (ID_Furnizor) REFERENCES Furnizori(ID_Furnizor) 
);

INSERT INTO Detalii_Achizitie (ID_Produs, ID_Furnizor, Cantitate) VALUES 
(21, 1, 100),
(22, 3, 150),
(23, 2, 80),
(24, 4, 200),
(25, 1, 120);


CREATE TABLE Achizitii (
    ID_Achizitie INT IDENTITY(1,1) PRIMARY KEY, -- ID-ul va fi generat automat și va fi cheia primară
    ID_Detalii_Achizitie INT,
    ID_Angajat INT,
    Data_Achizitiei DATE,
    FOREIGN KEY (ID_Detalii_Achizitie) REFERENCES Detalii_Achizitie(ID), -- Cheie externă către tabela Detalii_Achizitie
    FOREIGN KEY (ID_Angajat) REFERENCES Angajati(ID_Angajat) -- Cheie externă către tabela Angajati
);


INSERT INTO Achizitii (ID_Detalii_Achizitie, ID_Angajat, Data_Achizitiei) VALUES 
(6, 3, '2023-05-15'),
(2, 3, '2023-06-20'),
(3, 3, '2023-07-10'),
(4, 3, '2023-08-05'),
(5, 3, '2023-09-12');

CREATE TABLE Rapoarte (
    ID_Raport INT IDENTITY(1,1) PRIMARY KEY, 
    Inspector_Responsabil NVARCHAR(50),
    Verdict VARCHAR(20),
    Sectoare_Verificate VARCHAR(100),
    ID_Angajat INT,
    FOREIGN KEY (ID_Angajat) REFERENCES Angajati(ID_Angajat) 
);

INSERT INTO Rapoarte (Inspector_Responsabil, Verdict, Sectoare_Verificate, ID_Angajat) VALUES 
('Inspector1', 'Trecut', 'Receptie', 3),
('Inspector2', 'Neprelucrat', 'Bucatarie', 3),
('Inspector3', 'Neprelucrat', 'Spa', 3),
('Inspector4', 'Trecut', 'Camere', 3),
('Inspector5', 'Neprelucrat', 'Sali conferinte', 3),
('Inspector6', 'Trecut', 'Piscina', 3),
('Inspector7', 'Trecut', 'Zona exterioara', 3);

CREATE TABLE Controale_Calitate (
    ID_Control INT IDENTITY(1,1) PRIMARY KEY, 
    Tip_Control VARCHAR(50),
    Data_Controlului DATE,
    ID_Raport INT,
    FOREIGN KEY (ID_Raport) REFERENCES Rapoarte(ID_Raport) 
);

INSERT INTO Controale_Calitate (Tip_Control, Data_Controlului, ID_Raport) VALUES 
('Curatenie', '2023-10-15', 1),
('Igiena', '2023-10-20', 2),
('Verificare echipamente', '2023-10-25', 3),
('Siguranta', '2023-11-02', 4),
('Servicii oferite', '2023-11-10', 5);

CREATE TABLE Evenimente (
    ID_Eveniment INT IDENTITY(1,1) PRIMARY KEY, 
    Nume NVARCHAR(50),
    Descriere TEXT,
    Data_Inceput DATETIME,
    Data_Sfarsit DATETIME,
    Locatie NVARCHAR(100),
    Organizator NVARCHAR(50)
);

INSERT INTO Evenimente (Nume, Descriere, Data_Inceput, Data_Sfarsit, Locatie, Organizator) VALUES 
('Conferinta Anuala', 'Eveniment de networking si prezentare de noi tehnologii.', '2024-03-15 09:00', '2024-03-15 17:00', 'Sala de conferinte', 'Compania X'),
('Petrecere de Revelion', 'Sarbatoare de Anul Nou cu muzica live si artificii.', '2024-12-31 21:00', '2025-01-01 03:00', 'Salonul de evenimente', 'Hotel Management'),
('Seminariu Wellness', 'Zi de relaxare si activitati de wellness.', '2024-05-20 10:00', '2024-05-20 16:00', 'Spa & Centru Wellness', 'Fitness Experts'),
('Lansare de Produse', 'Prezentare de produse si sesiuni de teste pentru clienti.', '2024-08-10 14:00', '2024-08-10 18:00', 'Holul principal', 'Compania Y'),
('Curs de Gatit', 'Sesiuni practice de gatit sub indrumarea unui bucatar celebru.', '2024-06-05 17:30', '2024-06-05 20:30', 'Bucataria restaurantului', 'Chef Gourmet');


CREATE TABLE Restaurant (
    ID_Mancare INT IDENTITY(1,1) PRIMARY KEY,
    Nume NVARCHAR(50),
    Descriere TEXT,
    Pret DECIMAL(10, 2)
);

INSERT INTO Restaurant (Nume, Descriere, Pret) VALUES 
('Omeleta la micul dejun', 'Omeleta cu oua proaspete, branza, sunca si legume.', 12.99),
('Pui cu cartofi', 'Piept de pui la gratar cu cartofi prajiti si salata de legume.', 25.99),
('Pizza Quattro Stagioni', 'Pizza cu sos de rosii, mozzarella, sunca, ciuperci, masline si ansoa.', 18.50),
('Salata Caesar cu pui', 'Salata de la gratar cu piept de pui, crutoane, parmezan si dressing Caesar.', 15.75),
('Filet de somon', 'Filet de somon la gratar cu garnitura de orez si sparanghel.', 28.00),
('Tiramisu', 'Desert clasic italian cu strat de biscuiti Savoiardi si mascarpone.', 12.99);




