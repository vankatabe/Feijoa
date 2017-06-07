Feature: RegisterPageTests
	As a Visitor
	I want to be able to register an Account
	So that I can Login to the System

@Regression
# This test does not pass because of the usage in *Steps.cs of the registerPage.FillRegistrationFormNegative method instead of registerPage.FillRegistrationForm method. But is added here for completion.
Scenario: Register _ UniqueCredentials _ RegisterSuccessful
	Given I am on the Registration page
	When I fill-in the registration form is "Register_UniqueCredentials_RegisterSuccessful"
	Then Message should be displayed is "Hello"

@Negative
Scenario: Register _ Without _ Email
	Given I am on the Registration page
	When I fill-in the registration form is "Register_Without_Email"
	Then Message should be displayed is "The Email field is required."

@Negative
Scenario: Register _ Without _ FullName
	Given I am on the Registration page
	When I fill-in the registration form is "Register_Without_FullName"
	Then Message should be displayed is "The Full Name field is required."

@Negative
Scenario: Register _ Without _ Password
	Given I am on the Registration page
	When I fill-in the registration form is "Register_Without_Password"
	Then Message should be displayed is "The Password field is required."

@Negative
Scenario: Register _ Without _ ConfirmPassword
	Given I am on the Registration page
	When I fill-in the registration form is "Register_Without_ConfirmPassword"
	Then Message should be displayed is "The password and confirmation password do not match."