fixes:
- formatting
- removed Console.log
- fixed references to Nunit ( if error "Nunit already referenced in .Net" - update nuget from visual studio gallery helps me)
- added to project file messages.routes.js
- included to zip all nuget packages and client libs 
- included missing genereted *d.tsd files


### Workplace prerequisites


* [MsSqlExpress]
* [.Net452]
* [IISExpress]

### Setup Test Database

Execute script for test run but change filename inside .sql files
sqlcmd  -S localhost\SQLExpress -i {solution}\test.messages\createtestdb.sql
sqlcmd  -S localhost\SQLExpress -i {solution}\test.messages\createtesttable.sql

### Client (all files included - run if you need to update client libs, else skip)
npm install
bower install

### Workflow

* Run web-server without Debugging for integration tests (Menu - Debug - Start without debugging)
	(default port "http://localhost:5001/"  )

### TODO 
facky integration tests if when run all test
client validation
chrome driver reuse
linq2db insert should return generated identity id, but always return 1