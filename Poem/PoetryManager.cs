using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
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
        public void AddPoem(Poetry poem) => repository.AddPoem(poem);
        public void RemovePoem(Poetry poem) => repository.RemovePoem(poem);
        public List<Poetry> FindByAuthor(string author) => repository.FindByAuthor(author);
        public List<Poetry> FindByTitle(string title) => repository.FindByTitle(title);

    }
}
