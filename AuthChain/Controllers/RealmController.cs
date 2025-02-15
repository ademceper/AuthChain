using AuthChain.DTOs;
using Microsoft.AspNetCore.Mvc;
[Route("realm")]
public class RealmController : Controller
{
    private readonly IRealmService _realmService;

    public RealmController(IRealmService realmService)
    {
        _realmService = realmService;
    }

    public async Task<IActionResult> Index()
    {
        var realms = await _realmService.GetAllAsync();
        return View(realms);  
    }

    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var realm = await _realmService.GetByIdAsync(id);
            return View(realm); 
        }
        catch (KeyNotFoundException)
        {
            TempData["ErrorMessage"] = $"Realm with ID {id} not found.";
            return RedirectToAction("Index");
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateRealmDto dto)
    {
        if (ModelState.IsValid)
        {
            var createdRealm = await _realmService.CreateAsync(dto);
            return RedirectToAction("Details", new { id = createdRealm.Id });
        }
        return View(dto); 
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var realm = await _realmService.GetByIdAsync(id);
            var updateDto = new UpdateRealmDto
            {
                Name = realm.Name,
                Description = realm.Description
            };
            return View(updateDto); 
        }
        catch (KeyNotFoundException)
        {
            TempData["ErrorMessage"] = $"Realm with ID {id} not found.";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UpdateRealmDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var updatedRealm = await _realmService.UpdateAsync(id, dto);
                return RedirectToAction("Details", new { id = updatedRealm.Id }); 
            }
            catch (KeyNotFoundException)
            {
                TempData["ErrorMessage"] = $"Realm with ID {id} not found.";
                return RedirectToAction("Index");
            }
        }
        return View(dto); 
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var deleted = await _realmService.DeleteAsync(id);
            if (deleted)
            {
                TempData["SuccessMessage"] = "Realm successfully deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = "Realm not found.";
            }
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "An error occurred while deleting the realm.";
            return RedirectToAction("Index");
        }
    }
}
