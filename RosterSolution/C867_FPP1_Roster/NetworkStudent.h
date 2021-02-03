#ifndef NETWORKSTUDENT_H
#define NETWORKSTUDENT_H
#include "Student.h"
#include <iostream>
#include <string>
using namespace std;

class NetworkStudent: public Student
{
private:
	Degree::degree netStud;

public:
	// constructor and destructor
	NetworkStudent(string SID, string inFirstName, string inLastName, string inEmail, int inStudentAge, int* toComplete, Degree::degree passDegree);
	~NetworkStudent();
	
	
	//getter
	Degree::degree GetDegreeProgram();

	void Print();
};
#endif 



