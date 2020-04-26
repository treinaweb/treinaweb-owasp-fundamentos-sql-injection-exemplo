using System;
using System.Windows;
using System.Data.SQLite;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;

namespace WPF_SQL_Injection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string cs = @"URI=file:C:\SQLite\test.db";
        public MainWindow()
        {
            InitializeComponent();            
            ///Create our example database
            using (var con = new SQLiteConnection(cs))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(con))
                {                    
                    cmd.CommandText = "DROP TABLE IF EXISTS users";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"CREATE TABLE users(id INTEGER PRIMARY KEY,
                    username TEXT, password TEXT, lastname TEXT, accesstype TEXT)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO users(username, password, lastname, accesstype) VALUES('mateus','ojsojsojs','Queiroz','Administrator')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO users(username, password, lastname, accesstype) VALUES('marcos','passwo','Silva','ReadOnly')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO users(username, password, lastname, accesstype) VALUES('leticia','isuiusius','Silveira','ReadOnly')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO users(username, password, lastname, accesstype) VALUES('miguel','asasdasd','Felipe','Administrator')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO users(username, password, lastname, accesstype) VALUES('davi','2222222','Moraes','ReadOnly')";
                    cmd.ExecuteNonQuery();
                }                
            }
        }

        ///Below are some queries to test, just copy and paste them
        //1 or 1=1; drop table users
        //1 or 1=1; INSERT INTO users(username, password, lastname, accesstype) VALUES('hacker','xxxxxx','robot','Administrator')
        //1 UNION SELECT username, password, accesstype from users
        //1 UNION SELECT COUNT(*) ,username, accesstype FROM users


        ///Using Interpreter
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    using (var con = new SQLiteConnection(cs))
        //    {
        //        con.Open();

        //        try
        //        {
        //            using (var cmd = new SQLiteCommand("SELECT username, lastname, accesstype FROM users WHERE id=" + mySearch.Text, con))
        //            {
        //                using (SQLiteDataAdapter sqlAdap = new SQLiteDataAdapter(cmd))
        //                {
        //                    DataTable dt = new DataTable();
        //                    sqlAdap.Fill(dt);
        //                    UsersTable.ItemsSource = null;
        //                    UsersTable.ItemsSource = dt.AsDataView();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //}

        ///Using Interpreter but and validating data
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var positiveIntRegex = new Regex("^[0-9]+$");
        //    if (positiveIntRegex.IsMatch(mySearch.Text))
        //    {
        //        using (var con = new SQLiteConnection(cs))
        //        {
        //            con.Open();

        //            try
        //            {
        //                using (var cmd = new SQLiteCommand("SELECT username, lastname, accesstype FROM users WHERE id=" + mySearch.Text, con))
        //                {
        //                    using (SQLiteDataAdapter sqlAdap = new SQLiteDataAdapter(cmd))
        //                    {
        //                        DataTable dt = new DataTable();
        //                        sqlAdap.Fill(dt);
        //                        UsersTable.ItemsSource = null;
        //                        UsersTable.ItemsSource = dt.AsDataView();
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Invalid Character(s)");
        //    }
        //}

        ///Using ORM  - EntityFramework with LINQ
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new UserContext())
            {
                try
                {
                    DataTable dt = new DataTable();
                    UsersTable.ItemsSource = null;
                    UsersTable.ItemsSource = db.Users
                        .Where(x => x.id == Int16.Parse(mySearch.Text)).ToList();

                    //Just ommiting the password column don't do it on a real application :) this is just a test app to demonstrate how SQL injection works, so it is OK :)
                    UsersTable.Columns[2].Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}

