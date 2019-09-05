namespace Ads.Application.Dto
{
    public class CreateCommandDto
    {
        public int AdId { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
    }
}