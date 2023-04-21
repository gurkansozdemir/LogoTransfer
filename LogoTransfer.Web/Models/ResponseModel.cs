namespace LogoTransfer.Web.Models
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
