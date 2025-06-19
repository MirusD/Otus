namespace SOLID.Domain.Enums
{
    public enum GuessResult
    {
        Correct,     // Правильный ответ
        TooLow,      // Ответ меньше загаданного числа
        TooHigh,     // Ответ больше загаданного числа
        AttemptsOver // Закончились попытки
    }
}
