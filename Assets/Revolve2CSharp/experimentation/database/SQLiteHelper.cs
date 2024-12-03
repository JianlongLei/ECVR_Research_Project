// using System;
// using System.IO;
// using Microsoft.Data.Sqlite;

// namespace Revolve2.Database
// {
//     /// <summary>
//     /// Utility for opening SQLite databases.
//     /// </summary>
//     public static class SQLiteHelper
//     {
//         /// <summary>
//         /// Opens a synchronous SQLite database connection.
//         /// </summary>
//         /// <param name="dbFile">The database file path.</param>
//         /// <param name="openMethod">The method to use when opening the database.</param>
//         /// <returns>An open SQLite connection.</returns>
//         public static SqliteConnection OpenDatabase(string dbFile, OpenMethod openMethod = OpenMethod.OpenIfExists)
//         {
//             HandleDatabaseFile(dbFile, openMethod);
//             var connection = new SqliteConnection($"Data Source={dbFile}");
//             connection.Open();
//             return connection;
//         }

//         /// <summary>
//         /// Handles database file creation or validation based on the open method.
//         /// </summary>
//         /// <param name="dbFile">The database file path.</param>
//         /// <param name="openMethod">The method to use when opening the database.</param>
//         /// <exception cref="InvalidOperationException">Thrown when the operation cannot proceed based on the method.</exception>
//         private static void HandleDatabaseFile(string dbFile, OpenMethod openMethod)
//         {
//             var directory = Path.GetDirectoryName(dbFile);
//             if (directory != null)
//             {
//                 Directory.CreateDirectory(directory);
//             }

//             var exists = File.Exists(dbFile);
//             switch (openMethod)
//             {
//                 case OpenMethod.OpenIfExists:
//                     if (!exists)
//                     {
//                         throw new InvalidOperationException($"Database does not exist: {dbFile}");
//                     }
//                     break;

//                 case OpenMethod.OpenOrCreate:
//                     // Ensure the directory exists, no further action needed.
//                     break;

//                 case OpenMethod.NotExistsAndCreate:
//                     if (exists)
//                     {
//                         throw new InvalidOperationException($"Database already exists: {dbFile}");
//                     }
//                     break;

//                 case OpenMethod.OverwriteIfExists:
//                     if (exists)
//                     {
//                         File.Delete(dbFile);
//                     }
//                     break;

//                 default:
//                     throw new ArgumentOutOfRangeException(nameof(openMethod), openMethod, "Invalid OpenMethod specified.");
//             }
//         }
//     }
// }
