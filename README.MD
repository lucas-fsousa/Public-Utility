# Public Utility Package

App Utils is a library that contains utilities for asp.net / .net / c# application development

## Installation

Use the package manager [NuGet](https://www.nuget.org/packages/LucasFsousa.PublicUtility) to install this package or:

```bash
EXAMPLE
PM> Install-Package LucasFsousa.PublicUtility -Version 1.1.0

.NET CLI> dotnet add package LucasFsousa.PublicUtility --version 1.1.0

Package Reference> <PackageReference Include="LucasFsousa.PublicUtility" Version="1.1.0" />

```

## Usage

```csharp
using PublicUtility; // call Namespace
using PublicUtility.Xnm; //  call aux Namespace


// ================= examples of use of the XSystem class =================

//creates and runs a script.bat 
string[] commands = new { "echo Hello", "echo World" };
XSystem.RunCmdScript(commands);

// returns a list of files from the indicated path
List<FileInfo> files = XSystem.GrabFilesFromFolder(@"C:\MyDocs");

// gets all processes that have "chrome" signature or similar
List<Process> processes = XSystem.GetAllProcessByName("chrome", false);


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

// shows a text box in graphic format
XScreen.ShowMessageBox("INFORMATION", "THIS IS A MESSAGE BOX");

// Get a printscreen / currently only available for windows / Available for other platforms soon
object response = XScreen.PrintScreen();
if(response.GetType().Name == "Bitmap") {
	Bitmap printScreen = (Bitmap)response; // cast the object
	printScreen.Save(@"C:\MyDocs\printscreen.png", ImageFormat.Png); // save the image in the desired folder
}

// ================= examples of use of the XSecurity class =================

// get a hash
string oneHash = XSecurity.GetHash();

// Get an MD5 encryption standard
string strHashMd5 = XSecurity.GetHashMD5("KeUabaN!a=a%@15LNBaiQ");

// Encrypting a string with a numeric key 
string encrypt = XSecurity.Encrypt("string for encrypto", "12345678");

// Decrypting a string encrypted by a numeric key
string decrypt = XSecurity.Decrypt(encrypt, "12345678");


// ================= examples of use of the X class =================

string a = "A 1 4 1 ;A,. BCCC a daad ad.,.,.,78 aJdyha @ 1 :44Gajç1:#lXpP#; 13 1* a;)#(:!!.,;%%!";

var letters = a.GetOnlyLetters();

var digits = a.GetOnlyLetterAndNumber();

var numbers = a.GetOnlyNumbers(); // Get only numbers

var simb = a.GetOnlySymbol(); // get only symbols

var branco = a.GetOnlyWhiteSpace(); // get only whitespace

var lower = a.GetOnlyLowerCase(); // get only lowercase characters

var upper = a.GetOnlyUpperCase(); // get only uppercase characters

var tes = a.GetOnlySpecialChars(); // get only special characters

var one = a.GetOne(); // get a random item from an array, list, or enumerator

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

// requests a response confirmation from the server
var ping = X.GetPing("8.8.8.8").Result; // 8.8.8.8 is a google server IP
ping.ToString().Print(); 


/* Usage example: Inform the type of object that will be imputed and the conversion will be performed at the time of data entry.
 * Data entry is valid only for predetermined items. It is not possible to use a Custom Object as input input.
 * the Input has 2 optional parameters, being the message to be written in the console, and the hiding of the console
 * to not display the typed characters.*/

// Example with 2 parameters
string password = X.Input<string>("Password: ", true);

// Example with 0 parameter
int anyNumber = X.Input<int>();

// Example with 1 parameter
char anyChar = X.Input<char>("Enter a new Char: ");

// DateTime input Examples: 2022/01/01 | 2022-01-01 | 01/01/2022 | 01-01-2022 will be converted to 01/01/2022 00:00:00
DateTime anyDate = X.Input<DateTime>("Enter a Date: ");

// Examples of using Custom Print
long a = 13193141; // object to print
a.Print(); // 1st way to use
X.Print(a); // 2nd way to use

string message = "Hello World!"; // string message to print
message.Print(); // 1st way to use
X.Print(message); // 2nd way to use


// ================= examples of use of the XRequest class =================

// Makes a Get on the given URL and returns a content in json format / Can be used asynchronously
string url = string.Format("http://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1"); // URL of a free API for testing
Root response = XRequest.HttpGet<Root>(url).GetAwaiter().GetResult();

// Make a Post request by sending an object to the server via URL
string url = string.Format("http://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");

Root root = new Root();
root.success = true;
root.deck_id = "3p40paa87x90";
root.remaining = 52;
root.shuffled = true;

HttpResponseMessage response = XRequest.HttpPost(url, root).GetAwaiter().GetResult();


// ================= examples of use of the XMouse class =================

// quickly moves to the indicated location and performs a click
XMouse.MoveToAndClick(10, 10, MouseSpeed.X4);

// performs a click on the current mouse position (left button)
XMouse.LeftClick();

// performs a double-click in the current mouse position (left button)
XMouse.LeftClick(true);

// performs a click on the current mouse position (right button)
XMouse.RightClick();

// Moves the mouse to the specified screen coordinate instantly, without motion animation.
XMouse.MoveTo(150, 200, MouseSpeed.Full);

Point start = new(10, 15); // start x y coordinate point
Point end = new(220, 500); // end x y coordinate point
// Performs drag from start point and drop to end point with x2 move animation
XMouse.Drag(start, end, MouseSpeed.X2);

// get the current cursor position
Point currentPos = XMouse.GetPosition();

// triggers the mouse scroll by scrolling down and then scrolling up
XMouse.RollDown(600);
XMouse.RollUp(100);

// ================= examples of use of the XKeyboard class =================

// Executes the key-by-key keyboard manipulation action with output result: "hello!"
XKeyboard.PressKey(Key.H);
XKeyboard.UnPressKey(Key.H);
XKeyboard.PressKey(Key.E);
XKeyboard.UnPressKey(Key.E);
XKeyboard.PressKey(Key.L);
XKeyboard.UnPressKey(Key.L);
XKeyboard.PressKey(Key.L);
XKeyboard.UnPressKey(Key.L);
XKeyboard.PressKey(Key.O);
XKeyboard.UnPressKey(Key.O);
XKeyboard.PressKey(Key.Shif);
XKeyboard.PressKey(Key.N1);
XKeyboard.UnPressKey(Key.N1);
XKeyboard.UnPressKey(Key.Shif);

// Executes the action of manipulating the keys creating the following output: "hello!"
// Single digit combines Presskey with UnpressKey
XKeyboard.SigleDigit(Key.H);
XKeyboard.SigleDigit(Key.E);
XKeyboard.SigleDigit(Key.L);
XKeyboard.SigleDigit(Key.L);
XKeyboard.SigleDigit(Key.O);
XKeyboard.PressKey(Key.Shif);
XKeyboard.SigleDigit(Key.N1);
XKeyboard.UnPressKey(Key.Shif);

// The following method is efficient to perform multiple key actions as long as it is indicated which action will be taken (press or release)
// Combines multiple keys - Keyboard output: "hello!" 
XKeyboard.KeyCombine(KeyAction.Press, new Key[] { Key.H, Key.E, Key.L, Key.L, Key.O, Key.Shif, Key.N1 });

// releases all keys that have been pressed
XKeyboard.KeyCombine(KeyAction.Drop, new Key[] { Key.H, Key.E, Key.L, Key.L, Key.O, Key.Shif, Key.N1 });

// Checks if the specified key is pressed returning boolean as response
bool keyDetail = XKeyboard.KeyStateInfo(Key.Shif).IsPressed;

// ================= examples of use of the XTextFile class =================

string path = @"C:\MyDocs\doc.txt";

XTextFile.AppendMultLines(path, new[] { "TESTE MULTI LINE 01", "TESTE MULTI LINE 02", "TESTE MULTI LINE 03", "TESTE MULTI LINE 04" }); // Insert multiple lines into the file consecutively

XTextFile.AppendLine(path, $"TEST AT {DateTime.Now}"); // Inserts a new line in the text document

XTextFile.ReplaceLine(path, 4, "NEW ITEM FOR INSERT IN LINE 4 IF THERE ARE 4 LINES IN THE FILE"); // Change the value of a row based on its location number (min is 1)

XTextFile.ReplaceLine(path, "TESTE MULTI LINE 02", "FIRST LOCATED - REPLACED", true); // Change the value of the old line if match (can be applied to all lines found or just the first line)

string text01 = XTextFile.GetTextLine(path, 2); // get the text of line 2 from the destination file

List<string> textLines = XTextFile.GetTextLines(path, new[] { 1, 5, 3 }); // Get specific text lines from the file ( line 1, line 5 and 3 for example)

List<string> otherTextLines = XTextFile.GetTextLines(path, 4, 6); // Gets the text of lines in the desired range

XTextFile.CreateText(@"C:\MyDocs\newdoc.txt", "LONG TEXT HERE"); // Creates a new Text file or replaces an existing one

string recText = XTextFile.GetText(@"C:\MyDocs\newdoc.txt"); // Get all the text from a file

XTextFile.DeleteLine(path, 21); // Deletes the line text corresponding to the indicator (minimum number = 1)

XTextFile.DeleteLines(path, new[] { 3, 1 }); // Deletes specific lines of text

XTextFile.DeleteLines(path, 51, 73); // Delete lines of text in a range


```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)