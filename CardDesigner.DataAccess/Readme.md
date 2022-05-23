# Markdown File

To populate a database:
- In Package manager console:
-- Set default project to .Data
-- Set .Data project as startup project
-- Run **add-migration initial**
-- Run *update-database*
-- Changing startup project could be avoided by UI project referencing EntityFrameworkCore