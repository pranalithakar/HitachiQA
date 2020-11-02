# Introduction 
Test Automation for the Apollo Project using Behavior Driven Development. 

## NuGet Packages
Please install the following packages
```
Selenium.WebDriver
Selenium.Support
Selenium.WebDriver.ChromeDriver
Microsoft.NET.Test.Sdk
NUnit
NUnit3TestAdapter
Specflow
SpecFlow.NUnit
SpecFlow.Tools.MsBuild.Generation
```
# Overview

 TODO

## Folder Overview

```
/DataFiles
-Intended to contain files relating to State and Objects that are to be used in(usually) state. 
-State.cs acts as a global file that is continously updated as the test runs.
-Basically an object that stores variables such "createOrgRecent" which keeps track of the most recently created organization.
-State also contains multiple lists, such as "createOrgsList" which keeps track of all the organization created during current test run. 
-The object files such IOrganization.cs are meant to be used in (usually) lists in order to have strongly typed objects.

```
```
/Docs
-Intended to contain any written documentation relating to this Project.
-ResuableStatement.txt are split in two, small and large, and contain a list of specflow statements that can be easily reused.
```
```
/Driver
-Intended to contain any files concerning initiating the browser drivers.
-Currently contains setup.cs which contains methods and actions that setup a web driver, initiating database connections, maintain during test run and disposing of objects/classes setup.
-(Parallel features in dev)
```
```
/Helpers
-Intended to contain helpers functions that are outside the scope of individual page objects.
-Any function that is highly resuable and can be reused across the application belong here.
-Anything relating to database functions such as generic and unique queries, such as cosmos.cs containing Cosmos DB related queries.
-Any immutable dictionaries, variables, or lists are stored in Defaults.cs

```
```
/Pages
-Intended to contain Page objects following Page Object Model.
-The page objects are currently split into their relative section such as Application folder containing all page objects relation to application
-Any functions that are unique to that page should be in the respective page object such ChecklicenseNumber() function being in OrganizationDriver page object.
-Any functions, component or a modal that occurs on multiple pages should be stored in Shared folder as a seperate page objects such as AddVehicle.cs representing the vehicle modal
```

```
/TestCases
-Intended to contain any test cases
-Any old or obsolete test cases belong in the Deprecated Folder
-Regression folder contains test cases relating to individual User Story. 
-Regression folder also contains the specflow page objects(see below on how to build a test case)
```

```
/Workflows
-Intended to contain any functions that are large in scope and can be considered a multistep workflow such as CreateOrganization.cs

```

# How to build a Test Case

### Guidelines
* The test case itself should **NOT** contain a Specflow Step file
* Every testcase is individual in scope, meaning all test cases start at Homepage(Dashboard)
* Test cases do not rely on a previous test case(the test cases do not run in order)
* All test cases start with US-USNumber-USTitle: **US-15363-App Quote**

## Specflow Objects
Modeled after Page Object Model, we are following a method where specflow definations are broken down into individual objects. 
The objects are currently split into major areas of the Apollo such as Organization folder containing all specflow definations relating to Organization.
Each object has a feature file and step file, such as C002-CreateAnOrganization.cs contains statements relating to creating a organization and steps file with code. 
The specflow objects are never meant to run individually, objects are designed to have high reusable(not always the case) statements that can be combined into a larger testcase. 
It's best to think of specflow objects as function written in Gherkin that can be reused by any testcase if need be. 
Using this method you can have a test case that uses statements from multiple specflow objects. The end result being that testcases can have just a feature file and no need for a individual stepfile for each testcase. 

## Sample TestCase

**Test Case For US-21891**

```
Feature: US-21891-View an Issued Policy with Left Navigation Stepper
	As an underwriter, 
	I want to be able to view an issued Policy 
	with the Left Navigation stepper.

Scenario:TC01 Verify Policy Left Nav Exists
	Given User is on Homepage
	When User navigates to policy ID 10562
	Then Verify sidetab is present
	| Key | Value                 |
	| a   | Business Information |
	| b   | Contacts              |
	| c   | UW Questions          |
	| d   | Additional Questions  |
	| e   | Drivers               |

```

In the above file all 3 statements come from different specflow objects. 


# Basic BDD Guidelines
All tests are written using the BDD Gherkin Language. Here are some basic guidelines:

### Title
* An explicit title.

### Narrative
Should usually already be present in the US
 A short introductory section with the following structure:
* **As a:** the person or role who will benefit from the feature;
* **I want:** the feature;
* **so that:** the benefit or value of the feature.

### Acceptance criteria
 A description of each specific scenario of the narrative with the following structure:

Scenario: A simple description of what the scenario is doing, usually the TC title
* **Given:** the initial context at the beginning of the scenario, in one or more clauses;
* **When:** the event that triggers the scenario;
* **Then:** the expected outcome, in one or more clauses.



