using ContactAPI.DataContext;
using ContactAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace ContactAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly ContactDbContext _context;
        public ContactController(ContactDbContext _context)
        {
            this._context = _context;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await _context.contacts.ToListAsync());

        }
        [HttpPost]

        public async Task<IActionResult> Addcontacts(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                Name = addContactRequest.Name,
                Phone = addContactRequest.Phone

            };
            await _context.contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return Ok(contact);
        }
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateContacts([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await _context.contacts.FindAsync(id);
            if(contact != null)
            {
                contact.Name = updateContactRequest.Name;
                contact.Email = updateContactRequest.Email;
                contact.Phone = updateContactRequest.Phone;
                contact.Address = updateContactRequest.Address;
                await _context.SaveChangesAsync();

            }
            return NotFound();
        }
        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetIdContacts([FromRoute] Guid id)
        {
            var contact = await _context.contacts.FindAsync(id);
            if( contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContacts([FromRoute] Guid id)
        {
            var contact =await _context.contacts.FindAsync(id);
            if(contact != null)
            {
                _context.contacts.Remove(contact);
                await _context.SaveChangesAsync();
                return Ok(contact);

            }
            return NotFound();
        }





    }
}
