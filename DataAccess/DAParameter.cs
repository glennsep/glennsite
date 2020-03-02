/************************************************************************************************************
 * Program Id:      DAParameter                                                                             *
 * Author:          Glenn Seplowitz                                                                         *
 * Purpose:         Used to store information about parameters passed to sql objects.                       *
 * Written:         September 29, 2013                                                                      *
 *                                                                                                          *
 * Modifications:                                                                                           *
 ***********************************************************************************************************/  

// import namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

// define class namespace
namespace DataAccess
{
    // define class
    public class DAParameter 
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DAParameter() { }

        /// <summary>
        /// Constructor that takes a sql parameter
        /// </summary>
        /// <param name="_pa">the sql parameter</param>
        public DAParameter(SqlParameter _pa)
        {
            PAParam = new SqlParameter();
            PAParam = _pa;
        }

        /// <summary>
        /// Contstructor that defines a parameter with a value
        /// </summary>
        /// <param name="_name">name of the parameter</param>
        /// <param name="_direction">direction of the parameter</param>
        /// <param name="_type">the sql type</param>
        /// <param name="_value">the value</param>
        public DAParameter(string _name, ParameterDirection _direction, SqlDbType _type, object _value)
        {
            // define the parameter
            PAParam = new SqlParameter();

            // store the information
            PAParam.ParameterName = _name;
            PAParam.Direction = _direction;
            PAParam.SqlDbType = _type;
            PAParam.Value = _value;
        }

        /// <summary>
        /// Contstructor that defines a parameter with a value and size
        /// </summary>
        /// <param name="_name">name of the parameter</param>
        /// <param name="_direction">direction of the parameter</param>
        /// <param name="_type">the sql type of the parameter</param>
        /// <param name="_size">the size of the parameter</param>
        /// <param name="_value">the value</param>
        public DAParameter(string _name, ParameterDirection _direction, SqlDbType _type, int _size, object _value)
        {
            // define the parameter
            PAParam = new SqlParameter();

            // store the information
            PAParam.ParameterName = _name;
            PAParam.Direction = _direction;
            PAParam.SqlDbType = _type;
            PAParam.Size = _size;
            PAParam.Value = _value;
        }

        /// <summary>
        /// Constructor for a parameter used for output or return value with size
        /// </summary>
        /// <param name="_name">the name of the parameter</param>
        /// <param name="_direction">the direction of the parameter</param>
        /// <param name="_type">the sql type of the parameter</param>
        /// <param name="_size">the size</param>
        public DAParameter(string _name, ParameterDirection _direction, SqlDbType _type, int _size)
        {
            // define the parameter
            PAParam = new SqlParameter();

            // store the information
            PAParam.ParameterName = _name;
            PAParam.Direction = _direction;
            PAParam.SqlDbType = _type;
            PAParam.Size = _size;
        }

        /// <summary>
        /// Constructor for a parameter used for output or return value
        /// </summary>
        /// <param name="_name">the name of the parameter</param>
        /// <param name="_direction">the direction of the parameter</param>
        /// <param name="_type">the sql type of the parameter</param>
        public DAParameter(string _name, ParameterDirection _direction, SqlDbType _type)
        {
            // define the parameter
            PAParam = new SqlParameter();

            // store the information
            PAParam.ParameterName = _name;
            PAParam.Direction = _direction;
            PAParam.SqlDbType = _type;
        }

        #endregion

        #region Methods
        
        #endregion

        #region Properties

        // the direction of the parameter
        public ParameterDirection PADirection { get; set; }

        // the parameter name
        public string PAName { get; set; }

        // the parameter type
        public SqlDbType PASqlType { get; set; }

        // the parameter size 
        public int PASize { get; set; }

        // the parameter value
        public object PAValue { get; set; }

        // the sql parameter
        public SqlParameter PAParam { get; set; }

        #endregion
    }
}
