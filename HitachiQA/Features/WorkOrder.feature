Feature: Work Order
	This Feature targets scenarios around FSA work order 


Scenario: User Creates a new work order
	Given user is landed on Apps page
	And The user is on Work order page 
	And user click on New ribbon button
	Then  New work order form is opened
	And User enters Service Account 'Service Account name'
	And User Enters Functional Location as 'Funtional  location'
	And  User enters Work order type as 'work order type'
	And User click on Save and close button
	Then Work order is created and saved
