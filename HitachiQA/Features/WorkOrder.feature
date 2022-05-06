Feature: Work Order
	This Feature targets scenarios around FSA work order 


Scenario: User Creates a new work order
	Given user is landed on Apps page
	And The user nagivates to work order page 
	And user creates a new work order
	And User enters Service Account 'Service Account name'
	And User Enters Functional Location as 'Funtional  location'
	And  User enters Work order type as 'work order type'
	And User click on Save and close button
	Then Work order is created and saved
