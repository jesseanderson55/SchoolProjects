#ifndef SOFTWARESTUDENT_H
#define SOFTWARESTUDENT_H
#include "Student.h"
#include <iostream>
#include <string>
using namespace std;

class SoftwareStudent: public Student
{
private:
	Degree::degree softStud;

public:
	// constructor and destructor
	SoftwareStudent(string SID, string inFirstName, string inLastName, string inEmail, int inStudentAge, int* toComplete, Degree::degree passDegree);
	~SoftwareStudent();

	// getter
	
	Degree::degree GetDegreeProgram();

	void Print();
};
#endif 



