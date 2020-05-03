using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using _01_Dal.Entities;
using Dapper;

namespace _01_Dal.Methods
{
    public class Methods
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConCC"].ToString());
        /// <summary>
        /// Consulta el tipo de documento
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TipoDocumento> ConsultType()
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var resp = conn.Query<TipoDocumento>("spTipDocumento", new { }, commandType: CommandType.StoredProcedure);
                    return resp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Inserta un usuario en la tabla Users
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public dynamic InsertUsers(Users users)
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();

                    p.Add("@id", dbType: DbType.String, direction: ParameterDirection.ReturnValue);
                    p.Add("@TipDoc", users.TipDocumento, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@Num", users.NumDocumento, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Nombres", users.Nombres, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Apellidos", users.Apellidos, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Direccion", users.Direccion, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Telefono", users.Telefono, dbType: DbType.String, direction: ParameterDirection.Input);

                    var resp = db.Query("spInsertarUsuario ", p, commandType: CommandType.StoredProcedure);

                    dynamic id = new { ID = resp.Select(x => x.ID).FirstOrDefault() };
                    
                    return id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Consultar todos los usuarios
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Users> ConsultPerson()
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var resp = conn.Query<Users>("spConsultaUsuarios", new { }, commandType: CommandType.StoredProcedure);
                    return resp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}