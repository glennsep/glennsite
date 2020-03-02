/************************************************************************************************************
 * Program Id:      Murder                                                                                  *
 * Author:          Glenn Seplowitz                                                                         *
 * Purpose:         Creates the crime scene.  Creates the victim, the murderer and where suspects fled.     *
 * Written:         September 15, 2013                                                                      *
 *                                                                                                          *
 * Modifications:                                                                                           *
 ***********************************************************************************************************/                                                                                                              

// import namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data;
using System.Data.SqlClient;
using Common;
using System.Configuration;

// define class namespace
namespace ElectronicDetective
{
    // declare class
    public class Murder
    {
        #region Attributes

        // define variables for this class
        private Random randomNumbers = new Random();    // random number generator, create one to help increase randomization
        private char place38Id;                         // stores the place where the 38 was found
        private char place45Id;                         // stores the place where the 45 was found
        private char[] places;                          // stores the place identifiers in an array
        private int numberPlaces;                       // stores the number of places
        private int numberSuspects;                     // stores the number of suspects
      
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Murder() { }
                
        #endregion

        #region Methods

        /// <summary>
        /// This will generate the crime scene
        /// </summary>
        public void CreateCrimeScene()
        {
            try
            {
                // initialize variables
                Suspects = new List<Suspect>();
                Places = new List<Place>();

                // set database connection
                DataRetrieval.SetSqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

                // build an array to store place id
                BuildPlacesIdArray();

                // get the number of suspects
                GetNumberSuspects();

                // populate the basic suspect information (name, sex, occupation, etc)
                PopulateSuspectInfo();

                // determine who the victim is
                DetermineVictim();

                // populate basic place information (name of place, etc)
                PopulatePlaceInfo();

                // determine who the murderer is
                DetermineMurderer();

                // put suspects in places
                PutSuspectsInTheirPlace();

                // build the suspect alibi
                GetSuspectAlibi();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This builds the array to store the place identifiers which will be used throughout the process of building the crime scene
        /// </summary>
        public void BuildPlacesIdArray()
        {
            // define variables
            DataTable placesData;

            try
            {
                // get the place identifiers
                placesData = DataRetrieval.QueryDataResults("ED.GetPlaceId", DataRetrieval.ConnectionSql, true);

                // if records are returned then build the array
                if (placesData.Rows.Count > 0)
                {
                    // define the # of elements in the array
                    places = new char[placesData.Rows.Count];

                    // store the number of places
                    numberPlaces = placesData.Rows.Count;

                    // put the place identifiers in the array
                    for (int count = 0; count <= placesData.Rows.Count - 1; count++)
                        places[count] = Char.Parse(placesData.Rows[count]["PlacesId"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the total number of suspects
        /// </summary>
        public void GetNumberSuspects()
        {
            // declare variables
            DataTable suspectData;

            try
            {
                // get the number of suspects
                suspectData = DataRetrieval.QueryDataResults("ED.GetNumberSuspects", DataRetrieval.ConnectionSql, true);

                // store the number of suspects
                numberSuspects = Int32.Parse(suspectData.Rows[0]["NumberSuspects"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load the basic information for each suspect (e.g. Name, sex, occupation, etc)
        /// </summary>
        public void PopulateSuspectInfo()
        {
            try
            {
                // for each suspect create a separate suspect object and store it to the suspects list
                for (int count = 1; count <= numberSuspects; count++)
                {
                    Suspects.Add(new Suspect(count));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This will determine who the victim is
        /// </summary>
        public void DetermineVictim()
        {
            // declare variables
            int num;

            try
            {
                // determine the suspect who is the victim and the place the victim was found
            num = randomNumbers.Next(1, numberSuspects+1);
                Victim = Suspects.Find(delegate(Suspect s) { return s.Id == num; });
                Victim.IsVictim = true;
                Victim.VictimPlace = places[randomNumbers.Next(0,numberPlaces)];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Loads information about the places
        /// </summary>
        public void PopulatePlaceInfo()
        {
            // declare variables
            int weaponType = 1;
            int[] randomPlaces;
            Place placeWeapon;
            DataTable pla;
            DataRow plaRow;

            try
            {
                // for each place load the basic information (place id, name)
                for (int count = 0; count < numberPlaces; count++)
                {
                    Places.Add(new Place(places[count]));
                }

                // we need to determine the location (east, west) and area (uptown, midtown, downtown) for each place
                // retrieve all combinations of locations and areas for the places
                pla = DataRetrieval.QueryDataResults("ED.GetPlacesLocationArea", DataRetrieval.ConnectionSql, true);

                // loop through each place and find its location and area
                foreach (Place p in Places)
                {
                    // find a random location and area and store it
                    plaRow = pla.Rows[randomNumbers.Next(pla.Rows.Count)];
                    p.LocationId = Int32.Parse(plaRow["LocationId"].ToString());
                    p.LocationName = plaRow["LocationDesc"].ToString();
                    p.AreaId = Int32.Parse(plaRow["AreaId"].ToString());
                    p.AreaName = plaRow["AreaDesc"].ToString();

                    // remove this row from the data table
                    pla.Rows.Remove(plaRow);
                }

                // now determine the location of the weapon for each place.  The weapon cannot be located where the victim's body is.
                // first generate an array with random numbers that represents each place
                randomPlaces = Enumerable.Range(0, numberPlaces).OrderBy(x => randomNumbers.Next()).ToArray();

                // now loop through the array and get the place of the .38 first then the .45  If the place is equal to where the
                // victim's body is we cannot use that
                for (int count = randomPlaces.GetLowerBound(0); count <= randomPlaces.GetUpperBound(0); count++)
                {
                    // get the location of the .38 or .45 
                    if (Victim.VictimPlace != places[randomPlaces[count]])
                    {
                        // first get a potential place to hide the weapon
                        placeWeapon = Places.Find(delegate(Place p) { return p.Id == places[randomPlaces[count]]; });
                        
                        // now store the weapon in the place provided that place doesn't already have a weapon
                        if (weaponType == 1 && placeWeapon.WeaponId == 0)
                        {
                            // .38
                            placeWeapon.WeaponId = 1;
                            placeWeapon.WeaponName = ".38";
                            place38Id = placeWeapon.Id;
                            weaponType++;
                        }
                        else if (weaponType == 2 && placeWeapon.WeaponId == 0)
                        {
                            // .45
                            placeWeapon.WeaponId = 2;
                            placeWeapon.WeaponName = ".45";
                            place45Id = placeWeapon.Id;
                            weaponType++;
                        }
                    }

                    // exit loop
                    if (weaponType > 2)
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Determine which suspect is the murderer
        /// </summary>
        public void DetermineMurderer()
        {
            // declare variables
            int[] randomNumber;

            try
            {
                // first generate an array with random numbers that represents each suspect
                randomNumber = Enumerable.Range(1, numberSuspects).OrderBy(x => randomNumbers.Next()).ToArray();

                // determine who the murderer is
                for (int count = randomNumber.GetLowerBound(0); count <= randomNumber.GetUpperBound(0); count++)
                {
                    if (randomNumber[count] != Victim.Id)
                    {
                        // set the murderer
                        Murderer = Suspects.Find(delegate(Suspect s) { return s.Id == randomNumber[count]; });
                        Murderer.IsMurderer = true;
                        break;
                    }
                }

                // get an array of numbers representing the places
                randomNumber = Enumerable.Range(0, numberPlaces).OrderBy(x => randomNumbers.Next()).ToArray();

                // determine the place the murderer went to
                for (int count = randomNumber.GetLowerBound(0); count <= randomNumber.GetUpperBound(0); count++)
                {
                    if (places[randomNumber[count]] != Victim.VictimPlace && places[randomNumber[count]] != place38Id && places[randomNumber[count]] != place45Id)
                    {
                        Murderer.MurdererPlace = places[randomNumber[count]];
                        Murderer.AlibiPlace = Places.Find(delegate(Place p) { return p.Id == Murderer.MurdererPlace; });
                        break;
                    }
                }

                // determine the murder weapon used
                Murderer.MurderWeapon = randomNumbers.Next(1, 3);
                Murderer.MurderWeaponName = Murderer.MurderWeapon == 1 ? ".38" : ".45";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This will place all the suspects in the places
        /// </summary>
        public void PutSuspectsInTheirPlace()
        {
            // declare variables
            int[] maleOddSuspects = Utilities.GetArrayofRandomNumbers(1, 10, true, randomNumbers);      // array of odd male suspect ids
            int[] maleEvenSuspects = Utilities.GetArrayofRandomNumbers(1, 10, false, randomNumbers);    // array of even male suspect ids
            int[] femaleOddSuspects = Utilities.GetArrayofRandomNumbers(11, 20, true, randomNumbers);   // array of odd female suspect ids
            int[] femaleEvenSuspects = Utilities.GetArrayofRandomNumbers(11, 20, false, randomNumbers); // array of even female suspect ids
            int[] randomNumber;                                                                         // an array of random numbers for generic purposes
            int maleOddSuspectsCount = 0;                                                               // the number of elements in male odd suspect array
            int maleEvenSuspectsCount = 0;                                                              // the number of elements in male even suspect array
            int femaleOddSuspectsCount = 0;                                                             // the number of elements in female odd suspect array
            int femaleEvenSuspectsCount = 0;                                                            // the number of elements in female even suspect array
            char placeThreeSuspects = ' ';                                                              // the place that contains only three suspects 
            Place victimPlace;                                                                          // references the victim place

            try
            {
                // remove the victim and murderer from the arrays
                maleOddSuspects = Utilities.RemoveNumberFromArray(maleOddSuspects, Victim.Id);
                maleOddSuspects = Utilities.RemoveNumberFromArray(maleOddSuspects, Murderer.Id);
                maleEvenSuspects = Utilities.RemoveNumberFromArray(maleEvenSuspects, Victim.Id);
                maleEvenSuspects = Utilities.RemoveNumberFromArray(maleEvenSuspects, Murderer.Id);
                femaleOddSuspects = Utilities.RemoveNumberFromArray(femaleOddSuspects, Victim.Id);
                femaleOddSuspects = Utilities.RemoveNumberFromArray(femaleOddSuspects, Murderer.Id);
                femaleEvenSuspects = Utilities.RemoveNumberFromArray(femaleEvenSuspects, Victim.Id);
                femaleEvenSuspects = Utilities.RemoveNumberFromArray(femaleEvenSuspects, Murderer.Id);

                // get an array of numbers representing the places
                randomNumber = Enumerable.Range(0, numberPlaces).OrderBy(x => randomNumbers.Next()).ToArray();

                // get the place with three suspects
                for (int count = randomNumber.GetLowerBound(0);count <= randomNumber.GetUpperBound(0);count++)           
                {
                    // get place with three suspects
                    placeThreeSuspects = places[randomNumber[count]];           

                    // check if place is equal to victim place
                    if (placeThreeSuspects == Victim.VictimPlace)
                        placeThreeSuspects = ' ';

                    // check if the murder and victim have same parity and the place with three suspects is same as place of murderer
                    if (Murderer.MurdererPlace == placeThreeSuspects)
                        if ((Utilities.IsOdd(Victim.Id) && Utilities.IsOdd(Murderer.Id)) || (!Utilities.IsOdd(Victim.Id) && !Utilities.IsOdd(Murderer.Id)))
                            placeThreeSuspects = ' ';

                    // exit from loop
                    if (placeThreeSuspects != ' ')
                        break;
                }

                // populate the victim place with the the victim
                if (!Utilities.IsOdd(Victim.Id) && Victim.Id <= 10)
                    Places.Find(delegate(Place p) { return p.Id == Victim.VictimPlace; }).EvenMaleSuspect = Victim;
                else if (Utilities.IsOdd(Victim.Id) && Victim.Id <= 10)
                    Places.Find(delegate(Place p) { return p.Id == Victim.VictimPlace; }).OddMaleSuspect = Victim;
                else if (!Utilities.IsOdd(Victim.Id) && Victim.Id > 10)
                    Places.Find(delegate(Place p) { return p.Id == Victim.VictimPlace; }).EvenFemaleSuspect = Victim;
                else if (Utilities.IsOdd(Victim.Id) && Victim.Id > 10)
                    Places.Find(delegate(Place p) { return p.Id == Victim.VictimPlace; }).OddFemaleSuspect = Victim;

                // place the victim temporarily into the place with three suspects to serve as a place holder
                if (!Utilities.IsOdd(Victim.Id) && Victim.Id <= 10)
                    Places.Find(delegate(Place p) { return p.Id == placeThreeSuspects; }).EvenMaleSuspect = Victim;
                else if (Utilities.IsOdd(Victim.Id) && Victim.Id <= 10)
                    Places.Find(delegate(Place p) { return p.Id == placeThreeSuspects; }).OddMaleSuspect = Victim;
                else if (!Utilities.IsOdd(Victim.Id) && Victim.Id > 10)
                    Places.Find(delegate(Place p) { return p.Id == placeThreeSuspects; }).EvenFemaleSuspect = Victim;
                else if (Utilities.IsOdd(Victim.Id) && Victim.Id > 10)
                    Places.Find(delegate(Place p) { return p.Id == placeThreeSuspects; }).OddFemaleSuspect = Victim;

                // place the murderer in their place
                if (!Utilities.IsOdd(Murderer.Id) && Murderer.Id <= 10)
                    Places.Find(delegate(Place p) { return p.Id == Murderer.MurdererPlace; }).EvenMaleSuspect = Murderer;
                else if (Utilities.IsOdd(Murderer.Id) && Murderer.Id <= 10)
                    Places.Find(delegate(Place p) { return p.Id == Murderer.MurdererPlace; }).OddMaleSuspect = Murderer;
                else if (!Utilities.IsOdd(Murderer.Id) && Murderer.Id > 10)
                    Places.Find(delegate(Place p) { return p.Id == Murderer.MurdererPlace; }).EvenFemaleSuspect = Murderer;
                else if (Utilities.IsOdd(Murderer.Id) && Murderer.Id > 10)
                    Places.Find(delegate(Place p) { return p.Id == Murderer.MurdererPlace; }).OddFemaleSuspect = Murderer;

                // loop through each place and store the suspects.  skip the place with the victim
                foreach (Place p in Places)
                {
                    if (p.Id != Victim.VictimPlace)
                    {
                        // store the male odd suspect
                        if (p.OddMaleSuspect == null)
                        {
                            p.OddMaleSuspect = Suspects.Find(delegate(Suspect s) { return s.Id == maleOddSuspects[maleOddSuspectsCount]; });
                            Suspects.Find(delegate(Suspect s) { return s.Id == maleOddSuspects[maleOddSuspectsCount]; }).AlibiPlace = p;
                            maleOddSuspectsCount++;
                        }

                        // store the male even suspect
                        if (p.EvenMaleSuspect == null)
                        {
                            p.EvenMaleSuspect = Suspects.Find(delegate(Suspect s) { return s.Id == maleEvenSuspects[maleEvenSuspectsCount]; });
                            Suspects.Find(delegate(Suspect s) { return s.Id == maleEvenSuspects[maleEvenSuspectsCount]; }).AlibiPlace = p;
                            maleEvenSuspectsCount++;
                        }

                        // store the female odd suspect
                        if (p.OddFemaleSuspect == null)
                        {
                            p.OddFemaleSuspect = Suspects.Find(delegate(Suspect s) { return s.Id == femaleOddSuspects[femaleOddSuspectsCount]; });
                            Suspects.Find(delegate(Suspect s) { return s.Id == femaleOddSuspects[femaleOddSuspectsCount]; }).AlibiPlace = p;
                            femaleOddSuspectsCount++;
                        }

                        // store the female even suspect
                        if (p.EvenFemaleSuspect == null)
                        {
                            p.EvenFemaleSuspect = Suspects.Find(delegate(Suspect s) { return s.Id == femaleEvenSuspects[femaleEvenSuspectsCount]; });
                            Suspects.Find(delegate(Suspect s) { return s.Id == femaleEvenSuspects[femaleEvenSuspectsCount]; }).AlibiPlace = p;
                            femaleEvenSuspectsCount++;
                        }
                    }
                }

                // now remove the victim from the place having only three suspects and put in place holder suspect
                if (!Utilities.IsOdd(Victim.Id) && Victim.Id <= 10)
                    Places.Find(delegate(Place p) { return p.Id == placeThreeSuspects; }).EvenMaleSuspect = new Suspect(0);
                else if (Utilities.IsOdd(Victim.Id) && Victim.Id <= 10)
                    Places.Find(delegate(Place p) { return p.Id == placeThreeSuspects; }).OddMaleSuspect = new Suspect(0);
                else if (!Utilities.IsOdd(Victim.Id) && Victim.Id > 10)
                    Places.Find(delegate(Place p) { return p.Id == placeThreeSuspects; }).EvenFemaleSuspect = new Suspect(0);
                else if (Utilities.IsOdd(Victim.Id) && Victim.Id > 10)
                    Places.Find(delegate(Place p) { return p.Id == placeThreeSuspects; }).OddFemaleSuspect = new Suspect(0);

                // put in place holder suspects for place that contains the victim
                victimPlace = Places.Find(delegate(Place p) { return p.Id == Victim.VictimPlace; });
                if (victimPlace.EvenMaleSuspect == null)
                    victimPlace.EvenMaleSuspect = new Suspect(0);
                if (victimPlace.OddMaleSuspect == null)
                    victimPlace.OddMaleSuspect = new Suspect(0);
                if (victimPlace.EvenFemaleSuspect == null)
                    victimPlace.EvenFemaleSuspect = new Suspect(0); 
                if (victimPlace.OddFemaleSuspect == null)
                    victimPlace.OddFemaleSuspect = new Suspect(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This will determine what information is part of the suspect's alibi
        /// </summary>
        public void GetSuspectAlibi()
        {
            // declare variables
            int[] alibiInfo;            // an array of four random numbers three will be used to determine the alibi information
                                        // 1 = location
                                        // 2 = area
                                        // 3 = place
                                        // 4 = suspects

            int[] alibiSuspects;        // an array of four random numbers three will be used to determine the suspects used
                                        // 1 = odd male suspect
                                        // 2 = even male suspect
                                        // 3 = odd female suspect
                                        // 4 = even female suspect

            int suspectsToShow = 0;     // the number of suspects to show in the alibi
            int alibiInfoToShow = 0;    // the number of alibi information to show
            int counter = 0;            // a counter to help determine number of suspects to show in alibi
            int alibiCounter = 0;       // a counter to help determine the alibi information

            try
            {
                // first set the place where the victim was found (we could not do this when determining the victim as the place information wasn't defined yet)
                Victim.AlibiPlace = Places.Find(delegate(Place p) { return p.Id == Victim.VictimPlace; });

                // now loop through each suspect to determine what their alibi information will be
                foreach (Suspect s in Suspects)
                {
                    // skip if this is the victim, victim doesn't have an alibi
                    if (s.Id != Victim.Id)
                    {
                        // reset counter for alibi array
                        alibiCounter = 0;

                        // populate the array of four random numbers.  this will determine the alibi information for each suspect
                        alibiInfo = Utilities.GetArrayofRandomNumbers(1, 4, randomNumbers);

                        // get the alibi information to show for this suspect
                        alibiInfoToShow = randomNumbers.Next(1, 4);

                        do
                        {
                            // set location as alibi
                            if (alibiInfo[alibiCounter] == 1)
                                s.AlibiShowLocation = true;

                            // set the area as alibi
                            else if (alibiInfo[alibiCounter] == 2)
                                s.AlibiShowArea = true;

                            // set the place as alibi
                            else if (alibiInfo[alibiCounter] == 3)
                                s.AlibiShowPlace = true;

                            // set the suspects as alibi
                            else if (alibiInfo[alibiCounter] == 4)
                            {
                                // reset the counter for the array
                                counter = 0;

                                // first determine the number of suspects to show as alibi
                                suspectsToShow = randomNumbers.Next(1, 4);

                                // get the array of suspects in random order
                                alibiSuspects = Utilities.GetArrayofRandomNumbers(1, 4, randomNumbers);

                                // now set which suspects will be included in the alibi
                                for (int count = alibiSuspects.GetLowerBound(0); count <= alibiSuspects.GetUpperBound(0); count++)
                                {
                                    // get the first suspect to potentially use and make sure this is not the current suspect
                                    // odd male suspect
                                    if (s.AlibiPlace.OddMaleSuspect.Id != 0)
                                        if (alibiSuspects[count] == 1 && s.AlibiPlace.OddMaleSuspect.Id != s.Id)
                                        {
                                                s.AlibiShowOddMaleSuspect = true;
                                                counter++;
                                        }

                                    // even male suspect
                                    if (s.AlibiPlace.EvenMaleSuspect.Id != 0)
                                        if (alibiSuspects[count] == 2 && s.AlibiPlace.EvenMaleSuspect.Id != s.Id)
                                        {
                                            s.AlibiShowEvenMaleSuspect = true;
                                            counter++;
                                        }

                                    // odd female suspect
                                    if (s.AlibiPlace.OddFemaleSuspect.Id != 0)
                                        if (alibiSuspects[count] == 3 && s.AlibiPlace.OddFemaleSuspect.Id != s.Id)
                                        {
                                            s.AlibiShowOddFemaleSuspect = true;
                                            counter++;
                                        }

                                    // even female suspect
                                    if (s.AlibiPlace.EvenFemaleSuspect.Id != 0)
                                        if (alibiSuspects[count] == 4 && s.AlibiPlace.EvenFemaleSuspect.Id != s.Id)
                                        {
                                            s.AlibiShowEvenFemaleSuspect = true;
                                            counter++;
                                        }

                                    // check if the number of suspects is reached
                                    if (counter >= suspectsToShow)
                                        break;
                                }
                            }

                            // increment alibi information counter
                            alibiCounter++;

                        } while (alibiCounter < alibiInfoToShow);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Saves the crime once all the scrime scene is generated.  This will be used to help faciliate answering questions quicker and updating the case fact sheet.
        /// </summary>
        public void SaveCrime()
        {
            // save information about the game


        }

        /// <summary>
        /// This saves information about the current game
        /// </summary>
        public void SaveGameInformation()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// A list of all suspects
        /// </summary>
        public List<Suspect> Suspects { get; set; }

        /// <summary>
        /// A list of places
        /// </summary>
        public List<Place> Places { get; set; }

        /// <summary>
        /// The suspect who is murdered
        /// </summary>
        public Suspect Victim { get; set; }

        /// <summary>
        /// The suspect who is the murderer
        /// </summary>
        public Suspect Murderer { get; set; }

        #endregion


    }
}
