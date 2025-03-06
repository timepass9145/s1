namespace SIBSAPI.Global
{
    public class LoginResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Header { get; set; }
        public object DivionData { get; set; }
        public object ModuleData { get; set; }
        //public string Activity_Name { get; set; }
    }

}
