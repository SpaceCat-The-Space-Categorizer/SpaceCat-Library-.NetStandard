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
    class DatabaseTests
    {
        public void populationTestArea(String filepath)
        {
            //creates a SQLiteConnection using the c# 'using' syntax
            //this means that I don't have to call a close/dispose method, as it will do it for me
            using (var connection = new SQLiteConnection(filepath))
            {
                connection.Open();

                //create a SQLiteCommand that uses parameter values with which to associate with
                //this helps prevent against SQLinjection into the server
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Area(Id, Name, Building, Floor, MaxCap, Category) VALUES(@id, @name, @building, @floor, @cap, @category)";

                    //add parameters associated with each value
                    command.Parameters.AddWithValue("@id", 00);
                    command.Parameters.AddWithValue("@name", "Cafe");
                    command.Parameters.AddWithValue("@building", "LIB");
                    command.Parameters.AddWithValue("@floor", 01);
                    command.Parameters.AddWithValue("@cap", 42);
                    command.Parameters.AddWithValue("@category", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();

                    Console.WriteLine("row inserted");

                    //It can be reused, this can be used nicely with a while/for loop
                    command.Parameters.AddWithValue("@id", 01);
                    command.Parameters.AddWithValue("@name", "Lobby");
                    command.Parameters.AddWithValue("@building", "LIB");
                    command.Parameters.AddWithValue("@floor", 01);
                    command.Parameters.AddWithValue("@cap", 95);
                    command.Parameters.AddWithValue("@category", "Silent Study");
                    command.Prepare();
                    command.ExecuteNonQuery();

                    Console.WriteLine("row inserted");

                    command.Parameters.AddWithValue("@id", 15);
                    command.Parameters.AddWithValue("@name", "FishBone");
                    command.Parameters.AddWithValue("@building", "LIB");
                    command.Parameters.AddWithValue("@floor", 02);
                    command.Parameters.AddWithValue("@cap", 50);
                    command.Parameters.AddWithValue("@category", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();

                    Console.WriteLine("row inserted");


                    String temp = DateTime.Today.ToString();

                    command.CommandText = "INSERT INTO AreaSurvey(AreaSurveyed, Date, SurveyNum, FilledSeats, Notes) VALUES(@id, @date, @surveynum, @filledseats, @notes)";

                    command.Parameters.AddWithValue("@id", 00);
                    command.Parameters.AddWithValue("@date", temp);
                    command.Parameters.AddWithValue("@surveynum", 01);
                    command.Parameters.AddWithValue("@filledseats", 42);
                    command.Parameters.AddWithValue("@notes", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();

                    command.Parameters.AddWithValue("@id", 00);
                    command.Parameters.AddWithValue("@date", temp);
                    command.Parameters.AddWithValue("@surveynum", 02);
                    command.Parameters.AddWithValue("@filledseats", 32);
                    command.Parameters.AddWithValue("@notes", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();

                    command.Parameters.AddWithValue("@id", 00);
                    command.Parameters.AddWithValue("@date", temp);
                    command.Parameters.AddWithValue("@surveynum", 03);
                    command.Parameters.AddWithValue("@filledseats", 28);
                    command.Parameters.AddWithValue("@notes", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();


                    command.Parameters.AddWithValue("@id", 00);
                    command.Parameters.AddWithValue("@date", temp);
                    command.Parameters.AddWithValue("@surveynum", 04);
                    command.Parameters.AddWithValue("@filledseats", 20);
                    command.Parameters.AddWithValue("@notes", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();

                    command.Parameters.AddWithValue("@id", 01);
                    command.Parameters.AddWithValue("@date", temp);
                    command.Parameters.AddWithValue("@surveynum", 01);
                    command.Parameters.AddWithValue("@filledseats", 81);
                    command.Parameters.AddWithValue("@notes", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();

                    command.Parameters.AddWithValue("@id", 01);
                    command.Parameters.AddWithValue("@date", temp);
                    command.Parameters.AddWithValue("@surveynum", 02);
                    command.Parameters.AddWithValue("@filledseats", 25);
                    command.Parameters.AddWithValue("@notes", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();

                    command.Parameters.AddWithValue("@id", 01);
                    command.Parameters.AddWithValue("@date", temp);
                    command.Parameters.AddWithValue("@surveynum", 03);
                    command.Parameters.AddWithValue("@filledseats", 79);
                    command.Parameters.AddWithValue("@notes", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();


                    command.Parameters.AddWithValue("@id", 01);
                    command.Parameters.AddWithValue("@date", temp);
                    command.Parameters.AddWithValue("@surveynum", 04);
                    command.Parameters.AddWithValue("@filledseats", 1);
                    command.Parameters.AddWithValue("@notes", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();

                    command.Parameters.AddWithValue("@id", 15);
                    command.Parameters.AddWithValue("@date", temp);
                    command.Parameters.AddWithValue("@surveynum", 01);
                    command.Parameters.AddWithValue("@filledseats", 34);
                    command.Parameters.AddWithValue("@notes", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();

                    command.Parameters.AddWithValue("@id", 15);
                    command.Parameters.AddWithValue("@date", temp);
                    command.Parameters.AddWithValue("@surveynum", 02);
                    command.Parameters.AddWithValue("@filledseats", 7);
                    command.Parameters.AddWithValue("@notes", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();

                    command.Parameters.AddWithValue("@id", 15);
                    command.Parameters.AddWithValue("@date", temp);
                    command.Parameters.AddWithValue("@surveynum", 03);
                    command.Parameters.AddWithValue("@filledseats", 49);
                    command.Parameters.AddWithValue("@notes", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();

                    command.Parameters.AddWithValue("@id", 15);
                    command.Parameters.AddWithValue("@date", temp);
                    command.Parameters.AddWithValue("@surveynum", 04);
                    command.Parameters.AddWithValue("@filledseats", 15);
                    command.Parameters.AddWithValue("@notes", "Group Study");
                    command.Prepare();
                    command.ExecuteNonQuery();
                }
            }
        }

        //basic selection test that selects all rows from the Area table
        public void selectionTestArea(String filepath)
        {
            //creates a SQLiteConnection using the c# 'using' syntax
            //this means that I don't have to call a close/dispose method, as it will do it for me
            using (var connection = new SQLiteConnection(filepath))
            {
                connection.Open();

                //querry to be performed
                string statement = "SELECT * FROM Area";

                //create a SQLiteCommand
                using (var command = new SQLiteCommand(statement, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        //while there are rows to be read do...
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetName(0)}: {reader.GetInt32(0)} \n{reader.GetName(1)}: {reader.GetString(1)} \n{reader.GetName(2)}: {reader.GetString(2)} \n{reader.GetName(3)}: {reader.GetInt32(3)} \n{reader.GetName(4)}: {reader.GetInt32(4)} \n{reader.GetName(5)}: {reader.GetString(5)}\n");
                        }
                    }

                    command.CommandText = "SELECT * FROM CombinedView";

                    //create a SQLiteCommand

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        //while there are rows to be read do...
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetName(0)}: {reader.GetString(0)} \n{reader.GetName(1)}: {reader.GetInt32(1)} \n{reader.GetName(2)}: {reader.GetInt32(2)} \n{reader.GetName(3)}: {reader.GetFloat(3)} \n{reader.GetName(4)}: {reader.GetInt32(4)} \n");
                        }
                    }
                }
            }
        }
    }
}
