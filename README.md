# .NETPraksa

## Library Management Web API #1
### Description
Create a web API that allows users to manage a small library system, covering basic database operations, security, and business logic. The system will track books, library members, and book rentals, ensuring that all actions adhere to specific rules.

### Requirements

#### Member management
In order to differentiate each member the system should allow members to register and log in. The following information should be correlated to the user:

* First name
* Last name
* Username (unique)
* Password
* Date of birth

In order to have user management the system should have the following features:

* Registration
* Login

In order to differentiate API calls (which member is the caller) JWT management will be necessary.


#### Book management
A library would be nothing without books, so let's define them. Each book should have the following information:

* Book name
* Authors
* Publication date
* ISBN (unique) 13 digit

Authors should be uniquely identified by their first and last names. Additionaly, their birth date is of a big importance.

#### Book borrowing
There are a couple of rules that have to be fulfilled in order to have a consistent system, and they include the following:

* Only available books can be borrowed
* A member can borrow up to 5 books at a time.
* When a book is borrowed, the book status should be "Rented"
* Members can borrow a book for no more than 15 days.

#### Returning books
* Each book rental should have the date when it was created, and when the books were returned.
