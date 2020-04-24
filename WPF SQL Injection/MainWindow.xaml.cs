using System;
using System.Windows;
using System.Data.SQLite;
using System.Data;

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
                    cmd.CommandText = "DROP TABLE IF EXISTS usuarios";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"CREATE TABLE usuarios(id INTEGER PRIMARY KEY,
                    username TEXT, password TEXT, lastname TEXT, tipoacesso TEXT)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO usuarios(username, password, lastname, tipoacesso) VALUES('mateus','ojsojsojs','Queiroz','Administrador')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO usuarios(username, password, lastname, tipoacesso) VALUES('marcos','passwo','Silva','ReadOnly')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO usuarios(username, password, lastname, tipoacesso) VALUES('leticia','isuiusius','Silveira','ReadOnly')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO usuarios(username, password, lastname, tipoacesso) VALUES('miguel','asasdasd','Felipe','Administrador')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO usuarios(username, password, lastname, tipoacesso) VALUES('davi','2222222','Moraes','ReadOnly')";
                    cmd.ExecuteNonQuery();
                }                
            }
        }

        ///Below are some queries to test just copy and paste them
        //1 or 1=1; drop table usuarios
        //1 or 1=1; INSERT INTO usuarios(username, password, lastname, tipoacesso) VALUES('hacker','xxxxxx','robot','Administrador')
        //1 UNION SELECT username, password, tipoacesso from usuarios
        //1 UNION SELECT COUNT(*) ,username, tipoacesso FROM usuarios


        ///Using Interpreter
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var con = new SQLiteConnection(cs))
            {
                con.Open();

                try
                {
                    using (var cmd = new SQLiteCommand("SELECT username, lastname, tipoacesso FROM usuarios WHERE id=" + campoPesquisa.Text, con))
                    {
                        using (SQLiteDataAdapter sqlAdap = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sqlAdap.Fill(dt);
                            TabelaUsuarios.ItemsSource = null;
                            TabelaUsuarios.ItemsSource = dt.AsDataView();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        ///Validating data
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var positiveIntRegex = new Regex("^[0-9]+$");
        //    if (positiveIntRegex.IsMatch(campoPesquisa.Text))
        //    {
        //        using (var con = new SQLiteConnection(cs))
        //        {
        //            con.Open();

        //            try
        //            {
        //                using (var cmd = new SQLiteCommand("SELECT username, lastname, tipoacesso FROM usuarios WHERE id=" + campoPesquisa.Text, con))
        //                {
        //                    using (SQLiteDataAdapter sqlAdap = new SQLiteDataAdapter(cmd))
        //                    {
        //                        DataTable dt = new DataTable();
        //                        sqlAdap.Fill(dt);
        //                        TabelaUsuarios.ItemsSource = null;
        //                        TabelaUsuarios.ItemsSource = dt.AsDataView();
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
        //        MessageBox.Show("Invalid Character");
        //    }
        //}

        ///Using ORM Linq + EntityFramework
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{      
        //    using (var db = new UsuarioContexto())
        //    {                
        //        try
        //        {
        //            DataTable dt = new DataTable();                    
        //            TabelaUsuarios.ItemsSource = null;
        //            TabelaUsuarios.ItemsSource = db.Usuarios
        //                .Where(x => x.id == Int16.Parse(campoPesquisa.Text)).ToList();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //}

    }
}

