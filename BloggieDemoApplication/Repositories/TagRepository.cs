using BloggieDemoApplication.Data;
using BloggieDemoApplication.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BloggieDemoApplication.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDbContext.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(id);
            if (existingTag != null) 
            {
                bloggieDbContext.Tags.Remove(existingTag);
                await bloggieDbContext.SaveChangesAsync();
                return existingTag;

            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {

            return await bloggieDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return bloggieDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null) 
            { 
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
