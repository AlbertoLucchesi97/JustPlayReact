Feature: VideogamesController

A controller that communicate with a database in order to get and set data.

*/

Scenario: GetVideogames_ReturnsListOfVideogames
	Given the VideogamesController
	When I query all videogames
	Then the Result should be a Task with a list of Videogames

Scenario: GetVideogamesBySearching_WithValidParameter_ReturnsVideogames
	Given the VideogamesController
	When I query all videogames by searching for a videogame with a valid parameter
	Then the Result should be a Task with a list of the found Videogames

Scenario: GetVideogamesBySearching_WithInvalidParameter_ReturnsAnEmptyVideogameList
	Given the VideogamesController
	When I query all videogames by searching for a videogame with an invalid parameter
	Then the Result should be a Task with an empty list of found Videogames

Scenario: GetVideogamesByFiltering_WithNullSearchAndValidSortParameters_ReturnsVideogamesFiltered
	Given the VideogamesController
	When I query all videogames with a null search and valid sort parameter
	Then the Result should be a Task with a sorted list of the found Videogames
	
Scenario: GetVideogame_WithWorkingId_ReturnsAVideogame
	Given the VideogamesController
	When I query a videogame with a working id
	Then the Result should be a Task with the found Videogame
	
Scenario: GetVideogame_WithNonWorkingId_ReturnsNotFound
	Given the VideogamesController
	When I query a videogame with a non working id
	Then the Result of GetVideogame should be a Task with a NotFound message

Scenario: PostVideogame_WithVideogameInstance_ReturnsTheVideogameAdded
	Given the VideogamesController
	When I post a videogame with a videogame instance
	Then the Result should be a Task with a ActionResult of Videogame
	
Scenario: PostVideogame_WithNullParameter_ReturnsBadRequest
	Given the VideogamesController
	When I post a videogame with a null parameter
	Then the Result of PostVideogame should be a Task with a BadRequest
	
Scenario: PutVideogame_WithIdAndVideogameUpdatedInstance_ReturnsUpdateVideogame
	Given the VideogamesController
	When I call the PutVideogame method with a valid videogame id and a videogame instance
	Then the Result should be a Task with the UpdatedVideogame
	
Scenario: PutVideogame_WithIdAndNullParameter_ReturnsNotFound
	Given  the VideogamesController
	When I call the PutVideogame method with a non working id and a null parameter
	Then the Result of PutVideogame should be a Task with NotFound error
	
Scenario: DeleteVideogame_WithId_ReturnsNoContent
	Given the VideogamesController 
	When I call the DeleteVideogame method with a valid videogame id
	Then the Result should be a Task with a NoContent
	
Scenario: DeleteVideogame_WithNoValidId_ReturnsNotFound
	Given the VideogamesController 
	When I call the DeleteVideogame method with a non working id
	Then the Result of DeleteVideogame should be a Task with a NotFound