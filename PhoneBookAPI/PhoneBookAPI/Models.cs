public abstract class Contact
{
    public string Id { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName { get; set; }
    
    public abstract string GetDisplay();
}

public class PersonalContact : Contact
{
    public override string GetDisplay() => $"Личный контракт: {PhoneNumber} - {FullName}";
}

public class BusinessContact : Contact
{
    public override string GetDisplay() => $"Бизнес контракт: {PhoneNumber} - {FullName}";
}

public class ContactRequest
{
    public string PhoneNumber { get; set; }
    public string FullName { get; set; }
    public string Type { get; set; }
}