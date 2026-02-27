# Foro Militar

## Requisitos
- Visual Studio 2022
- .NET Framework 4.8
- SQL Server LocalDB (viene con Visual Studio)

## Pasos para ejecutar al clonar

1. Clonar el repositorio
   git clone https://github.com/tu-usuario/Foro-Militar.git

2. Abrir Foro Militar.sln en Visual Studio 2022

3. Crear las tablas — abrir SQL Server Object Explorer
   - Clic derecho en (localdb)\MSSQLLocalDB → Nueva consulta
   - Abrir Scripts/CreateDatabase.sql → Ejecutar (Ctrl+Shift+E)

4. Cargar datos de prueba
   - Abrir Scripts/SeedData.sql → Ejecutar (Ctrl+Shift+E)

5. Ejecutar el proyecto → F5