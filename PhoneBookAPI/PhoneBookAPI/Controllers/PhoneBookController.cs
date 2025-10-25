using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class PhoneBookController : ControllerBase
{
    private static Dictionary<string, Contact> contacts = new Dictionary<string, Contact>();
    
    public PhoneBookController()
    {
        contacts.Clear();
        
        contacts["1"] = new PersonalContact 
        { 
            Id = "1", 
            PhoneNumber = "+79508380678", 
            FullName = "Швад" 
        };
        
        contacts["2"] = new BusinessContact 
        { 
            Id = "2", 
            PhoneNumber = "+79935315464", 
            FullName = "Шопор" 
        };
    }
    
    [HttpGet]
    public ActionResult<List<string>> Get()
    {
        var result = new List<string>();
        foreach (var contact in contacts.Values)
        {
            result.Add(contact.GetDisplay());
        }
        return result;
    }
    
    [HttpGet("{id}")]
    public ActionResult<Contact> Get(string id)
    {
        if (contacts.ContainsKey(id))
            return contacts[id];
        return NotFound();
    }
    
    [HttpPost]
    public ActionResult<Contact> Post([FromBody] ContactRequest request)
    {
        Contact contact;
        
        if (request.Type.ToLower() == "business")
            contact = new BusinessContact();
        else
            contact = new PersonalContact();
            
        contact.Id = System.Guid.NewGuid().ToString();
        contact.PhoneNumber = request.PhoneNumber;
        contact.FullName = request.FullName;
        
        contacts[contact.Id] = contact;
        return contact;
    }
    
    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] ContactRequest request)
    {
        if (!contacts.ContainsKey(id))
            return NotFound();
        
        var contact = contacts[id];
        contact.PhoneNumber = request.PhoneNumber;
        contact.FullName = request.FullName;
        
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        if (!contacts.ContainsKey(id))
            return NotFound();
        
        contacts.Remove(id);
        return Ok();
    }
}