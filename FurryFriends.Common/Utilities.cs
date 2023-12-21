using System.Globalization;

namespace FurryFriends.Common;

public static class Utilities
{
    public static DateTime ToDate(this string dateString)
    {
        // Define aquí el formato esperado, por ejemplo "dd/MM/yyyy"
        var format = "dd/MM/yyyy";
        DateTime parsedDate;

        // Utiliza DateTime.TryParseExact si quieres un formato específico
        if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
        {
            return parsedDate;
        }

        // Si no se puede parsear la fecha, puedes retornar una fecha por defecto o lanzar una excepción
        // Depende de cómo quieras manejar las fechas inválidas
        // return default; // Retorna el valor por defecto para DateTime, que es 'DateTime.MinValue'
        throw new ArgumentException("Invalid date format.", nameof(dateString));
    }
}
