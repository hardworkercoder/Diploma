using Diploma.Models;

namespace Diploma.Services
{
    public interface IFileParser
    {
      List<ExpirementRow> Parse(IFormFile file);
    }
}
