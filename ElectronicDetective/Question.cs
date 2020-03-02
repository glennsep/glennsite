/************************************************************************************************************
 * Program Id:      Question                                                                                *
 * Author:          Glenn Seplowitz                                                                         *
 * Purpose:         Questions for each suspect.                                                             *
 * Written:         October 19, 2013                                                                        *
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
    public class Question
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        private Question() { }

        /// <summary>
        /// Constructor that populates properties
        /// </summary>
        /// <param name="_suspectId">the suspect id</param>
        /// <param name="_questionId">the question id</param>
        /// <param name="_questionDesc">the question desc</param>
        public Question(int _suspectId, int _questionId, string _questionDesc)
        {
            SuspectId = _suspectId;
            QuestionId = _questionId;
            QuestionDesc = _questionDesc;
            Answers = new List<Answer>();
        }

        #endregion

        #region Members

        public void GetAnswers()
        {
            // declare variables
            DataTable answers;                          // data table to store answers to the questions

            try
            {
                // setup the parameter for the answer
                List<DAParameter> answerParam = new List<DAParameter>
                {
                    new DAParameter("@QuestionId",ParameterDirection.Input,SqlDbType.Int,(object)QuestionId)
                };

                // retrieve answers for the question
                answers = DataRetrieval.QueryDataResults("[ED].[GetQuestionAnswers]", DataRetrieval.ConnectionSql, answerParam);

                if (answers.Rows.Count > 0)
                {
                    // store the answers
                    for (int rowCount = 0; rowCount < answers.Rows.Count; rowCount++)
                    {
                        Answers.Add(new Answer(QuestionId, Int32.Parse(answers.Rows[rowCount]["AnswerId"].ToString()),
                            answers.Rows[rowCount]["AnswerDesc"].ToString()));
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
        /// The suspect id to relate question to suspect
        /// </summary>
        public int SuspectId { get; set; }

        /// <summary>
        /// Identifies the question
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// The question
        /// </summary>
        public string QuestionDesc { get; set; }

        public List<Answer> Answers { get; set; }

        #endregion

        // define answer class
        public class Answer
        {
            #region Constructors

            /// <summary>
            /// Default Constructor
            /// </summary>
            private Answer() { }

            /// <summary>
            /// Constructor that sets up property values
            /// </summary>
            /// <param name="_questionId">identifies the question</param>
            /// <param name="_answerId">identifies the answer</param>
            /// <param name="_answerDesc">the answer description</param>
            public Answer(int _questionId, int _answerId, string _answerDesc)
            {
                QuestionId = _questionId;
                AnswerId = _answerId;
                AnswerDesc = _answerDesc;
            }

            #endregion

            #region Properties

            /// <summary>
            /// The question id 
            /// </summary>
            public int QuestionId { get; set; }

            /// <summary>
            /// The answer id associated with the question id
            /// </summary>
            public int AnswerId { get; set; }

            /// <summary>
            /// The answer description
            /// </summary>
            public string AnswerDesc { get; set; }

            #endregion
        }
    }
}
