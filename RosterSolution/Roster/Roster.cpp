#include "Roster.h"
#include <string>
#include <iostream>
using namespace std;

int Roster::tempVar = 0;

void Roster::add(string studentID, string firstName, string LastName, string emailAddress, int age, int daysInCourse1, int daysInCourse2, int daysInCourse3, Degree::degree degreeName)
{

	int daysLeftForCourse[3] = { daysInCourse1,daysInCourse2,daysInCourse3 };

		if (degreeName == Degree::degree::SOFTWARE)
		{
			classRosterArray[tempVar] = new SoftwareStudent(studentID, firstName, LastName, emailAddress, age, daysLeftForCourse, degreeName);
		}
		if (degreeName == Degree::degree::NETWORKING)
		{
			classRosterArray[tempVar] = new NetworkStudent(studentID, firstName, LastName, emailAddress, age, daysLeftForCourse, degreeName);
		}
		if (degreeName == Degree::degree::SECURITY)
		{
			classRosterArray[tempVar] = new SecurityStudent(studentID, firstName, LastName, emailAddress, age, daysLeftForCourse, degreeName);
		}
		tempVar++;
}

void Roster::printAll()
{
	for (int i = 0; i < 5; i++)
	{
		classRosterArray[i]->Print();
	}
}

void Roster::printDaysInCourse(string studentID)
{
	float averageByCourse = 0;
	const float MAX_OF_COURSES = 3.0;
	for (int i = 0; i < 5; i++)
	{
		if ( classRosterArray[i]->GetStudentID() == studentID)
		{
			averageByCourse = (classRosterArray[i]->GetDaysInCourse()[0] + classRosterArray[i]->GetDaysInCourse()[1] + classRosterArray[i]->GetDaysInCourse()[2]) /  MAX_OF_COURSES;
		}
	}
		cout << "Student #" << studentID << " listed with an average amount of days in the 3 courses as ";
		cout << averageByCourse << endl;
}

void Roster::printInvalidEmails()
{
	for (int i = 0; i < 5; i++)
	{
		string emailCheck = classRosterArray[i]->GetEmail();
		bool validEmail = false; 
		size_t atFound = emailCheck.find("@");
		size_t periodFound = emailCheck.find(".");
		size_t spaceFound = emailCheck.find(" ");
		if (atFound != string::npos && periodFound != string::npos && spaceFound == string::npos)
		{
			validEmail = true;
		}
		if (!validEmail)
		{
			cout << emailCheck << " has a syntax error and is not a valid address." << endl;
		}
	}
}

void Roster::printByDegreeProgram(Degree::degree degreeProgram)
{
	for ( int i = 0 ; i < 5 ; i++)
	{ 
		if (classRosterArray[i]->GetDegreeProgram() == degreeProgram)
		{
			classRosterArray[i]->Print();
		}
	}
}

void Roster::remove(string studentID)
{

	bool studentToBeRemoved = false;

	for (int i = 0; i < 5; i++)
	{
		if (classRosterArray[i] != nullptr && classRosterArray[i]->GetStudentID() == studentID)
		{
			classRosterArray[i] = nullptr;
			studentToBeRemoved = true;
			cout << "Student with ID:" << studentID << " has been removed." << endl;
			break;

		}

	}
	
	if (studentToBeRemoved == false)
	{
		cout << "No such student found at ID:" << studentID << ". Please check ID." << endl;
	}
	
}

Roster::Roster()
{
}

Roster::~Roster()
{
}



int main()
{
	const string studentData[] =
	{"A1,John,Smith,John1989@gm ail.com,20,30,35,40,SECURITY",
	"A2,Suzan,Erickson,Erickson_1990@gmailcom,19,50,30,40,NETWORK",
	"A3,Jack,Napoli,The_lawyer99yahoo.com,19,20,40,33,SOFTWARE",
	"A4,Erin,Black,Erin.black@comcast.net,22,50,58,40,SECURITY",
	"A5,Jordan,Anderson,J.And@aol.com,36,44,26,30,SOFTWARE"};

	
	
	Roster* classRoster = new Roster(); 
	

	for (int i = 0; i < 5; i++)
	{
		string tempStr = studentData[i];	
		string delimiter = ",";
		string arrayValue[10]; 
		Degree::degree degreeToBePassed;
		int indx2 = 0;
		size_t pos = 0;
		while ((pos = tempStr.find(delimiter)) != string::npos) 
		{
			
			arrayValue[indx2] = tempStr.substr(0, pos);
			tempStr.erase(0, pos + delimiter.length());
			indx2++;
		}
		
		
		if ( tempStr == "NETWORK" )
		{
			degreeToBePassed = Degree::degree::NETWORKING;
		}
		if (tempStr == "SOFTWARE")
		{
			degreeToBePassed = Degree::degree::SOFTWARE;
		}
		if (tempStr == "SECURITY")
		{
			degreeToBePassed = Degree::degree::SECURITY;
		}
		
	classRoster->add(arrayValue[0], arrayValue[1], arrayValue[2], arrayValue[3], stoi(arrayValue[4]), stoi(arrayValue[5]), stoi(arrayValue[6]), stoi(arrayValue[7]), degreeToBePassed);
	
	}

	
	classRoster->printAll();
	classRoster->printInvalidEmails();
	//loop through classRosterArray and for each element:
	for (int i = 0; i < 5; i++)
	{
		classRoster->printDaysInCourse(classRoster->classRosterArray[i]->GetStudentID());
	}
	

	classRoster->printByDegreeProgram(Degree::degree::SOFTWARE);
	classRoster->remove("A3");
	classRoster->remove("A3");
	//expected: the above line should print a message saying such a student with this ID was not found.
	//cin.get();
	return 0;
}

