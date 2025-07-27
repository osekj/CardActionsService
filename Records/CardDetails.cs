using MadiffTechnicalAssignment.Enums;

namespace MadiffTechnicalAssignment.Records
{
    public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet)
    {
    }
}
