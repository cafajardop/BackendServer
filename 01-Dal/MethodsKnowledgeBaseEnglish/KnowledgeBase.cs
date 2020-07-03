using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using _01_Dal.Entities;
using Dapper;

namespace _01_Dal.MethodsKnowledgeBaseEnglish
{
    public class KnowledgeBase
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConCC"].ToString());

        public dynamic GetAllComparatives()
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var resp = conn.Query<Comparatives>("SP_Consultar_Comparativos", new { }, commandType: CommandType.StoredProcedure);

                    dynamic respTotal = new { Registros = resp, Total = resp.Count() };

                    return respTotal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Comparatives> GetComparativeById(string idComparative)
        {
            try
            {
                using (IDbConnection db = conn)
                {

                    var p = new DynamicParameters();

                    p.Add("@idAdjetive", idComparative, dbType: DbType.String, direction: ParameterDirection.Input);

                    var resp = db.Query<Comparatives>("SP_Consultar_Comparatives_Id ", p, commandType: CommandType.StoredProcedure);
                    return resp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetFindComparativeName(string NombreCategory)
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();
                    p.Add("@busqueda", NombreCategory, dbType: DbType.String, direction: ParameterDirection.Input);

                    var resp = db.Query<Comparatives>("SP_Find_Comparative", p, commandType: CommandType.StoredProcedure);

                    dynamic respTotal = new { Registros = resp, Total = resp.Count() };
                    return respTotal;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetFindComparativeID(string ID)
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();
                    p.Add("@busquedaID", ID, dbType: DbType.String, direction: ParameterDirection.Input);

                    var resp = db.Query<Question>("SP_Find_Comparative_ID", p, commandType: CommandType.StoredProcedure);

                    dynamic respTotal = new { Registros = resp, Total = resp.Count() };

                    return respTotal;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateComparative(Question comparative)
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();

                    p.Add("@idAdjetive", comparative.idAdjetive, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Adjetive", comparative.Adjetive, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Comparativo", comparative.Comparativo, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Superlative", comparative.Superlative, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Spanish", comparative.Spanish, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Question", comparative.Questions, dbType: DbType.String, direction: ParameterDirection.Input);

                    conn.Query<Categoria>("spUpdateComparative", p, commandType: CommandType.StoredProcedure);

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertComparative(Question comparative)
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();
                    
                    p.Add("@Adjetive", comparative.Adjetive, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Comparativo", comparative.Comparativo, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Superlative", comparative.Superlative, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Spanish", comparative.Spanish, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Question", comparative.Questions, dbType: DbType.String, direction: ParameterDirection.Input);

                    var result = conn.Query<Categoria>("spInsertComparative", p, commandType: CommandType.StoredProcedure).ToList();

                    return result.Count == 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}