
using Newtonsoft.Json;

var salesFiles = FindFiles("stores");
    
foreach (var file in salesFiles)
{
    Console.WriteLine(file);
}

var currentDirectory = Directory.GetCurrentDirectory();

Console.WriteLine("Directorio actual: " + currentDirectory);
//Obtiene la ruta de la carpeta de documentos en cualquier OS
string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
Console.WriteLine("Directorio de documentos: " + docPath);
//Separador de carpetas independiente del OS
Console.WriteLine($"stores{Path.DirectorySeparatorChar}201");

Console.WriteLine(Path.Combine("stores","201"));

string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";


//Obtiene toda la información de un archivo
FileInfo info = new FileInfo(fileName);

Console.WriteLine(
    $"Full Name: {info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}"); // And many more



IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        //Busca todos los json
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}


double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {      
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}


//Verifica si un directorio existe
bool doesDirectoryExist = Directory.Exists(Path.Combine("stores","201", "newDir"));

if (doesDirectoryExist)
{
    Console.WriteLine("El directorio existe");
}
else
{
    //Crear directorios
    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "stores","201","newDir"));

}

//Verifica si un directorio existe
bool doesFileExist = File.Exists(Path.Combine("stores","201", "newDir", "greeting.txt"));

if (doesFileExist)
{
    Console.WriteLine("El Fichero existe");
}
else
{
    //Crear directorios
    File.WriteAllText(Path.Combine("stores","201", "newDir", "greeting.txt"), "Hello World!");

}


Console.WriteLine("=====================================");
Console.WriteLine("Crear un directorio de SalesTotalsDir");
var salesTotalsDirectory = Path.Combine(currentDirectory, "stores", "SalesTotalsDir");

doesDirectoryExist = Directory.Exists(salesTotalsDirectory);

if (!doesDirectoryExist)
{
    Directory.CreateDirectory(Path.Combine(salesTotalsDirectory));
}
var salesTotalsFile = FindFiles(salesTotalsDirectory);
Console.WriteLine("=====================================");
Console.WriteLine("Crear un archivo de SalesTotals");
File.WriteAllText(Path.Combine(salesTotalsDirectory, "SalesTotals.txt"), String.Empty);

Console.WriteLine("=====================================");
Console.WriteLine("Escribir en el archivo de SalesTotals el total de ventas");
var totalSalesJson = File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");
var totalSales = JsonConvert.DeserializeObject<SalesTotal>(totalSalesJson);
Console.WriteLine($"Total Sales: {totalSales}");
File.WriteAllText($"{salesTotalsDirectory}{Path.DirectorySeparatorChar}SalesTotals.txt",$"{totalSales.Total}{Environment.NewLine}");
File.AppendAllText($"{salesTotalsDirectory}{Path.DirectorySeparatorChar}SalesTotals.txt", $"{totalSales.Total}{Environment.NewLine}");


class SalesTotal {
    public double Total { get; set; }
}


record SalesData (double Total);