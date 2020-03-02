/************************************************************************************************************
 * Program Id:      Suspect                                                                                 *
 * Author:          Glenn Seplowitz                                                                         *
 * Purpose:         Information about the suspect.                                                          *
 * Written:         October 5, 2013                                                                         *
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
    public class Suspect
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Suspect() { }

        /// <summary>
        /// Constructor that will load the suspect information
        /// </summary>
        /// <param name="suspectId">the identifier for the suspect</param>
        public Suspect(int suspectId)
        {
            // load the suspect information
            if (suspectId > 0)
                LoadSuspectInfo(suspectId);
            else
                LoadPlaceHolderSuspect();
        }
         
        #endregion

        #region Methods

        /// <summary>
        /// This serves as a place holder so we don't get null exceptions
        /// </summary>
        public void LoadPlaceHolderSuspect()
        {
            try
            {
                // load the suspect information
                Id = 0;
                FirstName = "Place";
                LastName = "Holder";
                Occupation = "N/A";
                Relationship = "N/A";
                MarriedId = 0;
                Sex = 'U';
                SexName = "Unknown";
                IsVictim = false;
                IsMurderer = false;
                VictimPlace = 'X';
                MurdererPlace = 'X';
                MurderWeapon = 0;
                MurderWeaponName = "None";
                AlibiPlace = null;
                AlibiShowLocation = false;
                AlibiShowArea = false;
                AlibiShowPlace = false;
                AlibiShowOddMaleSuspect = false;
                AlibiShowEvenMaleSuspect = false;
                AlibiShowOddFemaleSuspect = false;
                AlibiShowEvenFemaleSuspect = false;
                SuspectQuestions = new List<Question>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Loads the information about the suspect such as their name, sex, occupation, etc.
        /// </summary>
        public void LoadSuspectInfo(int suspectId)
        {
            try
            {
                // declare variables
                DataTable suspectInfo;

                // setup the parameter
                List<DAParameter> suspectParam = new List<DAParameter>
                {
                    new DAParameter("@SuspectId",ParameterDirection.Input,SqlDbType.Int,(object)suspectId)
                };

                // retrieve the suspect information
                suspectInfo = DataRetrieval.QueryDataResults("ED.GetSuspectInfo", DataRetrieval.ConnectionSql, suspectParam);

                // load the suspect information
                Id = Int32.Parse(suspectInfo.Rows[0]["SuspectId"].ToString());
                FirstName = suspectInfo.Rows[0]["SuspectFName"].ToString();
                LastName = suspectInfo.Rows[0]["SuspectLName"].ToString();
                Occupation = suspectInfo.Rows[0]["SuspectOccupation"].ToString();
                Relationship = suspectInfo.Rows[0]["SuspectRelationship"].ToString();
                MarriedId = Int32.Parse(suspectInfo.Rows[0]["SuspectMarriedId"].ToString());
                Sex = Char.Parse(suspectInfo.Rows[0]["SuspectSex"].ToString());
                SexName = Sex == 'M' ? "Male" : "Female";
                IsVictim = false;
                IsMurderer = false;
                VictimPlace = 'X';
                MurdererPlace = 'X';
                MurderWeapon = 0;
                MurderWeaponName = "None";
                AlibiPlace = null;
                AlibiShowLocation = false;
                AlibiShowArea = false;
                AlibiShowPlace = false;
                AlibiShowOddMaleSuspect = false;
                AlibiShowEvenMaleSuspect = false;
                AlibiShowOddFemaleSuspect = false;
                AlibiShowEvenFemaleSuspect = false;

                // load the questions for the suspect
                PopulateSuspectQuestions(suspectId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Populates the questions for this suspect
        /// </summary>
        /// <param name="suspectId"></param>
        public void PopulateSuspectQuestions(int suspectId)
        {
            // declare variables
            DataTable questions;                        // data table to store the questions

            try
            {
                // setup the parameter for the question
                List<DAParameter> questionParam = new List<DAParameter>
                {
                    new DAParameter("@SuspectId",ParameterDirection.Input,SqlDbType.Int,(object)suspectId)
                };

                // retrieve questions for the suspect
                questions = DataRetrieval.QueryDataResults("[ED].[GetSuspectQuestions]", DataRetrieval.ConnectionSql, questionParam);

                // loop through the questions and assign them to the suspect
                if (questions.Rows.Count > 0)
                {
                    // intialize generic list for questions
                    SuspectQuestions = new List<Question>();

                    // load questions for the suspect
                    for (int rowCount = 0; rowCount < questions.Rows.Count; rowCount++)
                    {
                        SuspectQuestions.Add(new Question(Int32.Parse(questions.Rows[rowCount]["SuspectId"].ToString()),
                            Int32.Parse(questions.Rows[rowCount]["QuestionId"].ToString()), questions.Rows[rowCount]["QuestionDesc"].ToString()));
                    }

                    // now get the answers for the questions
                    foreach (Question q in SuspectQuestions)
                    {
                        q.GetAnswers();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Suspects id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The suspects first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The suspects last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The suspects occupation
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// The suspects relationship status
        /// </summary>
        public string Relationship { get; set; }

        /// <summary>
        /// if the suspect is married then to whom
        /// </summary>
        public int MarriedId { get; set; }

        /// <summary>
        /// the sex of the suspect
        /// </summary>
        public char Sex { get; set; }

        /// <summary>
        /// Spells out the sex
        /// </summary>
        public string SexName { get; set; }

        /// <summary>
        /// Indicates if this suspect is the murder victim
        /// </summary>
        public bool IsVictim { get; set; }

        /// <summary>
        /// If suspect is the victim then indicate what place the victim's body was found
        /// </summary>
        public char VictimPlace { get; set; }

        /// <summary>
        /// If suspect is the murderer then indicate where they fled to
        /// </summary>
        public char MurdererPlace { get; set; }

        /// <summary>
        /// Indicates if this suspect is the murderer
        /// </summary>
        public bool IsMurderer { get; set; }

        /// <summary>
        /// Indicates the weapon the murderer used
        /// </summary>
        public int MurderWeapon { get; set; }

        /// <summary>
        /// The name of the murder weapon
        /// </summary>
        public string MurderWeaponName { get; set; }

        /// <summary>
        /// The place the suspect is located at.  Used for alibi purposes.
        /// </summary>
        public Place AlibiPlace { get; set; }

        /// <summary>
        /// Show the location (east, west) alibi
        /// </summary>
        public bool AlibiShowLocation { get; set; }

        /// <summary>
        /// Show the area (uptown, midtown, downtown) alibi
        /// </summary>
        public bool AlibiShowArea { get; set; }

        /// <summary>
        /// show the alibi place
        /// </summary>
        public bool AlibiShowPlace { get; set; }

        /// <summary>
        /// Show the odd male suspect
        /// </summary>
        public bool AlibiShowOddMaleSuspect { get; set; }

        /// <summary>
        /// Show the even male suspect
        /// </summary>
        public bool AlibiShowEvenMaleSuspect { get; set; }

        /// <summary>
        /// Show the odd female suspect
        /// </summary>
        public bool AlibiShowOddFemaleSuspect { get; set; }

        /// <summary>
        /// Show the even female suspect
        /// </summary>
        public bool AlibiShowEvenFemaleSuspect { get; set; }

        /// <summary>
        /// The questions that this suspect can be asked
        /// </summary>
        public List<Question> SuspectQuestions { get; set; }

        #endregion
    }
}
