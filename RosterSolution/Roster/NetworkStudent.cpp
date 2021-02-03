#include "NetworkStudent.h"


NetworkStudent::NetworkStudent(string SID, string inFirstName, string inLastName, string inEmail, int inStudentAge, int* toComplete, Degree::degree passDegree)
	: Student(SID, inFirstName, inLastName, inEmail, inStudentAge, toComplete)
{
	netStud = passDegree;
}


NetworkStudent::~NetworkStudent()
{
}

//getter

Degree::degree NetworkStudent::GetDegreeProgram()
{
	return netStud;
}

void NetworkStudent::Print()
{
	int* daysToCompleteCourse = GetDaysInCourse();
	cout << GetStudentID() << "\t";
	cout << "First Name: " << GetFirstName() << "\t";
	cout << "Last Name: " << GetLastName() << "\t";
	cout << "Age: " << GetAge() << "\t";
	cout << "DaysInCourse: {" << daysToCompleteCourse[0] << "," << daysToCompleteCourse[1] << "," << daysToCompleteCourse[2] << "}\t";
	cout << "Degree Program: " << netStud << " Networking\n";
}