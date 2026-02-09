using NUnit.Framework;
using PersonalAccount.Domain;

namespace PersonalAccount.UnitTests;

[TestFixture]
public class TestEntry
{
    [Test]
    public void Check_WelcomeMessage()
    {
        // Подготовка
        var class1 = new Entry();

        // Действие
        var result = class1.GetWelcomeMessage();

        // Проверки
        Assert.That( string.IsNullOrWhiteSpace(result) == false);
    }
}
