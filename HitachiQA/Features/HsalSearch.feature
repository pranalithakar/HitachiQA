Feature: HsalSearch
	In order to find information about automation
	As a potential customer
	I want to search HSAL

@HSALSearch
Scenario: Navigate to HSAL and search for automation
  Given user landed on HSAL homepage
	When user searches HSAL homepage for Automation
	And user attempts to search HSAL homepage
   Then user presented with search results from HSAL
