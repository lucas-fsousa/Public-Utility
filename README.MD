﻿# Public Utility Package

App Utils is a library that contains utilities for asp.net / .net / c# application development

## Installation

Use the package manager [NuGet](https://www.nuget.org/packages/LucasFsousa.PublicUtility) to install this package or:

```bash
EXAMPLE
PM> Install-Package LucasFsousa.PublicUtility -Version 1.0.1

.NET CLI> dotnet add package LucasFsousa.PublicUtility --version 1.0.1

Package Reference> <PackageReference Include="LucasFsousa.PublicUtility" Version="1.0.1" />

```

## Usage

```csharp
using App.Utils // call Namespace

// ================= examples of use of the XSystem class =================

//creates and runs a script.bat 
string[] commands = new { "echo Hello", "echo World" };
int exitCode = XSystem.RunCmdScript(commands);

// returns a list of files from the indicated path
List<FileInfo> files = XSystem.GrabFilesFromFolder(@"C:\MyDocs");

// gets all processes that have "chrome" signature or similar
List<Process> processes = XSystem.GetAllProcessByName("chrome", true);

// ================= examples of use of the XSql class =================

// run a sql using SqlServer and returns a data table
SqlCommand sqlCommand = new SqlCommand();
string errorMessage;

sqlCommand.CommandText = "select * from yourTable";
XSql xSql = new XSql(@"your connection string here", sqlCommand);
DataTable table = xSql.ReturnData(out errorMessage);

// run a void sql statement using SqlServer
SqlCommand sqlCommand= new SqlCommand();
string errorMessage;

sqlCommand.CommandText = "insert into yourTable values (@stringExample, @integerExample, @floatExample)";
sqlCommand.Parameters.AddWithValue("@stringExample", "Hello"); // PARAM
sqlCommand.Parameters.AddWithValue("@integerExample", 1); // PARAM
sqlCommand.Parameters.AddWithValue("@floatExample", 5.5); // PARAM
XSql xSql = new XSql(@"your connection string here", sqlCommand);
xSql.GoExec(out errorMessage);

// ================= examples of use of the XScreen class =================

// Simulates a mouse click in the screen point x, y 
XScreen.ClickAt(600, 500);

// shows a text box in graphic format
XScreen.ShowMessageBox("INFORMATION", "THIS IS A MESSAGE BOX");

// ================= examples of use of the XSecurity class =================

// get a hash
string oneHash = XSecurity.GetHash();

// Get an MD5 encryption standard
string strHashMd5 = XSecurity.GetHashMD5("KeUabaN!a=a%@15LNBaiQ");

// Encrypting a string with a numeric key 
string encrypt = XSecurity.Encrypt("string for encrypto", "12345678");

// Decrypting a string encrypted by a numeric key
string decrypt = XSecurity.Decrypt(encrypt, "12345678");

// ================= examples of use of the XExtension class =================

float f = 15.10f;
bool fb = f.MaxMin(20.41f, 150.50f); // returns a false value

int i = 1000;
bool ib = i.MaxMin(100, 5000); // return a true value

string str = "a b c d e f";
string newStr = str.RemoveWhiteSpaces(); // will return the following string: "abcdef"

string anyInvalidNumber = "a 1234,5";
bool isValid = anyInvalidNumber.IsNumber(); // return false value

string anyValidNumber = "10000.50"; // supports floating-point or non-floating-point checking
bool isValid = anyValidNumber.IsNumber(); // return true value

```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)