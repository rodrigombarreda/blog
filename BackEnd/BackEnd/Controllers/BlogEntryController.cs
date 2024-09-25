using BackEnd.Interfaces;
using BackEnd.Logic;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogEntryController : ControllerBase
    {
        private readonly IBlogEntryLogic _blogEntryLogic;

        public BlogEntryController(IBlogEntryLogic blogEntryLogic)
        {
            _blogEntryLogic = blogEntryLogic;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogEntry>>> GetAllEntries(int page, int pageSize, string filter = null)
        {
            // Get all entries for total count
            var allEntries = await _blogEntryLogic.GetAllEntries(1, int.MaxValue, filter);
            var totalEntries = allEntries.Count;

            // Get paginated entries
            var entries = await _blogEntryLogic.GetAllEntries(page, pageSize, filter);

            // Return the result with total count
            return Ok(new { entries, total = totalEntries });
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<BlogEntry>> GetEntryById(Guid id)
        {
            var entry = await _blogEntryLogic.GetEntryById(id);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [HttpPost]
        public async Task<ActionResult<BlogEntry>> CreateEntry([FromBody] EntryCreateOrUpdate EntryCreate)
        {
            var createdEntry = await _blogEntryLogic.CreateEntry(EntryCreate.title, EntryCreate.content, EntryCreate.category,EntryCreate.user);
            return CreatedAtAction(nameof(GetEntryById), new { id = createdEntry.Id }, createdEntry);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BlogEntry>> UpdateEntry(Guid id, [FromBody] EntryCreateOrUpdate EntryUpdate)
        {
            var updatedEntry = await _blogEntryLogic.UpdateEntry(id, EntryUpdate.title,EntryUpdate.content,EntryUpdate.category,EntryUpdate.user);
            return Ok(updatedEntry);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(Guid id)
        {
            var result = await _blogEntryLogic.DeleteEntry(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

    public class EntryCreateOrUpdate
    {
        public string title { get; set; }
        public string content { get; set; }

        public string category { get; set; }

        public string user { get; set; }
    }
}
