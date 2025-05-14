namespace ServicioIPS.Middleware
{
    public class ErrorMiddleware
    {
        public string Type { get; set; } = default!;
        public string Title { get; set; } = default!;
        public int Status { get; set; }
        public string TraceId { get; set; } = default!;

        public ErrorType? Errors { get; set; }
    }
}
