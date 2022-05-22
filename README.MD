# Public Utility Package

App Utils is a library that contains utilities for asp.net / .net / c# application development

## Installation

Use the package manager [NuGet](https://www.nuget.org/packages/LucasFsousa.PublicUtility) to install this package or:

```bash
EXAMPLE
PM> Install-Package LucasFsousa.PublicUtility -Version 1.1.3

.NET CLI> dotnet add package LucasFsousa.PublicUtility --version 1.1.3

Package Reference> <PackageReference Include="LucasFsousa.PublicUtility" Version="1.1.3" />

```

## Usage

```csharp
using PublicUtility; // call Namespace
using PublicUtility.Xnm; //  call aux Namespace


// ================= examples of use of the XSystem class =================

//creates and runs a script.bat 
string[] commands = new { "echo Hello", "echo World" };
XSystem.RunCmdScript(commands);

// ------------------------------------------------------------------

// returns a list of files from the indicated path
List<FileInfo> files = XSystem.GrabFilesFromFolder(@"C:\MyDocs");

// ------------------------------------------------------------------

// gets all processes that have "chrome" signature or similar
List<Process> processes = XSystem.GetAllProcessByName("chrome", false);


// ================= examples of use of the XSql class =================

// run a sql using SqlServer and returns a data table
SqlCommand sqlCommand = new SqlCommand();
string errorMessage;

sqlCommand.CommandText = "select * from yourTable";
XSql xSql = new XSql(@"your connection string here", sqlCommand);
DataTable table = xSql.ReturnData(out errorMessage);

// ------------------------------------------------------------------

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

// ------------------------------------------------------------------

// Get a printscreen / currently only available for windows / Available for other platforms soon
object response = XScreen.PrintScreen();
if(response.GetType().Name == "Bitmap") {
	Bitmap printScreen = (Bitmap)response; // cast the object
	printScreen.Save(@"C:\MyDocs\printscreen.png", ImageFormat.Png); // save the image in the desired folder
}

// ================= examples of use of the XSecurity class =================

// get a hash
string oneHash = XSecurity.GetHash();

// ------------------------------------------------------------------

// Get an MD5 encryption standard
string strHashMd5 = XSecurity.GetHashMD5("KeUabaN!a=a%@15LNBaiQ");

// ------------------------------------------------------------------

// Encrypting a string with a numeric key 
string encrypt = XSecurity.Encrypt("string for encrypto", "12345678");

// ------------------------------------------------------------------

// Decrypting a string encrypted by a numeric key
string decrypt = XSecurity.Decrypt(encrypt, "12345678");

// ================= examples of use of the X class =================

// ------------------------------------------------------------------

string[] stringArray = new[] { "one value", "two value", "three value", "four value", "five value" }; // array len is 5
string newValue = "six Value";
X.AddOnArray(ref stringArray, newValue); // added the item to the end of the array! After this, array len is 6

// ------------------------------------------------------------------

IEnumerable<string> strs = new List<string>() { "value 01", "value 02", "value 03", "value 04", "value 05" };
int index = strs.GetIndex("value 03"); // get the index of the specified item in an array, list or enumerator

var randomValue = strs.GetRandomValue(); // get a random item from an array, list, or enumerator

// ------------------------------------------------------------------

var str = string.Empty;
bool isdef = str.IsDefault(); //checks if the object value is the default specified by the compiler

// ------------------------------------------------------------------

var value = X.GetSafeValue<DateTime>("20A10-2012"); // gets the input value converted to the specified type. On error returns a safe value of the specified type

// ------------------------------------------------------------------
IEnumerable<int> enumerable = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// get all odd numbers from a list, array or enumerator
List<int> lstod = X.GetOddNumbers(enumerable.ToList());
int[] arrod = X.GetOddNumbers(enumerable.ToArray());
var enumod = X.GetOddNumbers(enumerable);

// get all even numbers from a list, array or enumerator
List<int> evlst = X.GetEvenNumbers(enumerable.ToList());
int[] evarr = X.GetEvenNumbers(enumerable.ToArray());
var evenum = X.GetEvenNumbers(enumerable);

// ------------------------------------------------------------------

var result = X.DeserializeTable<YourObjectHere>(your data table here); // Transforms the data returned from the database into a typed object

string a = "A 1 4 1 ;A,. BCCC a daad ad.,.,.,78 aJdyha @ 1 :44Gajç1:#lXpP#; 13 1* a;)#(:!!.,;%%!";
var letters = a.GetOnlyLetters(); // get only Letters from string

var digits = a.GetOnlyLetterAndNumber(); // get only Letters and numbers from string

var numbers = a.GetOnlyNumbers(); // Get only numbers from string

var simb = a.GetOnlySymbol(); // get only symbols from string

var branco = a.GetOnlyWhiteSpace(); // get only whitespace from string

var lower = a.GetOnlyLowerCase(); // get only lowercase characters from string

var upper = a.GetOnlyUpperCase(); // get only uppercase characters from string

var tes = a.GetOnlySpecialChars(); // get only special characters from string

var one = a.GetOne(); // get a random item from an array, list, or enumerator

// ------------------------------------------------------------------

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

// ------------------------------------------------------------------

// requests a response confirmation from the server
var ping = X.GetPing("8.8.8.8").Result; // 8.8.8.8 is a google server IP
ping.ToString().Print(); 

// ------------------------------------------------------------------

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

// ------------------------------------------------------------------

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

// ------------------------------------------------------------------

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

// ------------------------------------------------------------------

// performs a click on the current mouse position (left button)
XMouse.LeftClick();

// ------------------------------------------------------------------

// performs a double-click in the current mouse position (left button)
XMouse.LeftClick(true);

// ------------------------------------------------------------------

// performs a click on the current mouse position (right button)
XMouse.RightClick();

// ------------------------------------------------------------------

// Moves the mouse to the specified screen coordinate instantly, without motion animation.
XMouse.MoveTo(150, 200, MouseSpeed.Full);

// ------------------------------------------------------------------

Point start = new(10, 15); // start x y coordinate point
Point end = new(220, 500); // end x y coordinate point
// Performs drag from start point and drop to end point with x2 move animation
XMouse.Drag(start, end, MouseSpeed.X2);

// ------------------------------------------------------------------

// get the current cursor position
Point currentPos = XMouse.GetPosition();

// ------------------------------------------------------------------

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

// ------------------------------------------------------------------

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


// ================= examples of use of the XExcel class =================

 //DATABASE SIMULATOR
List<Obj> lstObj = new List<Obj>();
XTable table = new XTable(3);

for(int i = 1; i <= 10; i++) {
Obj obj = new();
obj.ID = i;
obj.Name = $"NAME {i}";
obj.NUMERO = (i * i);
lstObj.Add(obj);
}

// TABLE COLUMN NAMES
table.AddCell("A1", "ID");
table.AddCell("B1", "NAME");
table.AddCell("C1", "NUM");

// VALUES TO INSERT IN TABLE
string nextCell = XExcel.GetNextLine("A1"); // get the next line using the current one as a reference (this next is 'A2')
string aux = nextCell; // Saves the safe position of the used line
foreach(Obj obj in lstObj) {

table.AddCell(nextCell, $"{obj.ID}");
nextCell = XExcel.GetNextColumn(nextCell); // get the next column based on the current column

table.AddCell(nextCell, obj.Name);
nextCell = XExcel.GetNextColumn(nextCell); // get the next column based on the current column

table.AddCell(nextCell, obj.NUMERO.AsString());
nextCell = XExcel.GetNextColumn(nextCell); // get the next column based on the current column

nextCell = XExcel.GetNextLine(aux); // get the next line using the current one as a reference
aux = nextCell; // Saves the safe position of the used line
}

// Table style is optional.
XTableStyle style = new();
style.ColumnColor = "#063970";
style.FirstLineColor = "#A6cbde";
style.SecondLineColor = "#abdbe3";
style.FontColumnColor = "#000";
style.FontLineColor = "#000";

table.Style = style; //  table style

// WorkSheet that will hold the table
XWorkSheet sheet = new XWorkSheet();
sheet.WorkSheetColor = "#30091e";
sheet.WorksheetName = "Dashboard";
sheet.Tables = new List<XTable> { table };

// Excel document that will be created
XExcel excel = new();
excel.WorkSheets = new List<XWorkSheet> { sheet };
excel.Generate(@"C:\MyDocs\planTest.xlsx");

// AFTER GENERATE, THIS IS A TABLE RESULT IN DOC XLSX
//| ID |  NAME  |  NUM |
//| 1  | NAME 1 |  01  |
//| 2  | NAME 2 |  04  |
//| 3  | NAME 3 |  09  |
//| 4  | NAME 4 |  16  |
//| 5  | NAME 5 |  25  |
//| 6  | NAME 6 |  36  |
//| 7  | NAME 7 |  49  |
//| 8  | NAME 8 |  64  |
//| 9  | NAME 9 |  81  |
//| 10 | NAME 10|  100 |


```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)