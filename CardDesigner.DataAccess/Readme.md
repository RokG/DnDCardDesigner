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
-- Implement newly added Services to UI project HostBuildee