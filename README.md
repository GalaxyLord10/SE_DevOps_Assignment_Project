# Hosting on Render
### On Render there are 3 web services and 3 databases. 

**se-devops-staging** is the Staging Environment Webservice.  
**se-devops-PROD** is the Production Environment Webservice.  
**prometheus-monitoring** is where the Prometheus monitoring.  

**STAGING_DB** is the Staging Environment's PostgreSQL database.  
**PROD_DB** is the Production Environment's PostgreSQL database.  
**local-db** is the Local Environment's PostgreSQL database so that local development can be conducted on any machine without needed to install PostgreSQL locally.

Render does not support .NET natively but does support Docker. Since, the application is containerised in Docker, the application can be hosted on Render by selecting Docker as runtime and providing the path to the Dockerfile.

![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/75624c64-bb12-43c7-ad0d-8bb0b7a74f7c)

## Database Connection
On Render, the hosted PostgreSQL allows two types of connections - internal and external.  
External connection is used when services outside of Render want to connect to the hosted PostgreSQL database e.g., connecting from **local machine** to hosted **local-db**.\
Internal connection is used when the service seeking connection to the hosted database is also hosted on Render e.g., **se-devops-staging** and **STAGING_DB**

![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/3e073e95-c687-430a-b671-31ed6cf97e4b)


# application Functionality

Go to the website: https://se-devops-k.onrender.com
Note: It may take some time to warm up initially because Render scales it down due to inactivity when using the Free tier.


## Register by clicking register:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/1729d5b0-450a-43aa-bcc6-03b2bc702fef)

## And then fill out your information and click Register:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/fe89a5cb-2bb2-438d-be9c-6a36097afb01)

## Next, confirm your account by clicking the link provided (this is not an actual confirmation but is a simulation of the process for the sake of the assignment)
## If you do not confirm your account then your user will not be created
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/46f671d6-b18c-424a-bd09-1e336beb8c45)

## Once you have confirmed you will see this:

![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/1ae8301e-f0f1-49aa-b857-1789986728be)

## Next, proceed to login with your registered account:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/43137218-9ff3-4725-bf38-f070dd01ea97)

##  Once you are logged in you will see this:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/0b35a388-d87a-4d36-9e3f-a4c6c2d8ea0e)

##  The "Manage Users" option is only accessible to those who are an admin. Given that you are not an admin you will get an Access Denied if you try to access it:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/00ff4d03-c718-45e2-a137-268f629d2b0e)

## The "View my Tasks" is accessible to all users regardless of roles. By clicking "View my Tasks" you will see the Tasks list page like so:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/ce293510-3752-449c-8a56-03458dba804b)

## This is the CRUD aspect of the application where a User can Create, Read, Update, and Delete their Tasks.

## To Create, click "Create new Task" and you will see the Create Task form:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/c1d3aaec-fe2e-48c1-a12a-b42355cdd196)

## Once you have created a new Task you will see it in your Tasks list:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/51044eb2-7ea4-4743-a93e-6b63e8a41fc3)

##  You can also edit your Task by clicking "Edit" which will open up the Edit form:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/558a1fb8-c8ca-4db1-af04-d6c26a6ba47c)

## Once updated, you will see it in the Tasks list:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/773f646d-9738-4c96-888e-e677b3f9228f)

## To delete a Task, click the "Delete" button on the Task you wish to Delete and it will lead to the delete page where it asks for a confirmation before deleteing like so:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/ef2d3248-ee61-45f7-92ab-87c61e6cc341)

## Once you click Delete it will take you back to the Tasks List page where the task will no longer exist:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/4013d9b4-d2bf-4bb2-b90d-4b578664e8f4)

## Next is the Profile Section which you can see in the navigation bar in the top right:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/5d242ff9-ba72-4ec7-b69c-cf3b6eedcd2b)

## Clicking on "Profile" will take you to the Profile section:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/e84e100a-cfc2-413c-a7e6-71e765711ad7)

## You can update your Email by going to the "Email" section:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/cf40a1ce-8f15-4a80-bff4-a827a67acf69)

## You can update your password by going to the "Password" section:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/f4361a13-8ca5-4d50-b3a9-4eaf1f0a8bae)

## You can also view your personal data by going to "Personal data" section:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/b42e718e-7bbf-40d7-b6e2-fe245e4f0357)


## Admin Users can do everyting mentioned above but are also able to access the "Manage Users" option and also navigate to https://se-devops-k.onrender.com/metrics to view application metrics.

## Once you are logged in as an Admin user you can click "Manage Users" and you will see a list of Users currently registered in the System.
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/984039f5-5786-4e5c-b713-e00e19b1ba54)

## As an Admin you are able to delete Users:
 
 ![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/76bbb576-d297-428f-b748-cc8cd56e23c2)

 ![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/4d43fb84-43eb-4c50-b633-0c4d53f708b2)


## For Security purposes only a small portion of the metrics endpoint will be shown as a screenshot:
![image](https://github.com/GalaxyLord10/SE_DevOps_Assignment_Project/assets/41874717/3c248ced-a3ac-45c1-8d01-099393a06717)
























