using PersonalAccount.Domain;

// Лого
CurrentApplication.ShowLogo();

// Пауза
while (true)
{
    await Task.Delay(TimeSpan.FromHours(1));
}

