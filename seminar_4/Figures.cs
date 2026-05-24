namespace seminar_4;

/// <summary>
/// Абстрактная фигура
/// </summary>
/// <param name="type">Название типа фигуры</param>
internal abstract class Figure(string type) : IComparable
{
    /// <summary>
    /// Тип фигуры
    /// </summary>
    public string Type { get; } = type;

    /// <summary>
    /// Площадь фигуры
    /// </summary>
    public abstract double Area { get; }

    /// <summary>
    /// Приведение объекта к строке
    /// </summary>
    public override string ToString() => $"{Type} площадью {Area}";

    /// <summary>
    /// Сравнение фигур по площади
    /// </summary>
    public int CompareTo(object? obj) =>
        obj is Figure other
            ? Area.CompareTo(other.Area)
            : throw new ArgumentException("Объект не является фигурой");
}

/// <summary>
/// Класс Круг
/// </summary>
/// <param name="radius">Радиус круга</param>
internal class Circle(double radius) : Figure("Круг")
{
    /// <summary>
    /// Площадь круга
    /// </summary>
    public override double Area => Math.PI * radius * radius;
}

/// <summary>
/// Класс Прямоугольник
/// </summary>
/// <param name="height">Высота</param>
/// <param name="width">Ширина</param>
/// <param name="type">Тип фигуры</param>
internal class Rectangle(double height, double width, string type = "Прямоугольник") : Figure(type)
{
    /// <summary>
    /// Площадь прямоугольника
    /// </summary>
    public override double Area => width * height;
}

/// <summary>
/// Класс Квадрат
/// </summary>
/// <param name="size">Размер стороны квадрата</param>
internal class Square(double size) : Rectangle(size, size, "Квадрат")
{
}
