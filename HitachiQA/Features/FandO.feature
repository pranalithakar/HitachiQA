Feature: F&O
	This feature tests F&O - Vendor creation, 
	sales orders and purchase orders


Scenario: User is able to create new Vendor
	Given user navigates to dashobard
	And user continues to enter new Vendor section
	When user continues to enter General Info
	Then user successfully saves new Vendor

Scenario: User uploads attachment to sales order
#	Given user launches 'create' sales order batch script
	And user navigates to dashobard
	And user continues to sales order list
	# maybe one or two more steps
	Then user launches 'confirm' sales order batch script

