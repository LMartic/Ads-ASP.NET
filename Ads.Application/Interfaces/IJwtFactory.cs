namespace Ads.Application.Interfaces
{
    public interface IJwtFactory
    {
        string GenerateEncodeToken(string userId);
    }
}