namespace DAL;
using MySql.Data.MySqlClient;
using BOL_poco_;
//set database connection and query execution
//Dao layer
    public class DBmanager
    {
    //use static String for data base connection so it can be use anywhere
    //configure server,port,database,user,password
    public static string constring=@"server=localhost;port=3306;user=root;password=Mohit1234;Database=dotnet";
    //find all employees
    //make all method static so they are loaded once and can be usable wherever it requires
    public static List<Employee> GetEmployees()
    {
        List<Employee> list = new List<Employee>();
        //create database connection using above static string consisting of database config
        MySqlConnection conn = new MySqlConnection();
        conn.ConnectionString = constring;
        //use a try catch block to handle exception
        try
        {
            //open a conn
            conn.Open();
            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = conn;
            string query = "select * from emp"; 
            //structure same like JDBC known in dotnet as Odbc
            mySqlCommand.CommandText = query;
            //to fetch data i.e select query executeReader
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                int id = int.Parse(reader["id"].ToString());
                string name = reader["name"].ToString();
                double salary = double.Parse(reader["salary"].ToString());
                Employee emp = new Employee(id,name,salary);
                list.Add(emp);
            }
       
        }catch(Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
        finally { conn.Close(); }
        return list;
    }

    //get employee by id
    public static Employee GetEmployeeById(int id)
    {
        MySqlConnection conn = new MySqlConnection();
        conn.ConnectionString=constring;
         Employee emp = new Employee();
        try
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            string query = "select * from emp where id=" + id;
            cmd.CommandText = query;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int empid = int.Parse(reader["id"].ToString());
                string name = reader["name"].ToString();
                double salary = double.Parse(reader["salary"].ToString());
                emp = new Employee(empid, name, salary);
            }
        }catch(System.Exception ex) {
            Console.WriteLine(ex.Message);
        }
        //close a conn
        finally { conn.Close(); }
        return emp;
    }

    public static void insert(Employee emp)
    {
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString=constring;
        try
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            String query = "insert into emp values(" + emp.Id + ",'" + emp.Name + "'," + emp.Salary + ")";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
        }catch(System.Exception ex)
        {
            Console.WriteLine(ex.Message);
        }finally { con.Close(); }   
    }
    public static void delete(int empid)
    {
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = constring;
        try
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            string query = "delete from emp where id=" + empid;
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally { con.Close(); }
    }
    public static void update(Employee emp)
    {
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = constring;
        try
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            string query = "update emp set name='" + emp.Name + "',salary=" + emp.Salary + " where id=" + emp.Id;
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally { con.Close(); }
    }
}
