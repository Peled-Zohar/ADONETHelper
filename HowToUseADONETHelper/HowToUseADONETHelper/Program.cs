using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ADONETHelper;
using System.Data;

namespace HowToUseADONETHelper
{
    class Program
    {

        /*
         *      The following code shows how to use some of the methods of the IDBHelper.
         *      Methods that are shown in this demo are:
         *      
         *      In class DBHelperFactory: 
         *      GetInstance
         *      
         *      In class IDBHelper:
         *      CreateParameter
         *      CreateOutputParameter
         *      ExecuteNonQuery 
         *      ExecuteReader
         *      FillDataSet
         *      ExecuteScalar
         *      
         *      In class ParametersCollectionExtensions:
         *      GetValueOrDefault<T>
         *      
         *      In class IDataReaderExtensions:
         *      GetValueOrDefault<T>
         */

        private static IDBHelper _DB;

        static void Main(string[] args)
        {

            Console.WriteLine("Look at the code... Press any key to exit.");
            Console.ReadLine();

        }

        /// <summary>
        /// Initializes and instance of a class implementing the IDBHelper interface.
        /// </summary>
        private static void InitializeDBHelper()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            _DB = DBHelperFactory.GetInstance(DataBaseType.SQLServer, connectionString);
        }

        /// <summary>
        /// Executes an insert statement with multiple parameters.
        /// </summary>
        /// <returns></returns>
        private static int ExecuteNonQuery()
        {
            var sql = "INSERT INTO MyTable(Col1, Col2) VALUES(@val1, @val2)";
            var parameters = new IDbDataParameter[]
            {
                _DB.CreateParameter("@val1", ADONETType.VarChar, "My parameter"),
                _DB.CreateParameter("@val2", ADONETType.Int, 15)
            };
            int returnValue = 0;
            try
            {
                returnValue = _DB.ExecuteNonQuery(sql, CommandType.Text, parameters);
            }
            catch (Exception e)
            {
                // Exception handling code goes here...
            }
            return returnValue;
        }

        /// <summary>
        /// Executes a select statement that returns a single value.
        /// </summary>
        /// <returns>The string returned from the select statement.</returns>
        private static string ExecueScalar()
        {
            var sql = "SELECT 'Hello World'";
            string returnValue = string.Empty;
            try
            {
                returnValue = _DB.ExecuteScalar<string>(sql, System.Data.CommandType.Text);
            }
            catch(Exception e)
            {
                // Exception handling code goes here...
            }
            return returnValue;
        }

        /// <summary>
        /// Fills a dataset and also returning the value of an output parameter.
        /// </summary>
        /// <param name="outputValue">A double to hold the value of the output parameter.</param>
        /// <returns>A dataset containing the result of the stored procedure.</returns>
        private static DataSet FillDataset(out double outputValue)
        {
            DataSet dataset = null;
            outputValue = default(double);
            var sql = "stp_GetMeTheDataAndSomeDoubleValue";
            var outputParam = _DB.CreateOutputParameter(@"MyOutputParam", ADONETType.Double);
            try
            {
                dataset = _DB.FillDataSet(sql, CommandType.StoredProcedure, outputParam);
                outputValue = outputParam.GetValueOrDefault<double>();
            }
            catch (Exception e)
            {
                // Exception handling code goes here...
            }
            return dataset;
        }

        /// <summary>
        /// Executes a datareader, uses an anonymous method to populate a list of persons.
        /// </summary>
        private static List<Person> ExecuteReaderAnonymousPopulator()
        {
            var people = new List<Person>();
            var sql = "SELECT First_Name, Last_Name, Age FROM Persons WHERE";
            try
            {
                _DB.ExecuteReader
                (
                    sql,
                    CommandType.Text,
                    reader =>
                    {
                        while (reader.Read())
                        {
                            people.Add(
                            new Person()
                                {
                                    FirstName = reader.GetValueOrDefault<string>("First_Name"),
                                    LastName = reader.GetValueOrDefault<string>("Last_Name"),
                                    Age = reader.GetValueOrDefault<int>("Age")
                                }
                            );
                        }
                        return false;
                    }
                );
            }
            catch (Exception e)
            {
                // Exception handling code goes here...
            }
            return people;
        }

        /// <summary>
        /// Executes a datareader, uses a named method to populate a list of persons.
        /// </summary>
        private static List<Person> ExecuteReader()
        {
            var people = new List<Person>();
            var sql = "SELECT First_Name, Last_Name, Age FROM Persons WHERE";
            try
            {
                _DB.ExecuteReader
                (
                    sql,
                    CommandType.Text,
                    (reader) => PopulatePersonsList(reader, people)
                );
                  
            }
            catch (Exception e)
            {
                // Exception handling code goes here...
            }
            return people;
        }

        /// <summary>
        /// Populates a list of persons from the dataReader.
        /// </summary>
        /// <param name="reader">An instance of IDataReader</param>
        /// <param name="people">The list of persons to populate.</param>
        /// <returns></returns>
        private static bool PopulatePersonsList(IDataReader reader, List<Person> people)
        {
            var recordsFound = false;
            while (reader.Read())
            {
                people.Add(
                new Person()
                {
                    FirstName = reader.GetValueOrDefault<string>("First_Name"),
                    LastName = reader.GetValueOrDefault<string>("Last_Name"),
                    Age = reader.GetValueOrDefault<int>("Age")
                }
                );
                recordsFound  = true;
            }
            return recordsFound;
        }

        /// <summary>
        /// Executes a stored procedure with input and output parameters.
        /// </summary>
        /// <param name="person">An instance of Person to hold values for the parameters.</param>
        private static void ExecuteNonQueryWithOutputParameters(Person person)
        {
            var parameters = new IDbDataParameter[]
            {
                _DB.CreateOutputParameter("@Age", ADONETType.Int),
                _DB.CreateOutputParameter("@FirstName", ADONETType.NVarChar, 4000),
                _DB.CreateOutputParameter("@LastName", ADONETType.NVarChar, 4000),
                _DB.CreateParameter("@Id", ADONETType.Int, person.Id)
            };
            _DB.ExecuteNonQuery("stp_GetPersonDetailsById", CommandType.StoredProcedure, parameters);
            person.Age = parameters.GetValueOrDefault<int>("@Age");
            person.FirstName = parameters.GetValueOrDefault<string>("@FirstName");
            person.LastName = parameters.GetValueOrDefault<string>("@LastName");
        }
    }

    /// <summary>
    /// A simple data entity class to help demonstrate ADONETHelper methods.
    /// </summary>
    public class Person
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
