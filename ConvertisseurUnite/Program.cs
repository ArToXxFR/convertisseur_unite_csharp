using System.Data.SQLite;

using (var connection = new SQLiteConnection("../database.db"))
{
    connection.Open();

    var command = new SQLiteCommand("SELECT masse_volumique FROM Ingredents LIMIT 1", connection);

    var reader = command.ExecuteReader();

    Console.WriteLine(reader);
    connection.Close();
}