using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using ElectronicDetective;
using System.Configuration;

namespace GlennSite.Controllers
{
    public class ElectronicDetectiveController : Controller
    {
        // GET: ElectronicDetective
        public ActionResult ElectronicDetective()
        {
            return View();
        }

        public ActionResult HowToPlay()
        {
            return View();
        }

        public ActionResult ViewMurderData()
        {
            ViewBag.Message = "Welcome to Glenn's Web Site";

            // test logic to create crime scene
            DataRetrieval.SetSqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            Murder crimeScene = new Murder();
            string alibi = "";
            Suspect alibiSuspect;

            crimeScene.CreateCrimeScene();

            // get id and name of victim
            ViewBag.VictimId = crimeScene.Victim.Id;
            ViewBag.VictimName = crimeScene.Victim.FirstName + " " + crimeScene.Victim.LastName;
            ViewBag.VictimPlaceId = crimeScene.Victim.VictimPlace;
            ViewBag.VictimPlaceName = crimeScene.Victim.AlibiPlace.PlaceDescription;

            // get murderer information
            ViewBag.MurderId = crimeScene.Murderer.Id;
            ViewBag.MurderName = crimeScene.Murderer.FirstName + " " + crimeScene.Murderer.LastName;
            ViewBag.MurderPlaceId = crimeScene.Murderer.MurdererPlace;
            ViewBag.MurderPlaceName = crimeScene.Murderer.AlibiPlace.PlaceDescription;
            ViewBag.Sex = crimeScene.Murderer.SexName;
            ViewBag.Location = crimeScene.Murderer.AlibiPlace.LocationName;
            ViewBag.Area = crimeScene.Murderer.AlibiPlace.AreaName;
            ViewBag.WeaponUsed = crimeScene.Murderer.MurderWeaponName;

            // get place information
            ViewBag.AEvenMale = crimeScene.Places[0].EvenMaleSuspect.Id;
            ViewBag.AOddMale = crimeScene.Places[0].OddMaleSuspect.Id;
            ViewBag.AEvenFemale = crimeScene.Places[0].EvenFemaleSuspect.Id;
            ViewBag.AOddFemale = crimeScene.Places[0].OddFemaleSuspect.Id;
            ViewBag.ALocation = crimeScene.Places[0].LocationName;
            ViewBag.AArea = crimeScene.Places[0].AreaName;
            ViewBag.AWeapon = crimeScene.Places[0].WeaponName;

            ViewBag.BEvenMale = crimeScene.Places[1].EvenMaleSuspect.Id;
            ViewBag.BOddMale = crimeScene.Places[1].OddMaleSuspect.Id;
            ViewBag.BEvenFemale = crimeScene.Places[1].EvenFemaleSuspect.Id;
            ViewBag.BOddFemale = crimeScene.Places[1].OddFemaleSuspect.Id;
            ViewBag.BLocation = crimeScene.Places[1].LocationName;
            ViewBag.BArea = crimeScene.Places[1].AreaName;
            ViewBag.BWeapon = crimeScene.Places[1].WeaponName;

            ViewBag.CEvenMale = crimeScene.Places[2].EvenMaleSuspect.Id;
            ViewBag.COddMale = crimeScene.Places[2].OddMaleSuspect.Id;
            ViewBag.CEvenFemale = crimeScene.Places[2].EvenFemaleSuspect.Id;
            ViewBag.COddFemale = crimeScene.Places[2].OddFemaleSuspect.Id;
            ViewBag.CLocation = crimeScene.Places[2].LocationName;
            ViewBag.CArea = crimeScene.Places[2].AreaName;
            ViewBag.CWeapon = crimeScene.Places[2].WeaponName;

            ViewBag.DEvenMale = crimeScene.Places[3].EvenMaleSuspect.Id;
            ViewBag.DOddMale = crimeScene.Places[3].OddMaleSuspect.Id;
            ViewBag.DEvenFemale = crimeScene.Places[3].EvenFemaleSuspect.Id;
            ViewBag.DOddFemale = crimeScene.Places[3].OddFemaleSuspect.Id;
            ViewBag.DLocation = crimeScene.Places[3].LocationName;
            ViewBag.DArea = crimeScene.Places[3].AreaName;
            ViewBag.DWeapon = crimeScene.Places[3].WeaponName;

            ViewBag.EEvenMale = crimeScene.Places[4].EvenMaleSuspect.Id;
            ViewBag.EOddMale = crimeScene.Places[4].OddMaleSuspect.Id;
            ViewBag.EEvenFemale = crimeScene.Places[4].EvenFemaleSuspect.Id;
            ViewBag.EOddFemale = crimeScene.Places[4].OddFemaleSuspect.Id;
            ViewBag.ELocation = crimeScene.Places[4].LocationName;
            ViewBag.EArea = crimeScene.Places[4].AreaName;
            ViewBag.EWeapon = crimeScene.Places[4].WeaponName;

            ViewBag.FEvenMale = crimeScene.Places[5].EvenMaleSuspect.Id;
            ViewBag.FOddMale = crimeScene.Places[5].OddMaleSuspect.Id;
            ViewBag.FEvenFemale = crimeScene.Places[5].EvenFemaleSuspect.Id;
            ViewBag.FOddFemale = crimeScene.Places[5].OddFemaleSuspect.Id;
            ViewBag.FLocation = crimeScene.Places[5].LocationName;
            ViewBag.FArea = crimeScene.Places[5].AreaName;
            ViewBag.FWeapon = crimeScene.Places[5].WeaponName;

            // alibis
            alibi = "Suspect 1: ";
            alibiSuspect = crimeScene.Suspects[0];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect1Abili = alibi;

            alibi = "Suspect 2: ";
            alibiSuspect = crimeScene.Suspects[1];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect2Abili = alibi;

            alibi = "Suspect 3: ";
            alibiSuspect = crimeScene.Suspects[2];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect3Abili = alibi;

            alibi = "Suspect 4: ";
            alibiSuspect = crimeScene.Suspects[3];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect4Abili = alibi;

            alibi = "Suspect 5: ";
            alibiSuspect = crimeScene.Suspects[4];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect5Abili = alibi;

            alibi = "Suspect 6: ";
            alibiSuspect = crimeScene.Suspects[5];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect6Abili = alibi;

            alibi = "Suspect 7: ";
            alibiSuspect = crimeScene.Suspects[6];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect7Abili = alibi;

            alibi = "Suspect 8: ";
            alibiSuspect = crimeScene.Suspects[7];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect8Abili = alibi;

            alibi = "Suspect 9: ";
            alibiSuspect = crimeScene.Suspects[8];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect9Abili = alibi;

            alibi = "Suspect 10: ";
            alibiSuspect = crimeScene.Suspects[9];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect10Abili = alibi;

            alibi = "Suspect 11: ";
            alibiSuspect = crimeScene.Suspects[10];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect11Abili = alibi;

            alibi = "Suspect 12: ";
            alibiSuspect = crimeScene.Suspects[11];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect12Abili = alibi;

            alibi = "Suspect 13: ";
            alibiSuspect = crimeScene.Suspects[12];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect13Abili = alibi;

            alibi = "Suspect 14: ";
            alibiSuspect = crimeScene.Suspects[13];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect14Abili = alibi;

            alibi = "Suspect 15: ";
            alibiSuspect = crimeScene.Suspects[14];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect15Abili = alibi;

            alibi = "Suspect 16: ";
            alibiSuspect = crimeScene.Suspects[15];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect16Abili = alibi;

            alibi = "Suspect 17: ";
            alibiSuspect = crimeScene.Suspects[16];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect17Abili = alibi;

            alibi = "Suspect 18: ";
            alibiSuspect = crimeScene.Suspects[17];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect18Abili = alibi;

            alibi = "Suspect 19: ";
            alibiSuspect = crimeScene.Suspects[18];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect19bili = alibi;

            alibi = "Suspect 20: ";
            alibiSuspect = crimeScene.Suspects[19];
            if (alibiSuspect.AlibiShowLocation)
                alibi += alibiSuspect.AlibiPlace.LocationName + ' ';
            if (alibiSuspect.AlibiShowArea)
                alibi += alibiSuspect.AlibiPlace.AreaName + ' ';
            if (alibiSuspect.AlibiShowPlace)
                alibi += alibiSuspect.AlibiPlace.PlaceDescription + ' ';
            if (alibiSuspect.AlibiShowOddMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenMaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenMaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowOddFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.OddFemaleSuspect.Id.ToString() + ' ';
            if (alibiSuspect.AlibiShowEvenFemaleSuspect)
                alibi += alibiSuspect.AlibiPlace.EvenFemaleSuspect.Id.ToString() + ' ';
            ViewBag.Suspect20Abili = alibi;

            return View();
        }
    }
}