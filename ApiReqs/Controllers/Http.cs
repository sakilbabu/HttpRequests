using Microsoft.AspNetCore.Mvc;

namespace ApiReqs.Controllers;




[Route("Api/[controller]")]
[ApiController]
public class MyController : ControllerBase
{


    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(DemoService.Demos);
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetSingle(int id)
    {
        var demo = DemoService.Demos.FirstOrDefault(d=>d.Id==id);
        if (demo == null)
        {
            return NotFound();
        }
        return Ok(demo);
    }

    [HttpPost]
    public IActionResult Post(Demo demo)
    {
        var id = DemoService.Demos.Count + 1;
        demo.Id = id;
        DemoService.Demos.Add(demo);
    
        return CreatedAtAction("GetSingle",new {id=id},demo);
        
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id,Demo demo)
    {
        var demoFromDb = DemoService.Demos.FirstOrDefault(d => d.Id == id);
        if (demoFromDb == null)
        {
            return NotFound();
        }

        var index = DemoService.Demos.IndexOf(demoFromDb);
        Console.WriteLine(index);
        demoFromDb.Id = demo.Id;
        demoFromDb.Name = demo.Name;
        DemoService.Demos.RemoveAt(index);
        DemoService.Demos.Insert(index,demoFromDb);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var demo = DemoService.Demos.FirstOrDefault(d=>d.Id==id);
        if (demo == null)
        {
            return NotFound();
        }

        DemoService.Demos.Remove(demo);

        return NoContent();
    }
    

}

public class Demo
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public static class DemoService
{
    public static IList<Demo> Demos = new List<Demo>
    {
        new Demo{Id = 1,Name = "Manik"},
        new Demo{Id = 2,Name = "Shakil"},
    };
}