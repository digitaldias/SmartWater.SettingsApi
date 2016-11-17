# SmartWater.SettingsApi
Powel SmartWater API for retrieving Settings to Demonstrate DDD, TDD, and other principles

This is part of the technical evidence described in the Powel SmartWater Hackfest on DevOps Principles. 
Suggested way to read the code: 

- Clone or download the project to your local computer
- Double-click on the *SmartWater.SettingsApi.Sln* file to open it up in Visual Studio
- Expand the *10 Presentation Layer* folder, and then the **SmartWater.SettingsApi** project
- Expand *Controllers* and look at the *TenantController.cs* class

From here, you should be able to follow the code vertically. You will find relevant test-classes paired next to their respective class libraries. 

Finally, the *50 Cross-Cutting Layer* folder contains a base-class for all unit-tests that mocks out all dependencies of the class being tested as well as providing easy-access to all mocks.
