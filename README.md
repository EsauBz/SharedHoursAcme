# Shared Hours Exercise
## The Problem

The company ACME offers their employees the flexibility to work the hours they want. But due to some external circumstances they need to know what employees have been at the office within the same time frame.

The goal of this exercise is to output a table containing pairs of employees and how often they have coincided in the office. Input: the name of an employee and the schedule they worked, indicating the time and hours. This should be a .txt file with at least five sets of data.

#### Example:

| RENE=MO10:15-12:00,TU10:00-12:00,TH13:00-13:15,SA14:00-18:00,SU20:00-21:00 | 
| :---: | 
| ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00 |  
| OUTPUT |
| RENE-ASTRID: 3 |

## The Approach
To begin with the solution of the problem I started thinking if I wanted to work a console application or something with and interface, I thought that having an interface will demostrate better the different parts of a program, or a more complex architecture having a view and a controller.
I decided to go with a windows form approach, is a easy way to develop an application with an interface and still concentrate in the c# code rather than the development of the UI.
First I drew a sketch of how I wanted the interface to look like. After that I wrote in a blank document the models that I needed, at least a sketch of that. Thinking that I would need to store the name of the worker and the days and different hours I just wrote a little pseudocode of how the model would function.
After that I did a small draw of the responsabilities of the controller, the view and the models.

Before started coding I wrote in a document what kind of test I would have at the end, I wrote 5 small test as a pseudocode, what would enter and what would I have as a result, and I was ready to code.

Since I already had a sketch of the UI in the windows form, I wrote the interface since I need to call it from the view. When I was ready I started coding the controller, I advanced in the algorithm so I needed the models. I created the folder for the models and completed the code. 


## The Solution

  #### Architecture (The Model-View-Controller(MVC) Pattern with C#/WinForms)
  
  The idea is to separate the user interface into View (creates the display) and Controller (responds to user requests, interacting with both the View and Controller as necessary), the model is called as necessary from the controller to get information (used as an entity to model the input from the file). The main advantage of MVC pattern is Loose Coupling. 
All the layers are separated with their own functionality. In other words, MVC pattern is to break up UI behavior into separate pieces in order to increase reuse possibilities and testability. I am using Visual Studio 2019 with .NET 5.0 for creating this Application.

Model-View-Controller as the name applies considers three pieces:

- Model: It should be responsible for the data, in this case es used in creating classes that act as entitys to "Deserealize" (if we can use that word) the information from the .txt files.
- View: It presents the display of the model in the user interface, in this case a simple windows form that lets browse for a file and represents the result of shared hours.
- Controller: it is really the heart of the MVC, the intermediary that ties the Model and the View together, manipulates the model and causes the view to update when neccessary.
  
 ##### View 
  In the view part of the architecture we have the AcmeForm.cs class, we can clearly see the design if we inspect it in visual studio (since is a windows form). We will see implemented two buttons, one text box and a grid view. In the code we will encounter the implementation of the interface and two events (one for each button). The methods that come from the implementation of the interface are the ones in charge of the "refresh" of the interface. Clear the grid, clear the textbox or, finally, add the result of the exercise to the data grid.
#### Controller
The controller class is called SharedHoursController.cs and is host under the folder controllers. Since is a small exercise the controller has just "two" major actions. 

 The first one is, when the program starts, is resposible for setting the view in place, clearing the grid and textbox and showing the dialog, this functionalities are under the LoadView() method in the controller.
 
The second one is, open the dialog for the search of the file and "triggering" the operations to have the result of the shared hours in the office.
##### Algorithm
Starting with the method OpenHoursFile() we will open a file dialog which will try to open the file that contain the input (the txt in this case) and read the information. 

If we are able to read a stream of information (a string) we will pass this raw data to the method CreateWorkerFromInput().
This method will first, split the data into arrays (being the separator the \n character), this will give us the number of workers. Second step will be spliting each worker, the separator will be "=" character, we will obtain the name with this. The second element of the array splited will be tha days and hours, this second element of the array will be splited with "," character. Finally, we will obtain an array, we will iterate trough this to create the model of the day worked, with initial hour and final hour. 

We can mention in here that the model layer will be used in this part since the information readed will be stored in models or entities created for this purpose.
If he information recovered is not empty (a list of workers with the days and hours they worked) we will call the method CheckSharedHours(), this will be a process of iterate trough each worker and comparing his hours in the office with the rest of his collegues (n-1). If is the case that we encounter shared hours (this is calculated with a linq operation) then we will make a rest to know exactly how much time.

Finally, the controller will call the method that refresh the view AddWorkersToGrid() with the list of workers (different from zero) that shared time in the office, and of course the time shared.

##### View 
  We already mention a litte about the function of the model in the controller part, let's just describe which information is stored in which classes.
  Under the Model folder, we will find the Employee.cs file, which represents a class, this represents an employee with a name and a list of days were he/she was at the office and at wich time. The class that represents a day in the office is OfficeHours.cs inside we will encounter the name of the day, the initial hour and the final hour (or the time the employee arrived and left the office), the last two as DateTimes. 
This serve to do the calculation of the shared hours easily, since is just a rest of DateTimes. 

# The Instructions
The best approach to execute the app is first of all, it was developed in .NET 5, so it should be installed first.
The best option would be to have Visual stuido 2017 or 2019 installed, since it would be the fastest way to launch the app, also you could inspect and run the unit test easily.

So, for that we are going to download the repository (we can download as a zip and the extract or we can clone the repository directly from github) once that we have the solution, we will identify the file Acme.Workers.SharedHours.sln and double clck it (image below).

<img width="205" alt="image" src="https://user-images.githubusercontent.com/18746243/167988669-adc3737f-d778-4375-b6cc-c98f77a2ab53.png">

That will open the solution in visual studio, we will just idetify the icon play (a green arrow) in the middle of the toolbar and click said icon. The solution will execute automatically and we can test it (in the repository you will find three input files as an examples that can be used for the app).

<img width="167" alt="image" src="https://user-images.githubusercontent.com/18746243/167989398-23f12d13-ffc8-4d57-b538-43bf2434271c.png">

# The Unit Test

To run the unit test we can easily identify in the solution explorer view the project that contains the unit test (image below).

<img width="176" alt="image" src="https://user-images.githubusercontent.com/18746243/167989174-89f1408c-9b88-4ec8-994b-024d404e3446.png">

We can right click the project and choose the "Run test" button. That will triggered automatically all of the test, and after that we will see a small sintesis of all of them.

<img width="485" alt="image" src="https://user-images.githubusercontent.com/18746243/167972753-7fc624c0-417c-4ddb-858c-f7fccbb175d0.png">

