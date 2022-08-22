Feature: MauiTests

Tests to be run on maui-rentals.com

Scenario: Assert the number of records on the rental booking page
	Given that I'm on the "https://www.google.co.nz" page
	And the region is set to "New Zealand"
	When I search for "maui"
	Then I should see a search result linking to the URL "maui-rentals.com" within 3 pages of search results
	When I click on the search result linking to the URL "maui-rentals.com"
	And I select the destination as "New Zealand"
	#Date should be in the format of 'd/m/yyyy'
	And I set the Pick Up Date as "1/12/2022"
	And I set the Drop Off Date as "20/12/2022"
	And I set the Pick Up Location as "Christchurch"
	And I set the Drop Off Location as "Auckland"
	And I set the Drivers License as "Canada"
	And I click on the Search button
	Then the search results count should be 21

@ApiTest
Scenario: Assert API Response is OK and valid results count
	Given that I want to send a request for pricing to "Maui" 
	When I set the request parameter "Country" as "New Zealand"
	And I set the request parameter "pick up location" as "Christchurch"
	# Date should be in the format d/m/yyyy (e.g- 23/11/2022)
	And I set the request parameter "pick up date" as "1/12/2022"
	# Date should be in the format d/m/yyyy (e.g- 23/11/2022)
	And I set the request parameter "drop off date" as "20/12/2022"
	And I set the request parameter "drop off location" as "Auckland"
	And I set the request parameter "driver's license" as "Canada"
	And I set the request parameter "number of adults" as "1"
	And I send the pricing request
	Then the response status code should be OK
	And the number of valid search results should be 21
