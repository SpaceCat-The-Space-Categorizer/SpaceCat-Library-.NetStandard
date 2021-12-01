using System;
using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpaceCat
{
    public class DatabaseFactory
    {
        //grabs the file path that the .exe is currently running in
        private readonly String BaseFilePath = Persistence.BaseFilePath;

        //a string to be constructed as to connect to the database
        private string _ConstructedFilePath;
        public string ConstructedFilePath { get; set; }
        
        public string BuildFullPath(string dbName) { 

            //simple check to see if there is a file extension in filename and remove it
            if (dbName.Contains('.'))
            {
                String[] temp = dbName.Split('.');
                dbName = temp[0];
            }

            //add .db file extension to our file name
            dbName += ".db";

            //construct a temporary path to construct the directory, if not already created
            string tempPath = Path.Combine(BaseFilePath, FolderName);

            //add the filename to the previously created path string
            //also the plaintext string @"URI=file" must be in there otherwise
            //the SQLiteConnection will fail to open.
            string returnString = @"URI=file:" + tempPath + dbName;

            //checks to see if .db file exists, and if not creates one
            if (!File.Exists(_ConstructedFilePath))
            {
                //checks to see if directory exists, and if not creates one
                if (!Directory.Exists(tempPath))
                {
                    //create directory
                    Directory.CreateDirectory(tempPath);
                }

                //create file at specified filepath in connectionString
                SQLiteConnection.CreateFile(tempPath + dbName);
                Console.WriteLine("New File has been created.");
            }
            else
            {
                Console.WriteLine("File has already been created.");
            }
            ConstructedFilePath += ";foreign keys=true;";
            Console.WriteLine(dbName + " is the file selected.");
            return returnString;
        }

        //the name of the folder to be used for the databases
        //will be located in the same folder as the .exe
        private readonly String FolderName = @"Databases\";

        //most basic constructor
        //i dont think this will ever be called.
        public DatabaseFactory() { }

        //constructor that will see most use.
        //takes a filename as a string and creates the connection string and the tables.
        [JsonSerializer]
        public DatabaseFactory(string constructedFilePath)
        {
            if (constructedFilePath.Contains("URI"))
            {
                ConstructedFilePath = constructedFilePath;
            }
            else
            {
                ConstructedFilePath = BuildFullPath(constructedFilePath);
            }
            CreateTables(false);
        }

        public void AverageData(string filename)
        {
            //this is the string we will altering to do most of the work
            String currentLine = "";

            //this is the list that we add all the lines to so we can write it to a file
            List<String> allLines = new List<String> { };

            //simple check to see if there is a file extension in filename and remove it
            if (filename.Contains('.'))
            {
                String[] temp = filename.Split('.');
                filename = temp[0];
            }

            //add .db file extension to our file name
            filename += ".csv";

            //creates a SQLiteConnection using the c# 'using' syntax
            using (var connection = new SQLiteConnection(ConstructedFilePath))
            {
                connection.Open();

                //first select statement from the Area table
                string statement = "SELECT * FROM CombinedView";

                //create a SQLiteCommand that uses the above statment and connection
                using (var command = new SQLiteCommand(statement, connection))
                {

                    //open SQLiteDataReader to execute select statement
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        //add column names to the first line and add to our List
                        currentLine = $"{reader.GetName(0)}," +
                                        $"{reader.GetName(1)}," +
                                        $"{reader.GetName(2)}," +
                                        $"{reader.GetName(3)}," +
                                        $"{reader.GetName(4)}";
                        allLines.Add(currentLine);

                        //while there are rows to be read, read each row, format it, and add the line to the List
                        while (reader.Read())
                        {
                            currentLine = $"{reader.GetString(0)}," +
                                            $"{reader.GetInt32(1)}," +
                                            $"{reader.GetInt32(2)}," +
                                            $"{reader.GetFloat(3)}," +
                                            $"{reader.GetInt32(4)}";
                            allLines.Add(currentLine);
                            Console.WriteLine(currentLine);
                        }
                    }
                    //writes the List that has all our row data to the filename specified.
                    File.WriteAllLines((GetBaseFilePath() + GetFolderName() + filename), allLines);
                }
            }
        }

        //export all the rows of the Area table into a .csv file
        public void ExportCSV(String filename)
        {
            //this is the string we will altering to do most of the work
            string currentLine = "";

            //this is the list that we add all the lines to so we can write it to a file
            List<String> allLines = new List<String> { };

            //simple check to see if there is a file extension in filename and remove it
            if (filename.Contains('.'))
            {
                String[] temp = filename.Split('.');
                filename = temp[0];
            }

            //add .db file extension to our file name
            filename += ".csv";

            //creates a SQLiteConnection using the c# 'using' syntax
            //this means that I don't have to call a close/dispose method, as it will do it for me 
            using (var connection = new SQLiteConnection(ConstructedFilePath))
            {
                connection.Open();

                //first select statement from the Area table
                string statement = "SELECT * FROM AreaView";

                //create a SQLiteCommand that uses the above statment and connection
                using (var command = new SQLiteCommand(statement, connection))
                {

                    //open SQLiteDataReader to execute select statement
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        //grab the column names, format it, and write the line to the List
                        currentLine = $"{reader.GetName(0)}," +
                                        $"{reader.GetName(1)}," +
                                        $"{reader.GetName(2)}," +
                                        $"{reader.GetName(3)}," +
                                        $"{reader.GetName(4)}," +
                                        $"{reader.GetName(5)}";
                        allLines.Add(currentLine);

                        //while there are rows to be read, read each row, format it, and add the line to the List
                        while (reader.Read())
                        {
                            currentLine = $"{reader.GetInt32(0)}," +
                                            $"{reader.GetString(1)}," +
                                            $"{reader.GetString(2)}," +
                                            $"{reader.GetInt32(3)}," +
                                            $"{reader.GetInt32(4)}," +
                                            $"{reader.GetString(5)}";
                            allLines.Add(currentLine);
                            Console.WriteLine(currentLine);
                        }

                        //add a blank line inbetween table data to seperate them
                        currentLine = "";
                        allLines.Add(currentLine);
                    }

                    //edit command's CommandTextas to reuse it
                    command.CommandText = "SELECT * FROM AreaSurveyView";

                    //open SQLiteDataReader to execute select statement
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        //grab the column names, format it, and write the line to the List
                        currentLine = $"{reader.GetName(0)}," +
                                        $"{reader.GetName(1)}," +
                                        $"{reader.GetName(2)}," +
                                        $"{reader.GetName(3)}," +
                                        $"{reader.GetName(4)}";
                        allLines.Add(currentLine);

                        //while there are rows to be read, read each row, format it, and add the line to the List
                        while (reader.Read())
                        {
                            currentLine = $"{reader.GetInt32(0)}," +
                                            $"{reader.GetDateTime(1)}," +
                                            $"{reader.GetInt32(2)}," +
                                            $"{reader.GetInt32(3)}," +
                                            $"{reader.GetString(4)}";
                            allLines.Add(currentLine);
                            Console.WriteLine(currentLine);
                        }
                        //add a blank line inbetween table data to seperate them
                        currentLine = "";
                        allLines.Add(currentLine);
                    }

                    string filePath = Path.Combine(GetBaseFilePath(), GetFolderName(), filename);

                    if (!(File.Exists(filePath)))
                    {
                        File.Create(filePath).Close();
                    }
                    //writes the List that has all our row data to the filename specified.
                    File.WriteAllLines(filePath, allLines);
                }
            }
        }

        public void InsertArea(Area areaToInsert, Floor areaFloor, Building areaBuilding)
        {
            //creates a SQLiteConnection using the c# 'using' syntax
            //this means that I don't have to call a close/dispose method, as it will do it for me
            using (var connection = new SQLiteConnection(ConstructedFilePath))
            {
                connection.Open();

                //create a SQLiteCommand that uses parameter values with which to associate with
                //this helps prevent against SQLinjection into the server
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Area(Id, Name, Building, Floor, MaxCap, Category) VALUES(@id, @name, @building, @floor, @cap, @category)";

                    //add parameters associated with each value
                    command.Parameters.AddWithValue("@id", areaToInsert.AreaID);
                    command.Parameters.AddWithValue("@name", areaToInsert.AreaName);
                    command.Parameters.AddWithValue("@building", areaBuilding.Name);
                    command.Parameters.AddWithValue("@floor", areaFloor.FloorNumber);
                    command.Parameters.AddWithValue("@cap", areaToInsert.Capacity);
                    command.Parameters.AddWithValue("@category", areaToInsert.TagsToString());
                    command.Prepare();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ModifyArea(Area areaToModify, Floor areaFloor, Building areaBuilding)
        {
            using (var connection = new SQLiteConnection(ConstructedFilePath))
            {
                connection.Open();

                //create a SQLiteCommand that uses parameter values with which to associate with
                //this helps prevent against SQLinjection into the server
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"UPDATE Area
                                            SET Id = " + areaToModify.AreaID +
                                            @",   Name = '" + areaToModify.AreaName +
                                            @"',  Building = '" + areaBuilding.Name +
                                            @"',  Floor = " + areaFloor.FloorNumber +
                                            @",   MaxCap = " + areaToModify.Capacity +
                                            @",   Category = '" + areaToModify.TagsToString() +
                                            @"'   WHERE Id = " + areaToModify.AreaID;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertAreaSurvey(AreaSurvey surveyToInsert)
        {

            using (var connection = new SQLiteConnection(ConstructedFilePath))
            {
                connection.Open();

                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO AreaSurvey(AreaSurveyed, Date, SurveyNum, FilledSeats, Notes) VALUES (@areaSurveyed, @date, @surveyNum, @filledSeats, @notes)";

                    command.Parameters.AddWithValue("@areaSurveyed", surveyToInsert.AreaID);
                    command.Parameters.AddWithValue("@date", surveyToInsert.TimeSurveyed);
                    command.Parameters.AddWithValue("@surveyNum", surveyToInsert.SurveyNumber);
                    command.Parameters.AddWithValue("@filledSeats", surveyToInsert.FilledSeats);
                    command.Parameters.AddWithValue("@notes", surveyToInsert.AdditionalNotes);

                    command.Prepare();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DropTables()
        {
            //creates a SQLiteConnection using the c# 'using' syntax
            //this means that I don't have to call a close/dispose method, as it will do it for me
            using (var connection = new SQLiteConnection(ConstructedFilePath))
            {

                //open the connection
                connection.Open();

                //create a SQLiteCommand that im panning on reusing to drop both tables
                using (var command = new SQLiteCommand(connection))
                {

                    //drop tables in reverse order to avoid any parent/child errors that might arise
                    //drop table area survey if it exists
                    command.CommandText = "DROP TABLE IF EXISTS AreaSurvey";
                    command.ExecuteNonQuery();

                    //drop table area if it exists
                    command.CommandText = "DROP TABLE IF EXISTS Area";
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DropViews()
        {
            //creates a SQLiteConnection using the c# 'using' syntax
            //this means that I don't have to call a close/dispose method, as it will do it for me
            using (var connection = new SQLiteConnection(ConstructedFilePath))
            {

                //open the connection
                connection.Open();

                //create a SQLiteCommand that im panning on reusing to drop both tables
                using (var command = new SQLiteCommand(connection))
                {

                    //drop tables in reverse order to avoid any parent/child errors that might arise
                    //drop table area survey if it exists
                    command.CommandText = "DROP VIEW IF EXISTS CombinedView";
                    command.ExecuteNonQuery();

                    command.CommandText = "DROP VIEW IF EXISTS AreaSurveyView";
                    command.ExecuteNonQuery();

                    //drop table area if it exists
                    command.CommandText = "DROP VIEW IF EXISTS AreaView";
                    command.ExecuteNonQuery();
                }
            }
        }


        //create tables for database
        //BEWARE!!!!!!
        //IF RAN MORE THAN ONCE ON THE SAME DATABASE IT WILL ERASE THE DATA
        //MIGHT WANT TO PUT A WARNING POP UP FOR ADMIN IN APP
        //Should be able to rollback if something like that happens???
        //haven't done research into that part of SQLite yet
        public void CreateTables(bool mode)
        {
            //drop tables if requested in parameters
            if (mode == true)
            {
                DropTables();
            }

            //creates a SQLiteConnection using the c# 'using' syntax
            //this means that I don't have to call a close/dispose method, as it will do it for me
            using (var connection = new SQLiteConnection(ConstructedFilePath))
            {

                //open the connection
                connection.Open();

                //create a SQLiteCommand that im planning on reusing to create both tables
                using (var command = new SQLiteCommand(connection))
                {


                    //create table area with varying attributes
                    //no need to put id attribute in insert statements seeing as it auto increments
                    command.CommandText = @"CREATE TABLE Area(  Id INT NOT NULL PRIMARY KEY,
                                                        Name VARCHAR(30),
                                                        Building VARCHAR(30),
                                                        Floor INT,
                                                        MaxCap INT,
                                                        Category VARCHAR(30))";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Table Area created");

                    //create table area with varying attributes
                    //no need to put id attribute in insert statements seeing as it auto increments
                    command.CommandText = @"CREATE TABLE AreaSurvey(AreaSurveyed INT NOT NULL,
                                                            Date DATE,
                                                            SurveyNum INT,
                                                            FilledSeats INT,
                                                            Notes VARCHAR(300),
                                                            PRIMARY KEY(AreaSurveyed, Date, SurveyNum),
                                                            FOREIGN KEY(AreaSurveyed)
                                                            REFERENCES Area(Id) )";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Table AreaSurvey created");
                }
            }
        }

        public void CreateViews(bool mode)
        {
            if (mode == true)
            {
                DropViews();
            }

            //creates a SQLiteConnection using the c# 'using' syntax
            //this means that I don't have to call a close/dispose method, as it will do it for me
            using (var connection = new SQLiteConnection(ConstructedFilePath))
            {

                //open the connection
                connection.Open();

                //create a SQLiteCommand that im planning on reusing to create both tables
                using (var command = new SQLiteCommand(connection))
                {

                    command.CommandText = @"CREATE VIEW AreaView(   ""Area Id"",
                                                            ""Area Name"",
                                                            Building,
                                                            Floor,
                                                            ""Maximum Capacity"",
                                                            ""Area Category"")
                                    AS
                                    SELECT* FROM Area";

                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE VIEW AreaSurveyView( ""Area Id"",
                                                                Date,
                                                                ""Survey Number"",
                                                                ""Filled Seats"",
                                                                ""Additional Notes"")
                                    AS
                                    SELECT* FROM AreaSurvey";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE VIEW CombinedView(   Building,
                                                                Floor,
                                                                ""Area ID"",
                                                                ""Average Usage"",
                                                                ""Peak Usage"")
                                    AS
                                    SELECT Building, Floor, AreaSurveyed, AVG(FilledSeats), MAX(FilledSeats) 
                                    FROM AreaSurvey, Area 
                                    WHERE Area.Id = AreaSurvey.AreaSurveyed 
                                    GROUP BY AreaSurveyed";

                    command.ExecuteNonQuery();
                }
            }
        }


        //setter for connectionString
        //its really more of a constructor
        

        public int GetNewSurveyNumber()
        {
            using (var connection = new SQLiteConnection(ConstructedFilePath))
            {
                connection.Open();

                using (var command = new SQLiteCommand(connection))
                {

                    command.CommandText = @"SELECT MAX(SurveyNum) FROM AreaSurvey";

                    var newSurveyNum = command.ExecuteScalar();
                    if (newSurveyNum == DBNull.Value)
                    {
                        return 0;
                    }
                    else
                    {
                        return (int)newSurveyNum + 1;
                    }
                }
            }
        }

        public int GetNewAreaID()
        {
            using (var connection = new SQLiteConnection(ConstructedFilePath))
            {
                connection.Open();

                using (var command = new SQLiteCommand(connection))
                {

                    command.CommandText = @"SELECT MAX(Id) FROM Area";

                    var newAreaId = command.ExecuteScalar();
                    if (newAreaId == DBNull.Value)
                    {
                        return 0;
                    }
                    else
                    {
                        return (int)newAreaId + 1;
                    }
                }
            }
        }

        //basic getter for baseString
        public String GetBaseFilePath()
        {
            return BaseFilePath;
        }

        //basic getter for folder
        public String GetFolderName()
        {
            return FolderName;
        }

    }
}
