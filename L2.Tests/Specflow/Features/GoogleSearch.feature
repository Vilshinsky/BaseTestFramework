@googleSearch
Feature: GoogleSearch
	Description: The feature allows to perform search 
		via Google algorithm 
		by the text value

Scenario: Should be able to find website by name
	Given I am on the search page
	When I enter 'Selenium.dev' search value
	Then expected link 'Selenium.dev' should be listed in the suggestions