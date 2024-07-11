# MusicLibrary

This project was implemented using .Net 8, MongoDB and Angular.

## Prerequisites

Have 'Docker' installed on your machine.

## Build

Run `docker compose up --build` inside the project directory to build the project.

## Running tests

 - Navigate to `http://localhost:80` to return the application.
 - Navigate to `http://localhost:8081` to return the Mongo Express UI and view the Database table.
	- Username: `admin`
	- Password: `pass`
 - Navigate to `http://localhost:5000/api/Bands` to visualise the backend call for the Get.

 - Use `Postman` to test the requests that implement the CRUD operations.

## Closing the project

Run `docker compose down` inside the project directory to shut down the docker container and close the project.

## Further help

Feel free to reach me anytime via email if there are any clarifications that need to be made
