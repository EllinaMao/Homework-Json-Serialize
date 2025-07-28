using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poem.poetry
{
    public class PoetryManager
    {
        private static PoetryManager? instance;
        private readonly PoetryRepositore repository;
        private PoetryManager()
        {
            repository = new PoetryRepositore();
        }

        public static PoetryManager Instance
        {
            get
            {
                if (instance == null) 
                { 
                    instance = new PoetryManager();
                }
                return instance;
            }
        }

        public List<Poetry> GetAllPoems() => repository.GetAllPoems();
        public void AddPoem(Poetry poem) => repository.AddPoem(poem);
        public void RemovePoem(int index) => repository.RemovePoem(index);
        public void UpdatePoem(int index, Poetry newPoem) => repository.UpdatePoem(index, newPoem);
        public List<Poetry> FindByAuthor(string author) => repository.FindByAuthor(author);
        public List<Poetry> FindByTitle(string title) => repository.FindByTitle(title);
        public void SaveToFile(string filePath) => repository.SaveToFile(filePath);
        public void LoadFromFile (string filePath) => repository.LoadFromFile(filePath);
        public void ShowAllPoems() => repository.ShowAllPoems();
    }
}
