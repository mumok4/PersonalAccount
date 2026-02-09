using PersonalAccount.Domain;

var class1 = new Entry();
var result = class1.GetWelcomeMessage();
Console.WriteLine(result);

while (true)
{
    await Task.Delay(TimeSpan.FromHours(1));
}

