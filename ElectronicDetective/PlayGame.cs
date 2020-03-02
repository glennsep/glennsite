/************************************************************************************************************
 * Program Id:      PlayGame                                                                                *
 * Author:          Glenn Seplowitz                                                                         *
 * Purpose:         Routines that are used to play the game.                                                *
 * Written:         September 21, 2016                                                                      *
 *                                                                                                          *
 * Modifications:                                                                                           *
 ***********************************************************************************************************/

// import namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// define class namespace
namespace ElectronicDetective
{
    // declare class
    public class PlayGame
    {
        #region Attributes
        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public PlayGame() { }

        #endregion

        #region Methods

        /// <summary>
        /// This will create a new murder scenario for the game
        /// </summary>
        /// <returns>the Murder object that contains all information needed to play the game</returns>
        public Murder CreateNewMurderScenario()
        {
            // declare variables
            Murder crimeScene = new Murder();

            // create the crime
            crimeScene.CreateCrimeScene();



            // return the murder object
            return crimeScene;
        }

        /// <summary>
        /// This will store the murder information to the case fact sheet tables.  These tables are used to populate the case fact sheet on the web page during game play.
        /// </summary>
        /// <param name="crimeScene">a Murder object that contains all information about the murder</param>
        protected void StoreCaseFactSheet(Murder crimeScene)
        {
            // first store the murder facts

        }

        /// <summary>
        /// Stores the murder facts for the crime scene
        /// </summary>
        /// <param name="crimeScene">the murder object that contains information for game play</param>
        protected void StoreCFSMurderFacts(Murder crimeScene)
        {

        }


        #endregion
    }
}
