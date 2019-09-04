# ZipFileUploaderRepo
# Build Steps
1.	Open ZipFileUploader.sln
2.	Set ControlPanel.Api and ControlPanel.Web projects as start up projects
3.	Ensure that the AppSettings.ApiUrl is the same as the url of ControlPanel.Api
4.	Ensure that the user specified in Connection string exists on the given server and that user has DB create permission (for seed data creation)
5.	Run both projects mentioned in step two together

# Notes:
•	Register a user in the web portal and login using those credentials\
•	Basic authentication user name and password can be found in AppSettings. BasicAuthUsername AppSettings.BasicAuthPassword\
•	Key for encryption can be found in AppSettings.EncryptionKey
