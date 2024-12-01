using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public static class DatabaseConfig
{
    public static string connectionString { get; private set; }

    static DatabaseConfig()
    {
        LoadSqlConfiguration();
    }

    private static void LoadSqlConfiguration()
    {
  




               // connectionString = $"Data Source=Selim-PC;Initial Catalog=MaintenanceDB ;Integrated Security=True;Encrypt=False";

              connectionString = $"Data Source=192.168.50.5;Initial Catalog=MaintenanceDB;User Id=sa;Password=comsys@123;Encrypt=False";
            }
           


    }






