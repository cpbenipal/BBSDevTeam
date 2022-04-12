namespace BBS.Dto
{
    public class GenericApiResponse
    {
        public int ReturnCode { get; set; }
        public object ReturnMessage { get; set; }
        public object ReturnData { get; set; }
        public bool ReturnStatus { get; set; }
        public GenericApiResponse()
        {
            ReturnCode = 0;
            ReturnMessage = null;
            ReturnData = null;
            ReturnStatus = false;
        }
  
    }
}
