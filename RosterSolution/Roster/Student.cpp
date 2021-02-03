#include <iostream>
#include <string>
#include "degree.h"
#include "Student.h"
using namespace std;

//start constructor/deconstructor
Student::Student(string SID, string inFirstName, string inLastName, string inEmail, int inStudentAge, int* toComplete)
{
	studentID = SID;
	firstName = inFirstName;
	lastName = inLastName;
	emailAddress = inEmail;
	studentAge = inStudentAge;
	daysToCompleteCourse[0] = toComplete[0];
	daysToCompleteCourse[1] = toComplete[1];
	daysToCompleteCourse[2] = toComplete[2];
	return;
}

Student::Student()
{
}

Student::~Student() 
{
	
}



void Student::SetStudentID(string idofStudent)
{
	studentID = idofStudent;
	return;
}
void Student::SetFirstName(string firstName)
{
	this->firstName = firstName;
	return;
}
void Student::SetLastName(string lastName)
{
	this->lastName = lastName;
	return;
}
void Student::SetEmail(string emailAddy)
{
	emailAddress = emailAddy;
	return;
}
void Student::SetAge(int studentAge)
{
	this->studentAge = studentAge;
	return;
}

void Student::SetDaysInCourse(int* courseETA)
{
	for (int i = 0; i < 3; i++)
	{
		daysToCompleteCourse[i] = courseETA[i];
	}
}


// Getters start


string Student::GetStudentID() const
{
	return studentID;
}
string Student::GetFirstName() const
{
	return firstName;
}
string Student::GetLastName() const
{
	return lastName;
}
string Student::GetEmail() const
{
	return emailAddress;
}
int Student::GetAge() const
{
	return studentAge;
}

int* Student::GetDaysInCourse()
{

	return daysToCompleteCourse;

}

Degree::degree Student::GetDegreeProgram()
{
	return Degree::degree::SOFTWARE;
}

void Student::Print()
{
}

