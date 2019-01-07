namespace APP.Business.Services.Models
{
    public class ResultModel
    {

        public bool? Status { get; set; }
        public string Message { get; set; }
    }
    public class ResultModel<T> : ResultModel
    {
        public T Data { get; set; }
        public int? Total { get; set; }
    }
}
