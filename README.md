# GlassLewisWebApi
 Coding interview challenge

---- HOW TO RUN --------

You can clone this repository on your system. 
It has Swagger Front end implemented to test endpoints in browser.
http://localhost:5025/swagger/index.html

You can run GlassLewisWebApi project and it should automatically Swagger index page. 

MySQL database: 
You will need to run following script to create a database and server should be defined as below as mentioned in the connection string: 

"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CompanyDatabase;User=root;Password=root;"
}

Or you can change the server, username & password according to your machine :)

SQL Script: 

-- Create the database

CREATE DATABASE IF NOT EXISTS CompanyDatabase;


-- Switch to the database

USE CompanyDatabase;


-- Create the Companies table

CREATE TABLE Companies (
    Id INT PRIMARY KEY auto_increment,
    Name VARCHAR(255) NOT NULL,
    Ticker VARCHAR(10) NOT NULL,
    Isin VARCHAR(12) NOT NULL,
    Exchange VARCHAR(255) NOT NULL,
    Website VARCHAR(255),
    CONSTRAINT UC_Isin UNIQUE (Isin)
);

----END OF SQL SCRIPT--------

AUTHENTICATION: 

CompanyController can't be used without Authorization. 
You can get JWT token using api/Auth/login endpoint.
Use the below payload: 

{
  "username": "testuser",
  "password": "testpassword"
}


this should return a token.
(For the sake of simplicity and not creating another database to store users I have hardcoded user credentials in the code, Of course the connection string and user credentials will never be visible in production code :) )

You can click on Authorize button below on Swagger to automatically add Authorization header to subsequent api requests to CompanyController
<img width="1465" alt="Screenshot 2024-08-15 at 18 59 10" src="https://github.com/user-attachments/assets/4fcae904-1cec-497c-826a-5663d75e05d9">
You must follow the format of "Bearer <your-jwt-token>"
After that CompanyController should be accessible. 

A lot more could be done to create code more loosely coupled for example removing logic from AuthController and using an AuthService instead. But due to time restraints I couldn't implement those features. We are short on staff at my current job because many people are on holidays. 
