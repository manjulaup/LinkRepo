using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DALConnectionManager
/// </summary>
public class DALConnManager
{
	public DALConnManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //For MRP
    public static SqlConnection OpenMRP()
    {
        SqlConnection ConnectionManager = new SqlConnection();
        try
        {
            ConnectionManager.ConnectionString = @"Data Source=.;Initial Catalog=MRP;Persist Security Info=True;User ID=sa;Password=123";
            ConnectionManager.Open();
        }
        catch (Exception ex)
        {

        }
        return ConnectionManager;
    }

    //For Finance
    public static SqlConnection OpenFinance()
    {
        SqlConnection ConnectionManager = new SqlConnection();
        try
        {
            ConnectionManager.ConnectionString = @"Data Source=.;Initial Catalog=LinkFinace;Persist Security Info=True;User ID=sa;Password=123";
            ConnectionManager.Open();
        }
        catch (Exception ex)
        {

        }
        return ConnectionManager;
    }


    public static SqlConnection Open()
    {
        SqlConnection ConnectionManager = new SqlConnection();
        try
        {
            ConnectionManager.ConnectionString = @"Data Source=localhost;Initial Catalog=testdb;Persist Security Info=True;User ID=sa;Password=123";
            ConnectionManager.Open();
        }
        catch (Exception ex)
        {

        }
        return ConnectionManager;
    }

    public static void Close(SqlConnection con)
    {
        try
        {
            con.Close();
        }
        catch (SqlException ex)
        {

        }
    }


}