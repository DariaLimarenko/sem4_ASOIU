using System.Collections;

namespace seminar_4;

/// <summary>
/// Контейнерный элемент списка
/// </summary>
/// <typeparam name="T">Тип элемента списка</typeparam>
/// <param name="data">Данные элемента</param>
public class SimpleListItem<T>(T data)
{
    /// <summary>
    /// Данные
    /// </summary>
    public T Data { get; set; } = data;

    /// <summary>
    /// Следующий элемент
    /// </summary>
    public SimpleListItem<T>? Next { get; set; }
}

/// <summary>
/// Простой однонаправленный список
/// </summary>
/// <typeparam name="T">Тип элемента списка</typeparam>
public class SimpleList<T> : IEnumerable<T>
    where T : IComparable
{
    /// <summary>
    /// Первый элемент списка
    /// </summary>
    protected SimpleListItem<T>? first;

    /// <summary>
    /// Последний элемент списка
    /// </summary>
    protected SimpleListItem<T>? last;

    /// <summary>
    /// Количество элементов
    /// </summary>
    public int Count { get; protected set; }

    /// <summary>
    /// Добавление элемента в конец списка
    /// </summary>
    public void Add(T element)
    {
        var newItem = new SimpleListItem<T>(element);

        Count++;

        if (last is null)
        {
            first = newItem;
            last = newItem;
        }
        else
        {
            last.Next = newItem;
            last = newItem;
        }
    }

    /// <summary>
    /// Получение контейнера с заданным номером
    /// </summary>
    public SimpleListItem<T> GetItem(int number)
    {
        if (number < 0 || number >= Count)
        {
            throw new IndexOutOfRangeException($"Индекс {number} выходит за границы списка");
        }

        var current = first;

        for (int i = 0; i < number; i++)
        {
            current = current!.Next;
        }

        return current!;
    }

    /// <summary>
    /// Получение элемента с заданным номером
    /// </summary>
    public T Get(int number) => GetItem(number).Data;

    /// <summary>
    /// Перебор коллекции
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        var current = first;

        while (current is not null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    /// <summary>
    /// Реализация необобщенного интерфейса IEnumerable
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Сортировка списка
    /// </summary>
    public void Sort()
    {
        if (Count > 1)
        {
            Sort(0, Count - 1);
        }
    }

    /// <summary>
    /// Быстрая сортировка
    /// </summary>
    private void Sort(int low, int high)
    {
        int i = low;
        int j = high;

        T x = Get((low + high) / 2);

        do
        {
            while (Get(i).CompareTo(x) < 0)
            {
                i++;
            }

            while (Get(j).CompareTo(x) > 0)
            {
                j--;
            }

            if (i <= j)
            {
                Swap(i, j);
                i++;
                j--;
            }
        }
        while (i <= j);

        if (low < j)
        {
            Sort(low, j);
        }

        if (i < high)
        {
            Sort(i, high);
        }
    }

    /// <summary>
    /// Обмен элементов местами
    /// </summary>
    private void Swap(int i, int j)
    {
        var ci = GetItem(i);
        var cj = GetItem(j);

        (ci.Data, cj.Data) = (cj.Data, ci.Data);
    }
}

/// <summary>
/// Стек на основе простого списка
/// </summary>
/// <typeparam name="T">Тип элемента стека</typeparam>
public class SimpleStack<T> : SimpleList<T>
    where T : IComparable
{
    /// <summary>
    /// Добавление элемента в стек
    /// </summary>
    public void Push(T element) => Add(element);

    /// <summary>
    /// Удаление и чтение элемента из стека
    /// </summary>
    public T Pop()
    {
        T result;

        if (Count == 0)
        {
            return default!;
        }

        if (Count == 1)
        {
            result = first!.Data;
            first = null;
            last = null;
        }
        else
        {
            var newLast = GetItem(Count - 2);

            result = newLast.Next!.Data;

            last = newLast;
            newLast.Next = null;
        }

        Count--;

        return result;
    }
}
