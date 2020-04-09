public static partial class ArrayExtensions
{
    public static T[] Add<T>(this T[] target, T item)
    {
        if (target == null)
        {
            //TODO: Return null or throw ArgumentNullException;
        }
        T[] result = new T[target.Length + 1];
        target.CopyTo(result, 0);
        result[target.Length] = item;
        return result;
    }
}

