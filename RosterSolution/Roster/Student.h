#ifndef STUDENT_H
#define STUDENT_H
#include "degree.h"
#include <array>
#include <string>
using namespace std;

class Student
{
public:
	
	// Constructor and Destructor
	Student(string SID, string inFirstName, string inLastName, string inEmail, int inStudentAge, int* toComplete);
	Student(); 	

	~Student();
	
	// List of mutators/setters for Student class
	void SetStudentID(string idofStudent);
	void SetFirstName(string firstName);
	void SetLastName(string lastName);
	void SetEmail(string emailAddy);
	void SetAge(int studentAge);
	void SetDaysInCourse(int* courseETA);
	//FIX_ME how to populate degree courses

	// List of accessors/getters for Student class
	string GetStudentID() const;
	string GetFirstName() const;
	string GetLastName() const;
	string GetEmail() const;
	int GetAge() const;
	int* GetDaysInCourse();
	virtual Degree::degree GetDegreeProgram();
	virtual void Print(); 

private:
	string studentID;
	string firstName;
	string lastName;
	string emailAddress;
	int studentAge;
	int daysToCompleteCourse[3]; 
};

#endif