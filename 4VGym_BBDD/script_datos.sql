-- Script para poblar la base de datos de 4VGYM con datos de prueba iniciales

-- 1. TIPOS DE ACTIVIDAD
SET IDENTITY_INSERT ActivityTypes ON;
INSERT INTO ActivityTypes (Id, Name, NumberInstructors) VALUES 
(1, 'BodyPump', 2),
(2, 'Spinning', 1),
(3, 'Pilates', 1),
(4, 'CrossFit', 3);
SET IDENTITY_INSERT ActivityTypes OFF;

-- 2. INSTRUCTORES
SET IDENTITY_INSERT Instructors ON;
INSERT INTO Instructors (Id, Name, Email, Phone, PhotoUrl) VALUES 
(1, 'Miguel Goyena', 'miguel_goyena@cuatrovientos.org', '654121212', 'https://img.magnific.com/vector-premium/imagen-perfil-avatar-hombre-aislada-fondo-imagen-profil-avatar-hombre_1293239-4841.jpg'),
(2, 'María Martín', 'maria_martin@cuatrovientos.org', '663423121', 'https://img.magnific.com/vector-premium/avatar-mujer-sonriente_937492-6135.jpg'),
(3, 'Iban Sarría', 'iban_sarria@cuatrovientos.org', '654646464', 'https://img.magnific.com/vector-premium/imagen-perfil-avatar-hombre-aislada-fondo-imagen-profil-avatar-hombre_1293239-4861.jpg');
SET IDENTITY_INSERT Instructors OFF;

-- 3. ACTIVIDADES
SET IDENTITY_INSERT Activities ON;
INSERT INTO Activities (Id, MaxParticipants, DateStart, DateEnd, ActivityTypeId) VALUES 
(1, 20, '2026-11-10 10:00:00', '2026-11-10 11:00:00', 1),
(2, 15, '2026-11-10 11:30:00', '2026-11-10 12:30:00', 2);
SET IDENTITY_INSERT Activities OFF;

-- 4. RELACIÓN ACTIVIDAD - INSTRUCTORES (Muchos a Muchos)
INSERT INTO ActivityInstructor (ActivitiesId, InstructorsId) VALUES 
(1, 1),
(1, 2),
(2, 3);
