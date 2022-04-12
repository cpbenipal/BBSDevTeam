namespace BBS.Dto
{
    public class AdditionalPersonInformationDto
    {
        public bool IsUSCitizen { get; set; }
        public bool IsPublicSectorEmployee { get; set; }
        public bool IsIndividual { get; set; }
        public bool HaveCriminalRecord { get; set; }
        public bool HaveConvicted { get; set; }
    }
}
