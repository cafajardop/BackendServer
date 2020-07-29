using _01_Dal.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _01_Dal.Methods
{
    public class Authentication
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConCC"].ToString());

        /// <summary>
        /// Consulta el tipo de documento
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string, string> loginValidate(string username, string password)
        {
            try
            {
                string ePass = GetSHA256(password);
                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();

                    p.Add("@username", username, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@password", username, dbType: DbType.String, direction: ParameterDirection.Input);

                    var sqlUserName = "SELECT id,userName,email,password FROM LoginUser where email = @username";
                    var sqlPassword = "SELECT password FROM LoginUser where email = @username";
                    var Resuser = db.Query(sqlUserName, p);
                    var Respass = db.Query(sqlPassword, p);

                    dynamic user = Resuser.Select(x => new {
                        x.id,
                        x.email,
                        x.password,
                        x.userName
                    }).FirstOrDefault();

                    dynamic pass = new { password = Respass.Select(x => x.password).FirstOrDefault() };
                    if (user != null)
                    {
                        if (user.email == username && pass.password == ePass)
                            return Tuple.Create(true, user.userName, user.id);
                        else
                            return Tuple.Create(false, "", "");
                    }
                    else
                    {
                        return Tuple.Create(false, "", "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RegisterUser(LoginUser loginUser)
        {
            try
            {
                loginUser.id = Guid.NewGuid().ToString();
                loginUser.Password = GetSHA256(loginUser.Password);

                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();

                    p.Add("@id", loginUser.id, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@userName", loginUser.userName, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@email", loginUser.email.Trim(), dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@Password", loginUser.Password, dbType: DbType.String, direction: ParameterDirection.Input);

                    var sqlUserName = "SELECT email FROM LoginUser where email = @email";
                    var Resuser = db.Query(sqlUserName, p);

                    dynamic user = new { email = Resuser.Select(x => x.email).FirstOrDefault() };
                    if (user.email != null)
                    {
                        if (user.email == loginUser.email)
                            return "0";
                    }

                    db.Query("spCrearUser ", p, commandType: CommandType.StoredProcedure);

                    return loginUser.id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdatePassword(Pass pass)
        {
            try
            {
                pass.passwordOld = GetSHA256(pass.passwordOld);
                pass.password = GetSHA256(pass.password);

                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();

                    p.Add("@id", pass.id, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@passwordOld", pass.passwordOld, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@password", pass.password, dbType: DbType.String, direction: ParameterDirection.Input);

                    var sqlPassOld = "SELECT password FROM LoginUser where id = @id";
                    var Resuser = db.Query(sqlPassOld, p);
                    dynamic passChange = Resuser.Select(x => new {
                        x.password
                    }).FirstOrDefault();

                    if (passChange != null)
                    {
                        if (passChange.password == pass.passwordOld)
                        {
                            var updatepass = "UPDATE C SET C.password = @password FROM LoginUser C WHERE id = @id";
                            db.Query(updatepass, p);

                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateUser(LoginUser updateUser)
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();

                    p.Add("@id", updateUser.id, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@userName", updateUser.userName, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@email", updateUser.email, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@firstName", updateUser.firstName, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@lastName", updateUser.lastName, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@phoneNumber", updateUser.phoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@position", updateUser.position, dbType: DbType.String, direction: ParameterDirection.Input);

                    var resp = db.Query<LoginUser>("spUpdateUserCNR", p , commandType: CommandType.StoredProcedure).ToList();
                    
                    return resp.Count.Equals(0) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetLoginUser (string id)
        {
            try
            {
                using (IDbConnection db = conn)
                {
                    var p = new DynamicParameters();
                    p.Add("@id", id, dbType: DbType.String, direction: ParameterDirection.Input);
                    var sqlUserName = "SELECT id,userName,email,password,firstName,lastName,phoneNumber,position,UserId FROM LoginUser where id = @id";
                    var Resuser = db.Query(sqlUserName, p);

                    dynamic user = Resuser.Select(x => new {
                        x.email,
                        x.userName,
                        x.firstName,
                        x.lastName,
                        x.phoneNumber,
                        x.position,
                        x.UserId
                    }).FirstOrDefault();

                   return user;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Encriptacion de passoword
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);

            return sb.ToString();
        }


    }
}
