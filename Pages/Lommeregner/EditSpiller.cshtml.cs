using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MatchMakerDBU.Model;
using MatchMakerDBU.Services;

namespace MatchMakerDBU.Pages.Lommeregner
{
    public class EditSpillerModel : PageModel
    {
        private ISpillerService _service;

        private string position;

        public EditSpillerModel(ISpillerService service)
        {
            _service = service;
        }


        [BindProperty]
        public int Nummer { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public double Rating { get; set; }

        [BindProperty]
        public string Position { get => position; set => position = value.ToLower(); } 
    

        public void OnGet(int nummer)
        {
            Spiller editSpiller = _service.FindSpiller(nummer);

            Nummer = editSpiller.Nummer;
            Name = editSpiller.Name;
            Rating = editSpiller.Rating;
            Position = editSpiller.Type.ToString();

        }

 

        public IActionResult OnPostEdit()
        {
            Spiller editSpiller = new Spiller();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            editSpiller.Nummer = Nummer;
            editSpiller.Name = Name;
            editSpiller.Rating = Rating;

            switch (Position)
            {
                case "forsvar": editSpiller.Type = SpillerType.Forsvar; break;
                case "m?lmand": editSpiller.Type = SpillerType.M?lmand; break;
                case "midtbane": editSpiller.Type = SpillerType.Midtbane; break;
                case "angriber": editSpiller.Type = SpillerType.Angriber; break;
                default:
                    break;
            }


            _service.EditSpiller(editSpiller);

            return RedirectToPage("Index");

        }

        public IActionResult OnPostFortryd()
        {
            return RedirectToPage("Index");
        }


    }
}
