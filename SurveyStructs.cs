using System;

namespace SpaceCat
{
    public struct AreaSurvey
    {
        public int AreaID { get; }
        public DateTime TimeSurveyed { get; }
        public int SurveyNumber { get; }
        public int FilledSeats { get; }
        public string AdditionalNotes { get; }

        public AreaSurvey(int areaID, int surveyNumber, int filledSeats, string additionalNotes) : this()
        {
            AreaID = areaID;
            TimeSurveyed = DateTime.Now;
            SurveyNumber = surveyNumber;
            FilledSeats = filledSeats;
            AdditionalNotes = additionalNotes;
        }
    }
}