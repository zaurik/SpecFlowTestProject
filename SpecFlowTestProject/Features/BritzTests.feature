Feature: BritzTests

Tests to be run on Britz.com

Scenario: Assert the number of records on the rental booking page
	Given that I'm on the "https://www.google.co.nz" page
	And the region is set to "New Zealand"
	When I search for "britz"
	Then I should see a search result linking to the URL "britz.com" within 1 page of search results
	When I click on the search result linking to the URL "britz.com"
	And I select the destination as "Australia" on Britz
	#Date should be in the format of 'd/m/yyyy'
	And I set the Pick Up Date as "1/12/2022" on Britz	
	And I set the Drop Off Date as "20/12/2022" on Britz
	And I set the Pick Up Location as "Sydney" on Britz
	And I set the Drop Off Location as "Sydney" on Britz
	And I set the Drivers License as "Canada" on Britz
	And I click on the Search button on Britz
	Then the search results count should be 23

@ApiTest
Scenario: Assert API Response is OK and valid results count
	Given that I want to send a request for pricing to "Britz" 
	When I set the Britz request parameter "Country" as "Australia"
	And I set the Britz request parameter "pick up location" as "Sydney"
	# Date should be in the format d/m/yyyy (e.g- 23/11/2022)
	And I set the Britz request parameter "pick up date" as "1/12/2022"
	# Date should be in the format d/m/yyyy (e.g- 23/11/2022)
	And I set the Britz request parameter "drop off date" as "20/12/2022"
	And I set the Britz request parameter "drop off location" as "Sydney"
	And I set the Britz request parameter "driver's license" as "Canada"
	And I set the Britz request parameter "number of adults" as "1"
	And I send the Britz pricing request
	Then the Britz response status code should be OK
	And the number of valid Britz search results should be 23
