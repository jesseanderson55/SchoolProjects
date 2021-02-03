var app = require('express')(); // Create an instance of an Express app

var mobileApp = require('azure-mobile-apps')(); // Create an instance of a Mobile App with default settings

mobileApp.tables.add('Crew'); 
mobileApp.tables.add('Customer');
mobileApp.tables.add('JobFunction');
mobileApp.tables.add('Labor');
mobileApp.tables.add('Task');
mobileApp.tables.add('Note');
mobileApp.tables.add('Tool');
mobileApp.tables.add('Job');
mobileApp.tables.add('Worker');
mobileApp.tables.add('workerJob');
mobileApp.tables.add('Request');
mobileApp.tables.add('Message');
mobileApp.tables.add('TimeClockRecord');




app.use(mobileApp);
app.listen(process.env.PORT || 3000);
