# .NET Feature Management Powered by a Rest API

This project was created to show case how to use [ the Official .NET Feature Management Library](https://github.com/microsoft/FeatureManagement-Dotnet) and power it by using a Rest API to be able to centrally control all your feature flags.

This project doesn't go into the TimeWidow or Percentage features that the Feature Management library is capable of. However it does provide a good starting point.

Check our the official article at https://benburgecode.medium.com/net-feature-management-powered-by-a-rest-api-ce4a5613becd

## The Setup
* Using .NET 7
* Using Microsoft.EntityFrameworkCore.Tools
Postgres DB and accompanying Nuget package Npgsql.EntityFrameworkCore.PostgreSQL
* Using the Microsoft.FeatureManagement Nuget package
* A basic API project
* A basic console application