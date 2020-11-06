Feature: GoogleSearch
	Login biBerk home page with valid credentials



@GoogleSearch
Scenario: Navigate to Google and search for automation
  Given user landed in Google.com
	When user searches for Automation Framework
	And user attempts to search
   Then user login successfully to biBerk page
