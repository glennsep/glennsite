/************************************************************************************************************
 * Program Id:      Place                                                                                   *
 * Author:          Glenn Seplowitz                                                                         *
 * Purpose:         Information about the places .                                                          *
 * Written:         October 6, 2013                                                                         *
 *                                                                                                          *
 * Modifications:                                                                                           *
 ***********************************************************************************************************/  

// import namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

// define class namespace
namespace ElectronicDetective
{
    // declare class
    public class Place
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Place() { }

        /// <summary>
        /// Constructor that will load the place information
        /// </summary>
        /// <param name="placesId">the place identifier</param>
        public Place(char placesId)
        {
            LoadPlaceInfo(placesId);
        }
        
        #endregion

        #region Members

        public void LoadPlaceInfo(char placesId)
        {
            try
            {
                // declare variables
                DataTable placesInfo;

                // setup the parameter
                List<DAParameter> placesParam = new List<DAParameter>
                {
                    new DAParameter("@PlacesId", ParameterDirection.Input,SqlDbType.Char,1,(object)placesId)
                };

                // retrieve the suspect information
                placesInfo = DataRetrieval.QueryDataResults("ED.GetPlaceInfo", DataRetrieval.ConnectionSql, placesParam);

                // load the suspect information
                Id = Char.Parse(placesInfo.Rows[0]["PlacesId"].ToString());
                PlaceDescription = placesInfo.Rows[0]["PlaceDesc"].ToString();
                LocationId = 0;
                LocationName = "";
                AreaId = 0;
                AreaName = "";
                WeaponId = 0;
                WeaponName = "None";
                OddMaleSuspect = null;
                EvenMaleSuspect = null;
                OddFemaleSuspect = null;
                EvenFemaleSuspect = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Identifies the place
        /// </summary>
        public char Id { get; set; }

        /// <summary>
        /// The name of the place
        /// </summary>
        public string PlaceDescription { get; set; }

        /// <summary>
        /// The identifer of the location (east side or west side)
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// The name of the location (east side or west side)
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// The identifer of the area (uptown, downtown, midtown)
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// The name of the area (uptown, downtown, midtown)
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// Identifies the weapon found at the place
        /// </summary>
        public int WeaponId { get; set; }

        /// <summary>
        /// The name of the weapon
        /// </summary>
        public string WeaponName { get; set; }

        /// <summary>
        /// The odd numbered male suspect at this place
        /// </summary>
        public Suspect OddMaleSuspect { get; set; }

        /// <summary>
        /// The even numbered male suspect at this place
        /// </summary>
        public Suspect EvenMaleSuspect { get; set; }

        /// <summary>
        /// The odd female suspect at this place
        /// </summary>
        public Suspect OddFemaleSuspect { get; set; }

        /// <summary>
        /// The even female suspect at this place
        /// </summary>
        public Suspect EvenFemaleSuspect { get; set; }

        #endregion

    }
}
