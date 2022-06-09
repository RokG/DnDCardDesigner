# Markdown File

To create a migration and database
- Prerequisites
-- Delete Migration folder if necesary
-- Delete all .db files
- In Package manager console:
-- Set default project to .DataAccess
-- Run **add-migration initial**
-- Run *update-database*

Adding Creator/Provider services
-- Add Service Interfaces to Domain project
-- Add Service Classes to DataAccess project
-- Implement newly added Services to Domain project Store
-- Implement newly added Services to UI project HostBuilder (AddDatabaseAccess)

To update values when adding things to database
-- Add an action to Store
-- Add a method which will invoke the added Action
-- Call the method in CreateItem method
-- The method has to use the newly created item, in order for entity to track it
-- In the view model where the item will be added, create a method which will add item to the local variable
-- In the constructor of the view model add this method to the previously created action