# Hospital Project Team 2
Andriy Lytvynchuk (n01393936)

Hello Christine,

at this time I have integrated my part into a branch of the Team project here (didn't want to change a lot in the Master on the last day):
https://github.com/HarpreetGill15/HospitalProjectTeam2/tree/PublicationsReports

I have hard-coded Admin record in DB to access admin interface by doing the following:
1. Created new User using web interface, this adds new record to "AspNetUsers" DB table; copied user id to separate text file.
2. Generated Admin key in "AspNetRoles" table through the menu Tools-Create GUID (copied Admin key to separate text file), entered "Administrator" into Name column of this table. 
3. Entered copied User Id and Admin key into "AspNetUserRoles" table.

Admin login details I created locally are:
admin login: 		admin@gmail.com	
password: 		ilove.Net1	

My files in the Team project (https://github.com/HarpreetGill15/HospitalProjectTeam2/tree/PublicationsReports):
1. Public Reporting feature:
  Controllers: ReportsController; 
  Models: Report.cs and ReportFile.cs;
  Models-ViewModels: ReportingPageViewModel.cs, ReportViewModel;
  Views - Reports folder (Creat-, Edit-, Index.cshtml);
  
 2. Patients and Visitors feature:
    Controllers: PublicationsController;
    Models: Publication.cs;
    Models-ViewModels: PublicationPageViewModel.cs, PublicationViewModel,  PublicationShortViewModel;
    Views - Publications folder (Creat-, Edit-, Index.cshtml);
  
  3. Some Css at the bottom of Content - Site.css file.
