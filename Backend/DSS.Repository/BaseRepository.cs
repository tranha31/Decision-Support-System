using Dapper;
using DSS.Core.entity;
using DSS.Core.interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DSS.Core.enums.Enum;

namespace DSS.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        /// <summary>
        /// Thông tin kết nối
        /// create by : TQHa (27/7/2021)
        /// </summary>
        IConfiguration _configuration;
        string _connectionString;

        /// <summary>
        /// Biến khởi tạo kết nối
        /// create by: TQHa (27/7/2021)
        /// </summary>
        IDbConnection _dbConnection;
        string _tableName;

        #region Contructor
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DSSConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(TEntity).Name;
        }
        #endregion

        public ServiceResult GetAll()
        {
            var result = new ServiceResult();
            var sql = "Select * from " + _tableName;
            try
            {
                var entity = _dbConnection.Query<TEntity>(sql, commandType: CommandType.Text);
                result.Data = entity;
                result.Code = (int)DSSEnum.Success;
            }
            catch(Exception ex)
            {
                result.Code = (int)DSSEnum.ErrorSever;
                result.DevMsg = ex.Message;
                result.UserMsg = Properties.Resources.ErrorSystem;
            }

            return result;
        }

        public int GetAmount()
        {
            var result = 0;
            var sql = "Select sum(Amount) from " + _tableName;
            try
            {
                var entity = _dbConnection.QueryFirstOrDefault<int>(sql, commandType: CommandType.Text);
                result = entity;
                
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        public int GetAmountDoctor(List<Guid> provinceExcept)
        {
            var result = 0;
            var sql = $"Select sum(NumberDoctorDispatch) from Province Where ProvinceId IN (";
            DynamicParameters parameters = new DynamicParameters();
            for(var i=0; i<provinceExcept.Count(); i++)
            {
                sql += $"@ID{i}, ";
                parameters.Add($"@ID{i}", provinceExcept[i]);
            }
            sql = sql.Remove(sql.Length - 2, 2);
            sql += ")";

            try
            {
                var entity = _dbConnection.QueryFirstOrDefault<int>(sql, parameters, commandType: CommandType.Text);
                result = entity;
            }
            catch(Exception ex)
            {
                result = -1;
            }

            return result;
        }

        public ServiceResult GetProvince()
        {
            var result = new ServiceResult();
            var sql = $"Select * from Province" ;
            try
            {
                var entity = _dbConnection.Query<Province>(sql, commandType: CommandType.Text);
                result.Data = entity;
                result.Code = (int)DSSEnum.Success;
            }
            catch (Exception ex) 
            {
                result.Code = (int)DSSEnum.ErrorSever;
                result.DevMsg = ex.Message;
                result.UserMsg = Properties.Resources.ErrorSystem;
            }

            return result;
        }

        public ServiceResult GetProvinceForDoctor(List<Guid> provinceExcept)
        {
            var result = new ServiceResult();
            var sql = $"Select * from Province Where ProvinceId NOT IN (";
            DynamicParameters parameters = new DynamicParameters();
            for (var i = 0; i < provinceExcept.Count(); i++)
            {
                sql += $"@ID{i}, ";
                parameters.Add($"@ID{i}", provinceExcept[i]);
            }
            sql = sql.Remove(sql.Length - 2, 2);
            sql += ")";
            try
            {
                var entity = _dbConnection.Query<Province>(sql, parameters, commandType: CommandType.Text);
                result.Data = entity;
                result.Code = (int)DSSEnum.Success;
            }
            catch (Exception ex)
            {
                result.Code = (int)DSSEnum.ErrorSever;
                result.DevMsg = ex.Message;
                result.UserMsg = Properties.Resources.ErrorSystem;
            }

            return result;
        }

        public ServiceResult Update(TEntity entity)
        {
            var tableName = entity.GetType().Name;
            DynamicParameters parameters = new DynamicParameters();
            parameters.AddDynamicParams(entity);

            var result = new ServiceResult();
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    _dbConnection.Execute($"Proc_Update_{tableName}", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                    result.Code = (int)DSSEnum.Success;
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    result.Code = (int)DSSEnum.ErrorSever;
                    result.DevMsg = ex.Message;
                    result.UserMsg = Properties.Resources.ErrorSystem;
                    transaction.Rollback();


                }
                finally
                {
                    transaction.Dispose();
                    _dbConnection.Close();
                }
            }
            return result;
        }
    }
}
