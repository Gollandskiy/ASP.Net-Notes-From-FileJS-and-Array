using DZ3_04._02._2024_3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DZ3_04._02._2024_3.Controllers
{
    public class NoteController : Controller
    {
        public List<Note> Notes { get; set; }
        public List<Note> NotesFromFile { get; set; }


        public IActionResult Notes1()
        {
            OnGet();
            return View(new Tuple<List<Note>, List<Note>>(Notes, NotesFromFile));

        }

        public void OnGet()
        {
            TestSaveAndLoad();
            GetNotesFromSource();
        }

        private void GetNotesFromSource()
        {
            Notes = new List<Note>
            {
                new Note { Title = "Nikita", Text = "Proger", CreationDate = DateTime.Now, Tags = new List<string> { "C#", "C++" } },
                new Note { Title = "Danya", Text = "Proger", CreationDate = DateTime.Now, Tags = new List<string> { "JS", "C#" } }
            };
        }

        public IActionResult SaveNotesToFile(string filePath)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(Notes);
                System.IO.File.WriteAllText(filePath, jsonString);
                return Ok("Заметки успешно сохранены!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка сохранения заметок: {ex.Message}");
            }
        }

        public IActionResult LoadNotesFromFile(string filePath)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    string jsonString = System.IO.File.ReadAllText(filePath);
                    Notes = JsonSerializer.Deserialize<List<Note>>(jsonString);
                    NotesFromFile = new List<Note>(Notes);
                    return Ok("Заметки успешно загружены!");
                }
                else
                {
                    return BadRequest("Файл не найден.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка загрузки заметок: {ex.Message}");
            }
        }

        public IActionResult TestSaveAndLoad()
        {
            string filePath = "D:/Visual Studio ШАГ/C#/DZ3(04.02.2024)3/NotesJS.json";

            IActionResult loadResult = LoadNotesFromFile(filePath);
            if (loadResult is BadRequestObjectResult)
            {
                ViewBag.NotesLoadError = (loadResult as BadRequestObjectResult).Value.ToString();
            }

            return View("Notes1", new Tuple<List<Note>, List<Note>>(Notes, NotesFromFile));
        }
    }
}