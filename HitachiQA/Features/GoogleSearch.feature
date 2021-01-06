Feature: GoogleSearch
	Navigate to Google and search for automation

@GoogleSearch
Scenario: Navigate to Google and search for automation
  Given user landed in Google.com
	When user searches for Automation Framework
	And user attempts to search
   Then user presented with search results
