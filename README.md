# LibraryManagerServer

## Description

The Library Manager API is a RESTful API designed for managing a library. It allows you to perform operations for adding, removing, and updating information about books, authors, and users. The API also supports searching and retrieving information on various entities.

## Features

- Manage books (add, remove, update, search(tite/author))
- Manage authors (add, remove, search)
- Retrieve lists of all books, authors
- Search by various criteria

## Technologies

- **Backend:** ASP.NET Core
- **Database:** SQL Server
- **Logging:** NLog

- ## Installation

Clone the repository:
https://github.com/Blackdeer2/LibraryManagerServer.git

##Create Db
``CREATE TABLE IF NOT EXISTS `LibraryManager`.`Author` (
  `AuthorId` VARCHAR(36) NOT NULL,
  `Name` VARCHAR(60) NOT NULL,
  PRIMARY KEY (`AuthorId`))
ENGINE = InnoDB;``

``CREATE TABLE IF NOT EXISTS `LibraryManager`.`Book` (
  `BookId` VARCHAR(36) NOT NULL,
  `Title` VARCHAR(60) NOT NULL,
  `AuthorId` VARCHAR(36) NOT NULL,
  PRIMARY KEY (`BookId`),
  INDEX `fk_Book_Author_idx` (`AuthorId` ASC) VISIBLE,
  CONSTRAINT `fk_Book_Author`
    FOREIGN KEY (`AuthorId`)
    REFERENCES `LibraryManager`.`Author` (`AuthorId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;``

Update the connection string in `appsettings.json`.
"mysqlconnection": {
  "connectionString": "server=localhost;userid=root;password=yourpassword;database=librarymanager;"

## Endpoints

Test through the Postman program

### Books
**Add book**
 POST http://localhost:5000/api/book/author/{authorId}"

 **View a list of books**
 GET http://localhost:5000/api/book

  **Update book**
 PUT http://localhost:5000/api/book/{id}

   **Delete book**
 DELETE http://localhost:5000/api/book/{id}

   **Filter book by title**
 GET http://localhost:5000/api/book/title/{title}

  **Filter book by author id**
 GET http://localhost:5000/api/book/author/{authorId}

 **Add author**
 POST http://localhost:5000/api/author

  **Delete author and books**
 DELETE http://localhost:5000/api/author/{id}
