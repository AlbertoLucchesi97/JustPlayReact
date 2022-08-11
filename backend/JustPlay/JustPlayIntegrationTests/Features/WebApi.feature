Feature: WebApi

The WebApi that talks with the server
Background: Create App and Client
Given an App and a Client

Scenario: Call the WebApi to get the list of all videogames
	When I call the Api to get all the videogames
	Then The Status Code should be OK
	
Scenario: Call the WebApi to get a list of sorted videogames
	When I call the Api to get a list of sorted videogames
	Then The Status Code should be OK
	
Scenario: Call the WebApi to get a specific videogame
	When I call the Api to get a specific videogame
	Then The Status Code should be OK
	
Scenario: Call the WebApi to get a specific videogame with a wrong id
	When I call the Api to get a specific videogame with a wrong id
	Then The Status Code should be Not Found

Scenario: Call the WebApi to get a list of searched videogames
	When I call the Api to get a list of searched videogames
	Then The Status Code should be OK

Scenario: Call the WebApi to post a videogame
	When I call the Api to post a videogame
	Then The Status Code should be Created

Scenario: Call the WebApi to put a videogame
	When I call the Api to put a videogame
	Then The Status Code should be OK

Scenario: Call the WebApi to delete a videogame
	When I call the Api to delete a videogame
    Then The Status Code should be No Content

Scenario: Call the WebApi to delete a videogame with a wrong id
	When I call the Api to delete a videogame with a wrong id
	Then The Status Code should be Not Found

Scenario: Call the WebApi to get a user
	When I call the Api to get a user
	Then The Status Code should be OK
	
Scenario: Call the WebApi to get a user wishlist
	When I call the Api to get a user wishlist
	Then The Status Code should be OK
	
Scenario: Call the WebApi to get a user owned videogames
	When I call the Api to get a user owned videogames
	Then The Status Code should be OK
	
Scenario: Call the WebApi to add a videogame to wishlist
	When I call the Api to add a videogame to wishlist
	Then The Status Code should be OK
	
Scenario: Call the WebApi to remove a videogame from wishlist
	When I call the Api to remove a videogame from wishlist
	Then The Status Code should be OK
	
Scenario: Call the WebApi to add a videogame to owned
	When I call the Api to add a videogame to owned
	Then The Status Code should be OK

Scenario: Call the WebApi to remove a videogame from owned
	When I call the Api to remove a videogame from owned
	Then The Status Code should be OK
	