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
To begin with the solution of the problem I started thinking 

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

# The Unit Test

<img width="485" alt="image" src="https://user-images.githubusercontent.com/18746243/167972753-7fc624c0-417c-4ddb-858c-f7fccbb175d0.png">

