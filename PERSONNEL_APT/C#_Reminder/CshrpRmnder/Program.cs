//title: error handler and log maker
//author: APT
using System.IO;
string path = "C:/Users/pn01kbc/Documents/GitHub/323-Programmation_fonctionnelle/PERSONNEL_APT/logs/logs.txt";
string firstWord;
int newLogNumber;
void LogMaker(string text, bool output)
{
    try
    {
        if(File.Exists(path) && File.ReadLines(path).Any())
        {
            string lastLine = File.ReadLines(path).Last();
            firstWord = lastLine.Split(' ')[0];

        }
        else
        {
            throw new Exception("fichier non existant ou vide");
        }
    }
    catch(Exception e)
    {

    }




    try
    {

        //Convertit le numéro de la dernière log en int pour l'incrémenter pour les prochains logs
        if (int.TryParse(firstWord, out int lastLogNumber))
        {
            int newLogNumber = lastLogNumber + 1;
        }

        DateTime localDate = DateTime.Now;

        TextWriter tsw = new StreamWriter(path, true);
        //writes the log directly in the file
        tsw.WriteLine(newLogNumber + " / " + localDate + " / " + text + " / " + output);
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: " + e.Message);
    }
    finally
    {
        Console.WriteLine("Executing finally block.");
    }
}

