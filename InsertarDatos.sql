SET IDENTITY_INSERT ActivityTypes ON;
INSERT INTO ActivityTypes (Id, Name, NumberInstructors) VALUES 
(1, 'GAP', 2),
(2, 'NATACIÓN', 1),
(3, 'PINTURA', 1),
(4, 'BODYPUMP', 3);
SET IDENTITY_INSERT ActivityTypes OFF;

SET IDENTITY_INSERT Instructors ON;
INSERT INTO Instructors (Id, Name, Email, Phone, PhotoUrl) VALUES 
(1, 'Juan Magan', 'juan_magan@gmail.com', '654121212', 'https://foto-perfil-JM.jpg'),
(2, 'Antón Álvarez', 'anton_alvarez@hotmail.com', '663423121', 'https://foto-perfil-AA.jpg'),
(3, 'Alba Farelo', 'alba_farelo@cuatrovientos.org', '654646464', 'https://foto-perfil-AF.jpg');
SET IDENTITY_INSERT Instructors OFF;

SET IDENTITY_INSERT Activities ON;
INSERT INTO Activities (Id, MaxParticipants, DateStart, DateEnd, ActivityTypeId) VALUES 
(1, 20, '2026-11-10 10:00:00', '2026-11-10 11:00:00', 1),
(2, 15, '2026-11-10 11:30:00', '2026-11-10 12:30:00', 2);
SET IDENTITY_INSERT Activities OFF;

INSERT INTO ActivityInstructor (ActivitiesId, InstructorsId) VALUES 
(1, 1),
(1, 2),
(2, 3);
