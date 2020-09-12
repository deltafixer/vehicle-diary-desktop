# Vehicle Diary

Welcome!

This is a vehicle-history-reporting desktop application written in **WPF** using **Caliburn.Micro** and an **SQL database**.

## Features:

### Profiles

1. Person profile
   - Individual
   - Police
   - Taxi
   - Driving school
2. Service profile
   - Authorized
   - Unauthorized

Upon starting the application, the user is greeted with a `login window` which contains a `register` button.

Clicking the register button leads the user to register forms, where he/she may create a new profile with one of the above options.

Passwords are hashed using `BCrypt.Net-Next`.

### Check VIN

Upon loging in, a VIN check screen is presented.

This is the main feature of the application, no matter the user type, a user can use this feature.

A user enters a 17-character VIN and the application returns a full report:

1.  Vehicle
2.  Vehicle specifications
3.  Vehicle accidents
4.  Vehicle services

**This report is printable to PDF format.**

Users with person profiles have an even quicker approach to reports of vehicles they are owners of.

### My vehicles

A feature only available to users with person profiles.

A screen with a list of all vehicles the user owns, with options of removing any vehicle, viewing a report (same as check VIN option), and creating a sale listing for any vehicle.

This screen also contains a form for adding a new vehicle, with its specifications.

### Market

Another feature which is only available to users with person profiles.

The market screen displays a list of all of the vehicles that are listed for sale (by all of the application users).

Upon creating a sale listing for a vehicle, a user is redirected to the marketing screen and can see his/her listing up and active.

For every sale listing, the application generates a **suggestion score**.

This score is calculated based on:

- Vehicle make/model average asking price
- Number of services (incl. recommended services per year constant)
- Number of accidents (if accident count is bigger or equal to 4, the vehicle is written of and should not be sold)

### Add service

A feature only available to users with service profiles.

A service user may add a vehicle service.

Once a service is conducted, a service user should file a report via the provided form in the application.

A service user should enter vehicle VIN, date, price and service details.

### My services

A feature only available to users with service profiles.

A screen with a list of all services of the service user.

There are four period filters:

- All time
- This month
- Last week
- Today

Any service can also be opened in a new screen to display more information.

### Report accident

A feature which is only available to users with person profiles which are of type POLICE.

A police user may report a vehicle accident.

A police user files a report by entering vehicle VIN, date of accient and a damage price evaluation.

### Profile

Any user may edit his/her profile information.

This means that a user may change:

1. Person user:
   - First name
   - Last name
   - Password
   - Person type
2. Service user:
   1. Name
   2. Password
   3. Service type

## Notes

This application uses `Autofac` to handle/automate dependency injection and `EntityFramework` to handle database querying.

No design package was used, only native elements.

Global styling is defined in `App.xaml`.

Screens are responsive.

## Missing from initial plan

1. Admin account with complete control (not really a must-have due to users being able to manipulate their own data).
