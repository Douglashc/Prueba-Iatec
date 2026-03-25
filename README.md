# 📅 Agenda IATec - Sistema de Gestión de Eventos

Este proyecto es una solución **Full Stack** diseñada para la gestión de agendas personales y compartidas.

---

## 🛠️ Stack Tecnológico

### **Backend (.NET 8)**
* **Arquitectura:** Clean Architecture (WebAPI, Application, Domain, Infrastructure).
* **Base de Datos:** MySQL con Entity Framework Core.
* **Seguridad:** Autenticación JWT (JSON Web Tokens).

### **Frontend (Angular 16+)**
* **Estilos:** Tailwind CSS.
* **Estado:** Reactive Forms y RxJS.
* **Seguridad:** AuthGuards e Interceptores para manejo de Tokens.

---

## 🚀 Guía de Instalación y Configuración

### 1. Requisitos Previos
* **MySQL Server** instalado y en ejecución.
* **.NET SDK** (Versión 8.0).
* **Node.js** (Versión 18 LTS) y **Angular CLI**.

### 2. Preparación de la Base de Datos (MySQL)
Antes de iniciar el backend, debes crear el contenedor para la base de datos:
1. Abre tu terminal de MySQL o Workbench.
2. Ejecuta el siguiente comando:
   ```sql
   CREATE DATABASE agenda_iatec;

### 3. Preparación del Backend 
1. Entra a la carpeta del Backend: cd AgendalATec
2. Ejecuta el siguiente comando para descargar las dependencias: dotnet restore
3. Ejecuta las migraciones: dotnet ef database update --project Agenda.Infrastructure --startup-project Agenda.Api
4. Inicia el servidor: dotnet run --project Agenda.Api

### 4. Preparación del Frontend
1. Entra a la carpeta del Frontend: cd AgendaFront
2. Ejecuta el siguiente comando para descargar las dependencias: npm install
3. Inicia el proyecto: ng serve
4. Usuarios con datos de prueba: username: admin | password: admin123, username: juan | password: juan123  