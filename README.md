EPF Assessment documentation

Description

The web application allows for the user (Customer) to capture their personal 
information and upload their financial income and expenses in the form of an 
excel spreadsheet file. The results would the user’s information and a temporal 
graph showing the customers income and expenditure for the last 12 months.

Tools

1.	The solution uses C#’s Asp.net core mvc template for both the backend and the 
frontend, which combines html, javascript and css to make the application fully 
functional.
2.	The solution does not permanently store the any information in a database, 
but rather stores information temporarily in the memory.
3.	The application extracts data from an excel spreadsheet document. The use of 
fastExcel library allows the server to read the document with only one sheet and 
starts reading from the second row assuming the first row is reserved for column 
titles.

Running the project

To run the app, you will need to load the project using visual studio and run the 
app the normal way. The app is not dependent on any external resources and can run 
offline.
