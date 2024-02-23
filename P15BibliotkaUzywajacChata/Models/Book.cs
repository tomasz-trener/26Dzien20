using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P15BibliotkaUzywajacChata.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearOfPublication { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }

        // Konstruktor domyślny
        public Book()
        {
        }

        // Konstruktor z parametrami umożliwiający szybkie tworzenie obiektu z podstawowymi informacjami
        public Book(string title, string author, int yearOfPublication, string publisher, string genre, string description)
        {
            Title = title;
            Author = author;
            YearOfPublication = yearOfPublication;
            Publisher = publisher;
            Genre = genre;
            Description = description;
        }

        // Metoda ToString() może zostać nadpisana, aby zapewnić reprezentację obiektu w postaci tekstowej, przydatną np. przy debugowaniu
        public override string ToString()
        {
            return $"Title: {Title}, Author: {Author}, Year: {YearOfPublication}, Publisher: {Publisher}, Genre: {Genre}, Description: {Description}";
        }
    }
}
