namespace Pattern_prototype
{
    /// <summary>
    /// Дженерик интерфейс для реализации шаблона "Прототип"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IMyCloneable<T>
    {
        T Clone();
    }
}
