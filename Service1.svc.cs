using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace WcfService2
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę klasy „Service1” w kodzie, usłudze i pliku konfiguracji.
    // UWAGA: aby uruchomić klienta testowego WCF w celu przetestowania tej usługi, wybierz plik Service1.svc lub Service1.svc.cs w eksploratorze rozwiązań i rozpocznij debugowanie.
    public class Service1 : IService1
    {
        public string GetData(string log)
        {
            SqlConnection Conn = new SqlConnection("workstation id = SQLBDcomu.mssql.somee.com; packet size = 4096; user id = zolinek89SQL; pwd = Zolinek899; data source = SQLBDcomu.mssql.somee.com; persist security info = False; initial catalog = SQLBDcomu");
            try
            {
                Conn.Open();
            }
            catch
            {
                
            }
            SqlCommand Cmd = new SqlCommand("select Message From Message where Reviver = @login", Conn);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@login";
            param.Value = log;
            Cmd.Parameters.Add(param);
            SqlDataReader Dr = Cmd.ExecuteReader();
            string mess = "NO MESSAGE";
            while (Dr.Read())
            {
                mess= Dr[0].ToString();
            }
            Dr.Close();
            SqlCommand Cmb = new SqlCommand("delete From Message where Reviver = @login", Conn);

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@login";
            param2.Value = log;

            Cmb.Parameters.Add(param2);
            Cmb.ExecuteNonQuery();
            Conn.Close();
            return mess;

        }
        public bool Login(string log, string pass)
        {
            SqlConnection Conn = new SqlConnection("workstation id = SQLBDcomu.mssql.somee.com; packet size = 4096; user id = zolinek89SQL; pwd = Zolinek899; data source = SQLBDcomu.mssql.somee.com; persist security info = False; initial catalog = SQLBDcomu");
            try
            {
                Conn.Open();
            }
            catch 
            {
                return false;
            }
            SqlCommand Cmd = new SqlCommand("select Password From Users where Login = @login", Conn);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@login";
            param.Value = log;
            Cmd.Parameters.Add(param);
            SqlDataReader Dr = Cmd.ExecuteReader();
            string outpass="";
            while(Dr.Read())
            {
                string outpassnocon = Dr[0].ToString();
                outpass = System.Text.RegularExpressions.Regex.Replace(outpassnocon, @"\s", "");

            }
            Dr.Close();
            Conn.Close();

            if (outpass.Equals(pass))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SendData(string value,string log,string rec)
        {
            SqlConnection Conn = new SqlConnection("workstation id = SQLBDcomu.mssql.somee.com; packet size = 4096; user id = zolinek89SQL; pwd = Zolinek899; data source = SQLBDcomu.mssql.somee.com; persist security info = False; initial catalog = SQLBDcomu");
            try
            {
                Conn.Open();
            }
            catch
            {
                return false;
            }
            SqlCommand Cmd = new SqlCommand("INSERT INTO Message ( Login, Message,Reviver ) VALUES (@log,@value,@rec)", Conn);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@log";
            param.Value = log;
            Cmd.Parameters.Add(param);
            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@value";
            param2.Value = "od "+log+" : "+value;
            Cmd.Parameters.Add(param2);
            SqlParameter param3 = new SqlParameter();
            param3.ParameterName = "@rec";
            param3.Value = rec;
            Cmd.Parameters.Add(param3);
            Cmd.ExecuteNonQuery();
            return true;


        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        
    }
}
