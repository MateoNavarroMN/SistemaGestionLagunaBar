# 🍽️ Sistema de Gestión Gastronómica - La Laguna Bar

[![Ver Demo](https://img.shields.io/badge/Ver_Video_Demo-FF0000?style=for-the-badge&logo=youtube&logoColor=white)](https://youtu.be/xCLFh5Z9lqs)

Aplicación de escritorio desarrollada en **C# (WinForms)** y **SQL Server** para administrar de forma integral "La Laguna Bar", un resto-bar ubicado en Monte Cristo, Córdoba. 

El sistema fue diseñado para digitalizar la toma de pedidos, evitar pérdidas de información, agilizar el cobro y proporcionar estadísticas clave para la toma de decisiones comerciales.

## 🚀 Características Principales

- **Control de Mesas en Tiempo Real:** Visualización del estado de las mesas (ocupadas/disponibles) y asignación clara de mozos a cada una.
- **Gestión de Pedidos:** Registro digital de los consumos por mesa para evitar errores en la cocina y en la entrega.
- **Facturación y Cobro:** Cálculo automático del total consumido y emisión de tickets detallados al cerrar la cuenta.
- **Historial de Ventas:** Registro persistente en la base de datos para consultar ingresos diarios, desempeño por mozo y pedidos anteriores.
- **Módulo de Estadísticas:** Análisis de datos para identificar los menús más vendidos, horarios pico y el promedio de gasto por mesa.

## 🛠️ Tecnologías Utilizadas

- **Frontend:** C# / Windows Forms (WinForms)
- **Backend/Lógica:** .NET Framework
- **Base de Datos:** Microsoft SQL Server (Arquitectura Relacional)
- **Patrones:** CRUD (ABM) completo para administración del sistema.

## 🗄️ Estructura de la Base de Datos

El sistema cuenta con una base de datos relacional robusta que garantiza la integridad y consistencia de la información. Las entidades principales incluyen:
- **Gestión de RRHH:** `Empleados`, `Niveles` (Roles), `Barrios`.
- **Gestión de Salón:** `Mesas`, `Pedidos`, `Detalle_Pedidos`.
- **Gestión de Productos:** `Menus`, `Categorias_Menus`.
- **Facturación:** `Ventas`.

## 📦 Cómo probar el proyecto localmente

Dado que es una aplicación de escritorio con base de datos local, sigue estos pasos para ejecutarla:

1. Clona este repositorio:
   
   ```bash
   git clone https://github.com/MateoNavarroMN/SistemaGestionLagunaBar

   Base de Datos: En la carpeta /Database encontrarás el script schema.sql. Ejecútalo en tu entorno de SQL Server Management Studio (SSMS) para recrear las tablas y relaciones.

2. Abre la solución .sln en Visual Studio.

3. Modifica la cadena de conexión (ConnectionString) apuntando a tu servidor local de SQL Server.

4. Compila y ejecuta el proyecto (F5).

## 👨‍💻 Autores
Proyecto desarrollado por Mateo Navarro y Felipe Molina como trabajo práctico integral de Laboratorio de Programación 3.
