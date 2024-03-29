﻿Feature: Maintain Personal Details
	In order to ensure my profile information is correct
	As an investor
	I want to be able to maintain my details online 

Background: 
	Given the investor has logged in
		And the Dashboard is displayed

Scenario: Investor provides all mandatory data and updates their profile
	Given the investor has elected to maintain their profile
	When the following mandatory personal details are provided
		| Title | FirstName | LastName | Age | PhoneNumber  | Postcode | MoveInDateMonth | MoveInDateYear |
		| Mr    | Test      | Investor | 25  | 02920 111222 | CF31 5DD | 2               | 2010           |
	Then investors profile will be updated with no errors

Scenario: Investor omits mandatory data and is prompted accordingly
	Given the investor has elected to maintain their profile
	When their personal details are updated without providing any mandatory data
	Then the investor will be prompted about the following issues
		| Expected Error                                   |
		| First name is required                           |
		| Last name is required                            |
		| Title is required                                |
		| Date of birth is required                        |
		| Phone number is required                         |
		| Valid three years of address history is required |
	
Scenario: Investor does not meet minimum age requirement
	Given the investor has elected to maintain their profile
	When they supply a date of birth that makes them 17 years and 11 months old
	Then the investor will be prompted about the following issues
		| Expected Error                             |
		| Should there be an error if <18 years old? |

Scenario Outline: Investor meets minium age requirement
	Given the investor has elected to maintain their profile
	When they supply a date of birth that makes them <years> years and <months> months old
	Then investors profile will be updated with no errors
	
	Examples:
	| years | months |
	| 18    | 0      |
	| 18    | 1      |