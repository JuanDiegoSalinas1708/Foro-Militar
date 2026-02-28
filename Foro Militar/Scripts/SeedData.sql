-- ============================================================
--  PASO 2: Ejecutar esto después de CreateDatabase.sql
--  Llena todas las tablas con datos de prueba
-- ============================================================

USE Foro;
GO

DELETE FROM UserCommunities;
DELETE FROM Votes;
DELETE FROM Comments;
DELETE FROM PostCategories;
DELETE FROM Posts;
DELETE FROM Categories;
DELETE FROM Communities;
DELETE FROM Users;

DBCC CHECKIDENT ('Users',       RESEED, 0);
DBCC CHECKIDENT ('Communities', RESEED, 0);
DBCC CHECKIDENT ('Categories',  RESEED, 0);
DBCC CHECKIDENT ('Posts',       RESEED, 0);
DBCC CHECKIDENT ('Comments',    RESEED, 0);
DBCC CHECKIDENT ('Votes',       RESEED, 0);
GO

INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES
('admin',        'admin@foro.com',    'HASH_ADMIN', 'Admin'),
('coronel_rojo', 'coronel@foro.com',  'HASH_USER',  'User'),
('tankista99',   'tankista@foro.com', 'HASH_USER',  'User'),
('general_k',    'generalk@foro.com', 'HASH_USER',  'User'),
('marina_war',   'marina@foro.com',   'HASH_USER',  'User');
GO

INSERT INTO Communities (Name, Description, CreatedByUserId) VALUES
('Historia Militar',   'Conflictos y batallas historicas documentadas',            1),
('Geopolitica',        'Analisis de poder, alianzas y relaciones internacionales', 1),
('Tecnologia Militar', 'Armamento, vehiculos, aviones y tecnologia de defensa',    1),
('Conflictos Activos', 'Guerras y tensiones que ocurren en el mundo hoy',          1);
GO

INSERT INTO Categories (Name, ColorHex, Description, CommunityId) VALUES
('Actualidad',  '#E74C3C', 'Conflictos en curso',                  4),
('Historia',    '#3498DB', 'Eventos militares del pasado',          1),
('Analisis',    '#9B59B6', 'Opinion y analisis estrategico',        2),
('Tecnologia',  '#27AE60', 'Equipamiento y avances militares',      3),
('Geopolitica', '#E67E22', 'Relaciones de poder entre naciones',    2),
('Naval',       '#1ABC9C', 'Conflictos y operaciones navales',      1),
('Aereo',       '#2980B9', 'Aviacion militar y operaciones aereas', 3);
GO

INSERT INTO Posts (Title, Content, Image, Country, UserId, CommunityId, MainCategoryId, CreatedAt, IsDeleted) VALUES
('Conflicto en Gaza: Situacion actual','Desde el ataque sorpresa de Hamas el 7 de octubre de 2023, Israel inicio una campana militar masiva en la Franja de Gaza con mas de 1.5 millones de desplazados.',NULL,'Israel, Gaza',1,4,1,'2023-10-07',0),
('Guerra en Ucrania: Tres anos de conflicto','La invasion rusa de Ucrania iniciada el 24 de febrero de 2022 es el conflicto armado mas grande en Europa desde la Segunda Guerra Mundial.',NULL,'Ucrania, Rusia',2,4,1,'2022-02-24',0),
('Tensiones en el Mar de China Meridional','China continua militarizando islas artificiales. Filipinas, Vietnam y Taiwan disputan soberania sobre estas aguas vitales para el comercio mundial.',NULL,'China, Filipinas, Vietnam, Taiwan',3,4,1,'2024-03-15',0),
('La Guerra de Malvinas (1982)','El 2 de abril de 1982 Argentina ocupo las Islas Malvinas. Tras 74 dias de combates Argentina se rindio el 14 de junio dejando 649 argentinos y 255 britanicos muertos.',NULL,'Argentina, Reino Unido',4,1,2,'1982-04-02',0),
('La Guerra de Corea: El conflicto olvidado','Iniciada el 25 de junio de 1950. Tres anos de combates terminaron en un armisticio en 1953. Tecnicamente las dos Coreas siguen en guerra.',NULL,'Corea del Norte, Corea del Sur, EE.UU.',5,1,2,'1950-06-25',0),
('Batalla de Stalingrado: Punto de quiebre WWII','Entre agosto 1942 y febrero 1943 alemanes y sovieticos combatieron. El 6 Ejercito aleman quedo rodeado y Paulus se rindio con 91,000 soldados.',NULL,'Alemania, Union Sovietica',1,1,2,'1942-08-23',0),
('El F-35: El avion de combate mas avanzado','El Lockheed Martin F-35 Lightning II es un caza de quinta generacion con capacidades stealth. Su costo supera los 80 millones de dolares.',NULL,'EE.UU., OTAN',3,3,4,'2024-01-10',0),
('Drones en la guerra moderna: Bayraktar TB2','El dron turco TB2 demostro su eficacia en Ucrania destruyendo tanques rusos. Con apenas 5 millones de dolares redefine la superioridad aerea.',NULL,'Turquia, Ucrania',2,3,4,'2024-02-20',0),
('La OTAN tras la guerra de Ucrania','La invasion rusa provoco la mayor expansion de la OTAN en decadas. Finlandia y Suecia históricamente neutrales solicitaron su adhesion.',NULL,'OTAN, Rusia, Europa',4,2,5,'2024-04-01',0),
('Iran vs Israel: Guerra en las sombras','El enfrentamiento escalo en 2024 con ataques directos de misiles y drones. Iran apoya a Hamas, Hezbollah y los Houthis en el llamado Eje de Resistencia.',NULL,'Iran, Israel',5,2,5,'2024-04-15',0);
GO

