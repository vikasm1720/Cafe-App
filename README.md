# Cafe-App

A full-stack application to manage cafés and their employees, built using **.NET 8**, **MSSQL**, and **React.js**. This application demonstrates the use of **Clean Architecture**, **CQRS**, **Mediator Pattern**, and **Autofac** for dependency injection.

### Prerequisites
- Install **.NET 8 SDK**.
- Install **SQL Server** 
- Install a code editor like **Visual Studio** or **VS Code**.
- Install **Node.js** for frontend

### Steps to run the code
1. Clone the repo.
2. CafeOps:Backend implementation.
    - Clone the repository:
    - Navigate to the CafeOps folder:
    - Run the database script:
    - Navigate to CafeOps/CafeOps.DAL/DatabaseScripts.sql.
    - Execute the script in SQL Server to create the required database and tables.
    - Open the CafeOps solution in Visual Studio and build the project.
    - Run the solution to start the backend server. The Swagger UI should open automatically in a browser window. Keep this browser instance running.

3. CafeApp: front end implementation.
    - Open a terminal, cmd, git bash or VS code. Navigate to cafeapp folder. 
    - **_Run NPM install_** to install the required Node.js modules.
    - **_Run NPM start_** To Start run the react app.
    - On successful setup, you should see a landing page with buttons as per the requirements.

4. If required to change the port number then please update the port number in following place.
    - Open CafeOps\CafeOps.API\Properties\launchSettings.json change the port number for localhost under applicationUrl parameter.
    - Open cafeapp/package.json change the proxy port number. 

Note:
- I have implemented validation on frontend, backend and in database for phone number and gender.
- Seed data has  Bangalore, Boston, Singapore location.


If you face any issues running the project, I would be happy to provide a walkthrough.


