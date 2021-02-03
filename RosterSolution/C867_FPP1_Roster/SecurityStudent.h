#ifndef SECURITYSTUDENT_H
#define SECURITYSTUDENT_H
#include "Student.h"
#include <iostream>
#include <string>
using namespace std;

class SecurityStudent: public Student
{
private:
	Degree::degree secStud;

public:
	// constructor and destructor
	SecurityStudent(string SID, string inFirstName, string inLastName, string inEmail, int inStudentAge, int* toComplete, Degree::degree passDegree);
	~SecurityStudent();
	
	
	//getter
	Degree::degree GetDegreeProgram();

	void Print();

};
#endif 



