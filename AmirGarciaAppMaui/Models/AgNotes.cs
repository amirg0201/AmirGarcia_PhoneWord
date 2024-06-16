using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmirGarciaAppMaui.Models
{
    internal class AgNotes
    {
        public string AgFilename { get; set; }
        public string AgText { get; set; }
        public DateTime AgDate { get; set; }

        public void AgSave() =>
        File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, AgFilename), AgText);

        public void AgDelete() =>
            File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, AgFilename));

        public static AgNotes AgLoad(string filename)
        {
            filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            return new AgNotes()
                 {
                     AgFilename = Path.GetFileName(filename),
                     AgText = File.ReadAllText(filename),
                     AgDate = File.GetLastWriteTime(filename)
                 };

        }

        public static IEnumerable<AgNotes> LoadAll()
        {
            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            return Directory

                    // Select the file names from the directory
                    .EnumerateFiles(appDataPath, "*.notes.txt")

                    // Each file name is used to load a note
                    .Select(filename => AgNotes.AgLoad(Path.GetFileName(filename)))

                    // With the final collection of notes, order them by date
                    .OrderByDescending(note => note.AgDate);
        }

        public AgNotes()
        {
            AgFilename = $"{Path.GetRandomFileName()}.notes.txt";
            AgDate = DateTime.Now;
            AgText = "";
        }


    }
}
