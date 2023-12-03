using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Client1", "Client2", "Client3", "Client4", "Client5"
        };
        private readonly ILogger<ClientsController> _logger;
        private static List<Client> _clients = new List<Client>();

        public ClientsController(ILogger<ClientsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetClients")]
        public IEnumerable<Client> Get()
        {
            return _clients;
        }

        [HttpGet("{id}", Name = "GetClientById")]
        public IActionResult GetById(int id)
        {
            var client = _clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost(Name = "CreateClient")]
        public IActionResult Create([FromBody] Client client)
        {
            client.Id = _clients.Count + 1;
            _clients.Add(client);
            return CreatedAtRoute("GetClientById", new { id = client.Id }, client);
        }

        [HttpPut("{id}", Name = "UpdateClient")]
        public IActionResult Update(int id, [FromBody] Client client)
        {
            var existingClient = _clients.FirstOrDefault(c => c.Id == id);
            if (existingClient == null)
            {
                return NotFound();
            }

            existingClient.Name = client.Name;
            existingClient.Email = client.Email;
            existingClient.PhoneNumber = client.PhoneNumber;

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteClient")]
        public IActionResult Delete(int id)
        {
            var existingClient = _clients.FirstOrDefault(c => c.Id == id);
            if (existingClient == null)
            {
                return NotFound();
            }

            _clients.Remove(existingClient);
            return NoContent();
        }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
