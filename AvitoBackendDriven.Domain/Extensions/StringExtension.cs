namespace AvitoBackendDriven.Domain.Extensions;

/// <summary>
/// Расширение для класса <see cref="String"/>
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Проверяет является ли входная строка пустой или нулевой
    /// </summary>
    /// <param name="inputString">Входная строка для проверки</param>
    /// <returns>
    /// <b>true</b> - если пустая или равна null
    /// <br/>
    /// иначе <b>false</b>
    /// </returns>
    public static bool IsNullOrEmpty(this string? inputString) => string.IsNullOrEmpty(inputString);

    /// <summary>
    /// Конвертирует массив строк в <see cref="String"/>
    /// </summary>
    /// <param name="strings">Конвертируемый массив строк</param>
    /// <param name="separator">Разделитель</param>
    /// <returns>Строка из массива с разделителями</returns>
    public static string ArrayToString(this IEnumerable<string> strings, string separator) => string.Join(separator, strings);

    /// <summary>
    /// Конвертирует строку в <see cref="double"/>
    /// </summary>
    /// <param name="value">Конвертируемая строка</param>
    /// <returns><see cref="double"/></returns>
    /// <exception cref="ArgumentException">Если не удалось конвертировать</exception>
    public static double ToDouble(this string value) => !double.TryParse(value, out var result) 
            ? throw new ArgumentException($"Failed to convert string '{value}' to double") 
            : result;

    /// <summary>
    /// Конвертирует строку в <see cref="int"/>
    /// </summary>
    /// <param name="value">Конвертируемая строка</param>
    /// <returns><see cref="int"/></returns>
    /// <exception cref="ArgumentException">Если не удалось конвертировать</exception>
    public static int ToInt(this string value) => !int.TryParse(value, out var result) ? 0 : result;

    /// <summary>
    /// Конвертирует строку в <see cref="bool"/>
    /// </summary>
    /// <param name="value">Конвертируемая строка</param>
    /// <returns><see cref="bool"/></returns>
    /// <exception cref="ArgumentException">Если не удалось конвертировать</exception>
    public static bool ToBoolean(this string value) => bool.TryParse(value, out var result) && result;
}