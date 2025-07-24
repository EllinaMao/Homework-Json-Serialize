using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class PoetryRepositore//хранит класс и всю логику
    {
        private List<Poetry> poems = new List<Poetry>();

        public void AddPoem(Poetry poem) => poems.Add(poem);

        public void RemovePoem(Poetry poem) => poems.Remove(poem);

        public void UpdatePoem(int index, Poetry newPoem)
        {
            if (index >= 0 && index < poems.Count)
                poems[index] = newPoem;
        }

        public List<Poetry> GetAllPoems() => poems;

        public List<Poetry> FindByAuthor(string author) =>
            poems.Where(p => p.Autor.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Poetry> FindByTitle(string title) =>
            poems.Where(p => p.Name.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Poetry> FindByTheme(string theme) =>
            poems.Where(p => p.Theme.Contains(theme, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Poetry> FindByYear(int year) =>
            poems.Where(p => p.YearOfCreation.Year == year).ToList();

        public List<Poetry> FindByWord(string word) =>
            poems.Where(p => p.Text.Contains(word, StringComparison.OrdinalIgnoreCase)).ToList();
        public List<Poetry> FindByLength(int length) =>
            poems.Where(p => p.Text.Length == length).ToList();


    }
}
