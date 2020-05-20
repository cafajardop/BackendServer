﻿using System;
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
    public class MethodsGenerics
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConCC"].ToString());

        /// <summary>
        /// Consulta las categorias
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Categoria> ConsultCategory()
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var resp = conn.Query<Categoria>("SP_Consultar_Categoria", new { }, commandType: CommandType.StoredProcedure);
                    return resp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Actualiza las categorias
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Categoria> GetCategoryId(string idCategory)
        {
            try
            {
                int idCategoryParam = Convert.ToInt32(idCategory);

                using (IDbConnection db = conn)
                {

                    var p = new DynamicParameters();

                    p.Add("@idCategoria", idCategory, dbType: DbType.Int16, direction: ParameterDirection.Input);

                    var resp = db.Query<Categoria>("SP_Consultar_Categoria_Id ", p, commandType: CommandType.StoredProcedure);
                    return resp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Actualiza las categorias
        /// </summary>
        /// <returns></returns>
        public bool UpdateCategory(Categoria categoria)
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();

                    p.Add("@idCategoria", categoria.idCategoria, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@NombreCategoria", categoria.NombreCategoria, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Activa", categoria.Activa, dbType: DbType.Boolean, direction: ParameterDirection.Input);
                    
                    conn.Query<Categoria>("spActualizarCategoria", p, commandType: CommandType.StoredProcedure);

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public dynamic InsertarCategoria(Categoria categoria)
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();

                    p.Add("@id", dbType: DbType.Int16, direction: ParameterDirection.ReturnValue);
                    p.Add("@nombre_categoria", categoria.NombreCategoria, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Activa", categoria.Activa, dbType: DbType.Boolean, direction: ParameterDirection.Input);

                    var resp = db.Query("insertar_categoria ", p, commandType: CommandType.StoredProcedure);

                    dynamic id = new { ID = resp.Select(x => x.ID).FirstOrDefault() };

                    return id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCategoria(int id)
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();

                    p.Add("@idCategoria", id, dbType: DbType.Int16, direction: ParameterDirection.Input);

                    db.Query("spEliminarCategoria ", p, commandType: CommandType.StoredProcedure);

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
