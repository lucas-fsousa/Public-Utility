# App Utils

App Utils is a library that contains utilities for asp.net / .net / c# application development

## Installation

Use the package manager [nuget](https://www.nuget.org/packages) to install App Utils.

```bash
install-package App.Utils
```

## Usage

```csharp
using App.Utils // call Namespace

// ================= examples of use of the XSystem class =================

//creates and runs a script.bat 
string[] commands = new { "echo Hello", "echo World" };
int exitCode = All.XSystem.RunCmdScript(commands);

// returns a list of files from the indicated path
List<FileInfo> files = All.XSystem.GrabFilesFromFolder(@"C:\MyDocs");

// gets all processes that have "chrome" signature or similar
List<Process> processes = All.XSystem.GetAllProcessByName("chrome", true);

// ================= examples of use of the XSql class =================

// run a sql using SqlServer and returns a data table
SqlCommand sqlCommand = new SqlCommand();
string errorMessage;

sqlCommand.CommandText = "select * from yourTable";
All.XSql xSql = new All.XSql(@"your connection string here", sqlCommand);
DataTable table = xSql.ReturnData(out errorMessage);

// run a void sql statement using SqlServer
SqlCommand sqlCommand= new SqlCommand();
string errorMessage;

sqlCommand.CommandText = "insert into yourTable values (@stringExample, @integerExample, @floatExample)";
sqlCommand.Parameters.AddWithValue("@stringExample", "Hello"); // PARAM
sqlCommand.Parameters.AddWithValue("@integerExample", 1); // PARAM
sqlCommand.Parameters.AddWithValue("@floatExample", 5.5); // PARAM
All.XSql xSql = new All.XSql(@"your connection string here", sqlCommand);
xSql.GoExec(out errorMessage);

// ================= examples of use of the XScreen class =================

// gets the dimensions of the main screen
int desktopWidth = All.XScreen.DesktopWidth;
int desktopHeight = All.XScreen.DesktopHeight;

// Simulates a mouse click in the center of the screen
All.XScreen.ClickAt(desktopWidth / 2, desktopHeight / 2);

```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)