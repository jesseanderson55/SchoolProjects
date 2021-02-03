#ifndef ROSTER_H
#define ROSTER_H
#include "Degree.h"
#include "Student.h"
#include "SoftwareStudent.h"
#include "SecurityStudent.h"
#include "NetworkStudent.h"
#include <array>
#include <string>
using namespace std;


// FIX_ME LOOK UP STATIC VARIABLE AND DECLARE AN i INT IN ROSTER.H

class Roster
{
public:
	void add(string studentID, string firstName, string LastName, string emailAddress, int age, int daysInCourse1, int daysInCourse2, int daysInCourse3, Degree::degree degreeName);
	void printAll(); 
	void printDaysInCourse(string studentID);
	void printInvalidEmails();
	void printByDegreeProgram(Degree::degree degreeProgram);
	void remove(string studentID);
	Roster();
	~Roster();
	static int tempVar;
	Student *classRosterArray[5];
};
#endif