INSERT INTO PostCategories (PostId, CategoryId) VALUES
(1,1),(1,5),(2,1),(2,5),(3,1),(3,5),
(4,2),(4,6),(5,2),(6,2),
(7,4),(7,7),(8,4),(8,7),
(9,3),(9,5),(10,3),(10,5);
GO

INSERT INTO Comments (Content, UserId, PostId, ParentCommentId, CreatedAt) VALUES
('Situacion devastadora. El impacto humanitario es incalculable.',         2,1,NULL,'2023-10-08'),
('Hay que recordar que el ataque de Hamas mato a 1,200 israelies.',        3,1,1,   '2023-10-08'),
('Ambas cosas pueden ser ciertas, afecta a civiles inocentes.',            4,1,2,   '2023-10-09'),
('Putin subestimo completamente la resistencia ucraniana.',                5,2,NULL,'2022-02-25'),
('Los sistemas HIMARS cambiaron la dinamica del conflicto.',               1,2,4,   '2022-09-10'),
('Nadie ve una salida diplomatica realista. Puede durar anos.',            2,2,NULL,'2023-01-15'),
('Un conflicto que marco a toda una generacion de argentinos.',            3,4,NULL,'2023-05-02'),
('El hundimiento del Belgrano sigue siendo muy debatido.',                 4,4,7,   '2023-05-03'),
('La recuperacion britanica de las islas fue una hazana logistica.',       5,4,NULL,'2023-05-10'),
('El F-35 tuvo anos de retrasos pero el resultado es increible.',          2,7,NULL,'2024-01-11'),
('Rusos y chinos ya tienen cazas de 5ta generacion propios.',              3,7,10,  '2024-01-12'),
('El TB2 fue efectivo al inicio pero Rusia adapto su guerra electronica.', 4,8,NULL,'2024-02-21'),
('Aun asi demostraron que drones baratos pueden neutralizar equipos caros.',5,8,12, '2024-02-22');
GO

INSERT INTO Votes (UserId, PostId, CommentId, VoteType) VALUES
(2,1,NULL,1),(3,1,NULL,1),(4,1,NULL,1),(5,1,NULL,-1),
(2,2,NULL,1),(3,2,NULL,1),(4,2,NULL,1),(5,2,NULL,1),
(1,3,NULL,1),(2,3,NULL,-1),
(3,4,NULL,1),(4,4,NULL,1),(5,4,NULL,1),
(1,5,NULL,1),(2,5,NULL,1),
(1,7,NULL,1),(3,7,NULL,1),(4,7,NULL,1),
(1,8,NULL,1),(2,8,NULL,1),(5,8,NULL,1),
(1,NULL,1,1),(3,NULL,1,1),
(4,NULL,2,1),(5,NULL,2,-1),
(1,NULL,4,1),(2,NULL,4,1),
(3,NULL,7,1),
(1,NULL,10,1),(2,NULL,12,1);
GO

INSERT INTO UserCommunities (UserId, CommunityId) VALUES
(1,1),(1,2),(1,3),(1,4),
(2,1),(2,4),
(3,1),(3,3),
(4,2),(4,4),
(5,1),(5,2),(5,3);
GO

SELECT 'Users'            AS Tabla, COUNT(*) AS Registros FROM Users
UNION ALL SELECT 'Communities',     COUNT(*) FROM Communities
UNION ALL SELECT 'Categories',      COUNT(*) FROM Categories
UNION ALL SELECT 'Posts',           COUNT(*) FROM Posts
UNION ALL SELECT 'PostCategories',  COUNT(*) FROM PostCategories
UNION ALL SELECT 'Comments',        COUNT(*) FROM Comments
UNION ALL SELECT 'Votes',           COUNT(*) FROM Votes
UNION ALL SELECT 'UserCommunities', COUNT(*) FROM UserCommunities;
GO
