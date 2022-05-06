Feature: Work Order
	This Feature targets scenarios around FSA work order 


Scenario: User Creates a new work order
	Given user is landed on Apps page
	And The user nagivates to work order page 
	And user creates a new work order
	And user enters required work order Info 'abc'
	Then Work order is created and saved
