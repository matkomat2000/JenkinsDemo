using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace PRO_Vaja_1_Preizkusanje_enot
{
    public class Tekmovalec
    {
        private readonly int id;
        private readonly string ime;
        private readonly string priimek;
        private string drzava;
        private bool poomse;
        private bool borbe;
        private bool kick;
        private bool kaznovan;

        private int ranking = 0;

        private double tocke;
        private double avgPoomsePerformance;
        List<double> pointsFromCompetition_poomse = new List<double>();
        private double avgBorbePerformance;
        List<double> pointsFromCompetition_borbe = new List<double>();
        private double avgKickPerformance;
        List<double> pointsFromCompetition_kick = new List<double>();

        private Tekmovalec() { }

        public Tekmovalec(int spec_id, string theName, string theSurname, string country, bool pom, bool bor, bool kic)
        {
            id = spec_id;
            ime = theName;
            priimek = theSurname;
            drzava = country;
            poomse = pom;
            borbe = bor;
            kick = kic;
            kaznovan = false;
        }

        public int ID_Tekmovalec
        {
            get { return id; }
        }

        public string Ime_Tekmovalec
        {
            get { return ime; }
        }
        public string Priimek_Tekmovalec
        {
            get { return priimek; }
        }
        public string Country_Tekmovalec
        {
            get { return drzava; }
        }
        public bool Poomse_Tekmovalec
        {
            get { return poomse; }
        }
        public bool Borbe_Tekmovalec
        {
            get { return borbe; }
        }
        public bool Kick_Tekmovalec
        {
            get { return kick; }
        }
        public double Tocke_Tekmovalec
        {
            get { return tocke; }
        }
        public int Raking_Tekmovalec
        {
            get { return ranking; }
        }
        public bool Suspenz_Tekmovalec
        {
            get { return kaznovan; }
        }
        public List<double> PoomseTockeVektor
        {
            get { return pointsFromCompetition_poomse; }
        }
        public List<double> BorbeTockeVektor
        {
            get { return pointsFromCompetition_borbe; }
        }
        public List<double> KickTockeVektor
        {
            get { return pointsFromCompetition_kick; }
        }

        public static List<string> CountryList()
        {
            List<string> CultureList = new List<string>();
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach(CultureInfo getCulture in getCultureInfo)
            {
                RegionInfo GetRegionInfo = new RegionInfo(getCulture.LCID);
                if (!(CultureList.Contains(GetRegionInfo.EnglishName)))
                {
                    CultureList.Add(GetRegionInfo.EnglishName);
                }
            }
            CultureList.Sort();
            return CultureList;
        }

        public void changeCountry (string newCountry)
        {
            List<string> vseDrzave = CountryList();
            if (vseDrzave.Contains(newCountry))
            {
                drzava = newCountry;
            }
            else
            {
                throw new InvalidCastException("This country does not exist!");
            }
        }

        public void AddPoomsePoints(int place, int numberOfContestants, int numberOfRounds)
        {
            if (poomse)
            {
                if(place > numberOfContestants)
                {
                    throw new InvalidCastException("This is not possible, please check your input.");
                }
                else
                {
                    int resoult = numberOfContestants - place;
                    resoult *= numberOfRounds;
                    tocke += resoult;

                    pointsFromCompetition_poomse.Add(resoult);
                }
            }
        }

        public void AddBorbePoints(int fightsWon, int rankOfTournament, int numberOfCompetitors)
        {
            if (borbe)
            {
                if(fightsWon > numberOfCompetitors / 2)
                {
                    throw new InvalidCastException("This is not possible, please check your input.");
                }
                else
                {
                    double newTournament = fightsWon * rankOfTournament;

                    if (numberOfCompetitors < 12)
                    {
                        newTournament /= 2;
                    }
                    tocke += newTournament;
                    pointsFromCompetition_borbe.Add(newTournament);
                }
            }
        }

        public void AddKickPoints(int rounds)
        {
            if (kick)
            {
                tocke += rounds;
                pointsFromCompetition_kick.Add(rounds);
            }
        }

        public double DoPoomseAVG()
        {
            avgPoomsePerformance = pointsFromCompetition_poomse.Average();
            return avgPoomsePerformance;
        }
        public double DoBorbeAVG()
        {
            avgBorbePerformance = pointsFromCompetition_borbe.Average();
            return avgBorbePerformance;
        }
        public double DoKickAVG()
        {
            avgKickPerformance = pointsFromCompetition_kick.Average();
            return avgKickPerformance;
        }

        public void PenaltyPoints(double pen)
        {
            tocke -= pen;
        }

        public void Ban()
        {
            kaznovan = true;
        }

        public void Unban()
        {
            kaznovan = false;
        }

        public void setRanking(int rank)
        {
            ranking = rank;
        }

        public double sumAllPoints()
        {
            return pointsFromCompetition_poomse.Sum() + pointsFromCompetition_borbe.Sum() + pointsFromCompetition_kick.Sum();
        }

        public int SetNosilec(List<Tekmovalec> vsi)
        {
            int counter = 0;
            foreach(var el in vsi)
            {
                if(this.sumAllPoints() > el.sumAllPoints())
                {
                    counter++;
                }
            }
            ranking = vsi.Count() - counter;
            if(ranking > vsi.Count()/2)
            {
                return 0;
            }
            else
            {
                return ranking;
            }
        }

        public string Print()
        {
            string output = id.ToString() + "\t" + ime + "\t" + priimek + "\tRank: " + ranking.ToString() + ",\t" + drzava + "\t|\t";
            if (poomse)
            {
                output += "Poomse:" + pointsFromCompetition_poomse.Sum().ToString() + "\t";
            }
            if (borbe)
            {
                output += "Borbe:" + pointsFromCompetition_borbe.Sum().ToString() + "\t";
            }
            if (kick)
            {
                output += "Kick:" + pointsFromCompetition_kick.Sum().ToString();
            }
            return "\n" + output;
        }
        public static void Main()
        {
            List<Tekmovalec> vsi = new List<Tekmovalec>();
            Tekmovalec koren = new Tekmovalec(0, "Matevz", "Koren", "San Marino", true, true, true);
            koren.AddBorbePoints(4, 4, 16);
            koren.AddBorbePoints(5, 2, 62);
            vsi.Add(koren);
            Tekmovalec kicker = new Tekmovalec(1, "Veso", "Lola Ribar", "Kosovo", false, false, true);
            kicker.AddKickPoints(3);
            kicker.AddKickPoints(5);
            vsi.Add(kicker);
            Tekmovalec pomzer = new Tekmovalec(2, "Hari", "Mata Hari", "BiH", true, false, false);
            pomzer.AddPoomsePoints(1, 4, 1);
            vsi.Add(pomzer);
            int korenRanking = koren.Raking_Tekmovalec;
            int borbeNosilec = koren.SetNosilec(vsi);
            int pomzerRaking = pomzer.Raking_Tekmovalec;
            int poomseNosilec = pomzer.SetNosilec(vsi);
            int kickerRaking = kicker.Raking_Tekmovalec;
            int kickerNosilec = kicker.SetNosilec(vsi);
            Console.WriteLine("Borbe: " + borbeNosilec.ToString() + "\nPoomse: " + poomseNosilec.ToString() + "\nKick: " + kickerNosilec.ToString());
        }
    }
}
