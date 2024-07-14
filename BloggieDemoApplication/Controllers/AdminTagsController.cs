using BloggieDemoApplication.Data;
using BloggieDemoApplication.Models.Domain;
using BloggieDemoApplication.Models.ViewModels;
using BloggieDemoApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggieDemoApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        
        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest) 
        {
            // Mapping AsTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

                await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List() 
        {
           var tags = await tagRepository.GetAllAsync();

            
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) 
        {
            var tag = await tagRepository.GetAsync(id);

            if (tag != null) 
            {
                var editTagRequest = new EditTagRequest
                {
                    Id =  tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }

            return View(null); 
        }
        [HttpPost]
        public async Task <IActionResult> Edit(EditTagRequest editTagRequest) 
        {
            var tag = new Tag
            {
                Id =editTagRequest.Id, 
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null) 
            {
                
            }
            else
            {
                
            }

            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }

        [HttpPost]
        public async Task <IActionResult> Delete(EditTagRequest editTagRequest) 
        {
           var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                RedirectToAction("List");
            }

            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }
    }
}
