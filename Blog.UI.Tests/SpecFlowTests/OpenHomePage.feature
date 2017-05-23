Feature: OpenHomePage
	As a Visitor
	I want to be able to open Blog website
	So I can read it

@Regression
	Scenario: Check WebSite Load _ Enter Blog URL _ Open Blog Home Page
	Given that the Visitor opens a web browser
	When the Visitor navigates to the Online Blog Website address
	Then the Blog Home Page is open

