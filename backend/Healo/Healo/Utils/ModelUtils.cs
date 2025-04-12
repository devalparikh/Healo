namespace Healo.Models;

public class ModelUtils
{
    public static IEnumerable<TEnum> GetIndividualFlags<TEnum>(TEnum compositeType) where TEnum : struct, Enum
    {
        return Enum.GetValues<TEnum>()
            .Where(e =>
                !e.Equals(default(TEnum)) &&
                compositeType.HasFlag(e) &&
                !IsCompositeFlag(e));
    }

    private static bool IsCompositeFlag<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var intValue = Convert.ToInt32(value);
        return (intValue & (intValue - 1)) != 0; // Checks if more than one bit is set
    }
}